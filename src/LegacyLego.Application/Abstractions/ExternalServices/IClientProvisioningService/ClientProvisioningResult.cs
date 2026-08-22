using LegacyLego.Domain.Aggregates;

namespace LegacyLego.Application.Abstractions.ExternalServices.IClientProvisioningService;

public record ClientProvisioningResult(Client Client, ClientProvisioningStatus Status)
{
    public bool IsNewlyCreated => Status == ClientProvisioningStatus.Created;
    public bool AlreadyExisted => Status == ClientProvisioningStatus.AlreadyExists;
}