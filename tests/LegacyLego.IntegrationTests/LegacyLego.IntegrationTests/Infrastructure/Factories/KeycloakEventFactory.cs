using LegacyLego.Application.Dto;
using LegacyLego.Infrastructure.BackgroundJobs;
using LegacyLego.IntegrationTests.Infrastructure.Helpers;

namespace LegacyLego.IntegrationTests.Infrastructure.Factories;

public static class KeycloakEventFactory
{
    public static KeycloakUserRegisteredIntegrationEvent CreateUserRegistered(
        Guid? userId = null,
        string? username = null,
        string? email = null,
        string? firstName = "John",
        string? lastName = "Doe")
    {
        var id = userId ?? Guid.NewGuid();
        var uniqueUsername = username ?? $"user_{id:N}";
        var uniqueEmail = email ?? $"user_{id:N}@legacylego.local";

        return new KeycloakUserRegisteredIntegrationEvent(
            UserId: id,
            Timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            Type: "REGISTER",
            RealmId: "legacy-lego-realm",
            ClientId: "legacy-lego-app",
            Details: new KeycloakEventDetailsDto(
                Username: uniqueUsername,
                Email: uniqueEmail,
                FirstName: firstName,
                LastName: lastName
            )
        );
    }

    public static KeycloakUserRegisteredIntegrationEvent CreateUserRegisteredWithInvalidEmail(
        Guid? userId = null)
    {
        return CreateUserRegistered(
            userId: userId,
            email: "invalid-email-format");
    }

    public static ExternalUserProfile ToExternalUserProfile(
        this KeycloakUserRegisteredIntegrationEvent @event)
    {
        return new ExternalUserProfile(
            UserId: @event.UserId,
            Username: @event.Details?.Username ?? $"user_{@event.UserId:N}",
            Email: @event.Details?.Email ?? string.Empty,
            FirstName: @event.Details?.FirstName,
            LastName: @event.Details?.LastName,
            PhoneNumber: PhoneNumberGenerator.GeneratePhoneNumber(@event.UserId),
            CreatedAtUtc: DateTime.UtcNow
        );
    }
}