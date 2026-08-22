using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Orders.Commands.Create;
using LegacyLego.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyLego.Infrastructure.BackgroundJobs;

public class KeycloakEventsConsumer : BackgroundService
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly ILogger<KeycloakEventsConsumer> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConnectionFactory _connectionFactory;
    private readonly RabbitMqOptions _options;

    public KeycloakEventsConsumer(
        ILogger<KeycloakEventsConsumer> logger,
        IServiceScopeFactory scopeFactory,
        IConnectionFactory connectionFactory,
        IOptions<RabbitMqOptions> options)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _connectionFactory = connectionFactory;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(ct);
        using var channel = await connection.CreateChannelAsync(cancellationToken: ct);

        await channel.BasicQosAsync(
            prefetchSize: 0,
            prefetchCount: 1,
            global: false,
            cancellationToken: ct);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += (sender, ea) => OnMessageReceivedAsync(channel, ea, ct);

        await channel.BasicConsumeAsync(
            queue: _options.KeycloakEventQueue,
            autoAck: false,
            consumer: consumer,
            cancellationToken: ct);

        await Task.Delay(Timeout.Infinite, ct);
    }

    private async Task OnMessageReceivedAsync(IChannel channel, BasicDeliverEventArgs ea, CancellationToken ct)
    {
        var body = ea.Body.ToArray();
        var messageJson = Encoding.UTF8.GetString(body);

        _logger.LogDebug("Получено сырое сообщение из RabbitMQ: {Json}", messageJson);

        try
        {
            var @event = JsonSerializer.Deserialize<KeycloakUserRegisteredIntegrationEvent>(messageJson, JsonSerializerOptions);

            if (@event != null && @event.Type == "REGISTER")
            {
                _logger.LogDebug("Успешно распаршено событие регистрации для UserId: {UserId}", @event.UserId);

                using var scope = _scopeFactory.CreateScope();
                var keycloakClient = scope.ServiceProvider.GetRequiredService<IIdentityProviderService>();
                var commandDispatcher = scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();

                // запрос к Keycloak Admin API за полным профилем
                var userProfile = await keycloakClient.GetUserProfileByIdAsync(@event.UserId, ct);

                if (userProfile is null)
                    _logger.LogWarning("Не удалось найти профиль пользователя с ID {UserId} в Keycloak", @event.UserId);

                var registrationClientCommand = new RegisterClientCommand(userProfile!);
                var result = await commandDispatcher.DispatchAsync(registrationClientCommand);

                if (result.IsSuccess)
                    _logger.LogInformation("Круто,  сработало");
            }

            // Подтверждение успешной обработки сообщения в RabbitMQ
            await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обработке сообщения из RabbitMQ или запросе к Keycloak Admin API");

            // Возвращаем сообщение обратно в очередь
            await channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: ct);
        }
    }
}
public record KeycloakUserRegisteredIntegrationEvent(
    [property: JsonPropertyName("userId")] Guid UserId,
    [property: JsonPropertyName("time")] long Timestamp,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("realmId")] string RealmId,
    [property: JsonPropertyName("clientId")] string ClientId,
    [property: JsonPropertyName("details")] KeycloakEventDetailsDto? Details
);

public record KeycloakEventDetailsDto(
    [property: JsonPropertyName("username")] string? Username,
    [property: JsonPropertyName("email")] string? Email,
    [property: JsonPropertyName("first_name")] string? FirstName,
    [property: JsonPropertyName("last_name")] string? LastName
);