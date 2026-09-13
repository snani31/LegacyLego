using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;
namespace LegacyLego.IntegrationTests.Infrastructure.Factories;

public static class ClientFactory
{
    public static Client Create(
        Guid? id = null,
        string? username = null,
        string? email = null,
        string? firstName = "Test",
        string? lastName = "User")
    {
        var targetId = id ?? Guid.NewGuid();
        var clientId = ClientId.From(targetId).Value;

        // Автоматически генерируем уникальные username и email, если они не переданы явно
        var uniqueUsername = username ?? $"user_{targetId:N}";
        var uniqueEmail = email ?? $"user_{targetId:N}@legacylego.local";

        var emailObj = Email.Create(uniqueEmail).Value;

        var result = Client.Create(
            id: clientId,
            username: uniqueUsername,
            createdAt: DateTime.UtcNow,
            email: emailObj,
            preferences: ClientPreferences.Default,
            firstName: firstName,
            lastName: lastName,
            phoneNumber: null);

        if (result.IsFailure)
        {
            throw new InvalidOperationException($"Не удалось создать тестового клиента: {result.Error.Message}");
        }

        return result.Value;
    }
}