using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Orders.Commands.Create;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using MassTransit;

namespace LegacyLego.Infrastructure.BackgroundJobs;

public class KeycloakEventsConsumer : IConsumer<KeycloakUserRegisteredIntegrationEvent>
{
    private readonly ILogger<KeycloakEventsConsumer> _logger;
    private readonly IIdentityProviderService _keycloakClient;
    private readonly ICommandDispatcher _commandDispatcher;

    public KeycloakEventsConsumer(
        ILogger<KeycloakEventsConsumer> logger,
        IIdentityProviderService keycloakClient,
        ICommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _keycloakClient = keycloakClient;
        _commandDispatcher = commandDispatcher;
    }

    public async Task Consume(ConsumeContext<KeycloakUserRegisteredIntegrationEvent> context)
    {
        var @event = context.Message;

        _logger.LogDebug("Получено событие из RabbitMQ для пользователя: {UserId}", @event.UserId);

        if (@event.Type != "REGISTER")
        {
            _logger.LogWarning("Игнорирование сообщения: тип события '{Type}' != REGISTER.", @event.Type);
            // Успешный выход -> MassTransit автоматически отправляет ACK
            return;
        }

        // Запрос к Keycloak API.
        // HttpRequestException НЕ ловим локально — даем ему вылететь, 
        // чтобы MassTransit применил Retry Policy (повторные попытки при сетевом сбое).
        var userProfile = await _keycloakClient.GetUserProfileByIdAsync(@event.UserId, context.CancellationToken);

        if (userProfile is null)
        {
            _logger.LogError("Профиль пользователя с ID {UserId} не найден в Keycloak. Отправка в DLQ.", @event.UserId);
            // Бросаем исключение, чтобы MassTransit убрал сообщение в DLQ без повторов
            throw new KeycloakUserProfileNotFoundException(@event.UserId);
        }

        var registrationClientCommand = new RegisterClientCommand(userProfile);
        var result = await _commandDispatcher.DispatchAsync(registrationClientCommand);

        if (result.IsFailure)
        {
            _logger.LogError("Не удалось зарегистрировать клиента в базе. Ошибка: {Error}. Отправка в DLQ.", result.Error);
            throw new ClientRegistrationFailedException(result.Error.ToString());
        }

        _logger.LogInformation("Клиент {UserId} успешно зарегистрирован в системе.", @event.UserId);
    }
}

// Кастомные исключения для разграничения бизнес-ошибок и сетевых сбоев в политиках MassTransit
public class KeycloakUserProfileNotFoundException : Exception
{
    public KeycloakUserProfileNotFoundException(Guid userId)
        : base($"UserProfile for userId {userId} was not found in Keycloak.") { }
}

public class ClientRegistrationFailedException : Exception
{
    public ClientRegistrationFailedException(string reason)
        : base($"Failed to register client: {reason}") { }
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