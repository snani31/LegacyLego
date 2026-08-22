using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed record RegisterClientDetails : ICustomLogSeverity
{
    public const string AlreadyProcessedCode = "RegisterClient.AlreadyProcessed";
    public const string SuccessfullyRegisteredCode = "RegisterClient.SuccessfullyRegistered";

    public string Code { get; }
    public  Guid ClientId { get; }
    public string Message { get; }

    public bool IsWarning => Code switch
    {
        AlreadyProcessedCode => true,
        SuccessfullyRegisteredCode => false,
        _ => false
    };

    private RegisterClientDetails(string code,
    Guid clientId,
    string message)
    {
        this.Code = code;
        this.ClientId = clientId;
        this.Message = message;
    }

    internal static RegisterClientDetails GetAlreadyProcessedDetails(Guid enterId)
    {
        return new RegisterClientDetails(
            code: AlreadyProcessedCode,
            clientId: enterId,
            message: $"Failed to register the client in the database because an account with the enter client Id:{enterId} already exists.");
    }

    internal static RegisterClientDetails GetSuccessfullyRegisteredCodeDetails(Guid clientId)
    {
        return new RegisterClientDetails(
            code: SuccessfullyRegisteredCode,
            clientId: clientId,
            message: $"New Client with clientId: {clientId} successfully registered.");
    }
}