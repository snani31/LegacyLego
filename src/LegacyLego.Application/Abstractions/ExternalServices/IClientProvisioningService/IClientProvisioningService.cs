using LegacyLego.Application.Dto;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.ExternalServices.IClientProvisioningService;

public interface IClientProvisioningService
{
    public Task<Result<ClientProvisioningResult>> EnsureExistsAsync(
        ExternalUserProfile profile,
        CancellationToken ct = default);
}