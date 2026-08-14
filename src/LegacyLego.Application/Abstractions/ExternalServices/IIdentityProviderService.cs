namespace LegacyLego.Application.Abstractions.ExternalServices;

public record ExternalUserProfile(
    Guid UserId,
    string Username,
    string Email,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    DateTime CreatedAtUtc
);

public interface IIdentityProviderService
{
    public Task<ExternalUserProfile?> GetUserProfileByIdAsync(Guid userId, CancellationToken ct = default);
}