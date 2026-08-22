using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices.IClientProvisioningService;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Commands.Cancel;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Orders.Commands.Create;

public sealed class RegisterClientCommandHandler(
    IClientProvisioningService provisioningService) : ICommandHandler<RegisterClientCommand, RegisterClientDetails>
{
    public async Task<Result<RegisterClientDetails>> HandleAsync(RegisterClientCommand command, CancellationToken ct)
    {
        var provisionResult = await provisioningService.EnsureExistsAsync(command.ExternalUserProfile, ct);
        if (provisionResult.IsFailure)
            return Result<RegisterClientDetails>.Failure(provisionResult.Error);

        var (client, status) = provisionResult.Value;

        var details = status switch
        {
            ClientProvisioningStatus.AlreadyExists =>
                RegisterClientDetails.GetAlreadyProcessedDetails(client.Id.Value),

            ClientProvisioningStatus.Created =>
                RegisterClientDetails.GetSuccessfullyRegisteredCodeDetails(client.Id.Value),

            _ => throw new ArgumentOutOfRangeException()
        };

        return Result<RegisterClientDetails>.Success(details);
    }
}