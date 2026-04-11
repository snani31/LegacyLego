using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Exceptions;

public abstract class InfrastructureException : Exception
{
    public ExceptionalError Error { get; }

    protected InfrastructureException(ExceptionalError error)
        : base(error.Code + ": " + error.Message)
    {
        Error = error;
    }
}