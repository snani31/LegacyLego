namespace LegacyLego.Domain.Shared;

public abstract class DomainException : Exception
{
    public ExceptionalError Error { get; }

    protected DomainException(ExceptionalError error)
        : base(error.Code + ": " + error.Message) 
    {
        Error = error;
    }
}