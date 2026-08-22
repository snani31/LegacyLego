using LegacyLego.Application.Dto;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface IIdentityProviderService
{
    public Task<ExternalUserProfile?> GetUserProfileByIdAsync(Guid userId, CancellationToken ct = default);
}