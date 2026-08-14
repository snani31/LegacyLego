using LegacyLego.Application.Abstractions.ExternalServices;
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

                // запрос к Keycloak Admin API за полным профилем
                var userProfile = await keycloakClient.GetUserProfileByIdAsync(@event.UserId, ct);

                if (userProfile != null)
                {
                    _logger.LogDebug(
                        "Профиль пользователя извлечен! Username: {Username}, Email: {Email}, Phone: {Phone}",
                        userProfile.Username,
                        userProfile.Email,
                        userProfile.PhoneNumber ?? "Не указан");

                    _logger.LogDebug(
                        "Данные готовы для создания агрегата Client: Id={Id}, Nickname={Nickname}, CreatedAt={CreatedAt}",
                        userProfile.UserId,
                        userProfile.Username,
                        userProfile.CreatedAtUtc);

                    // TODO: реализация работы команды регистрации пользователя с диспетчером команд;
                }
                else
                {
                    _logger.LogWarning("Не удалось найти профиль пользователя с ID {UserId} в Keycloak", @event.UserId);
                }
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