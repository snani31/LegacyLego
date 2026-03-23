using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Exceptions;

public class InvariantViolationException : DomainException
{
    public InvariantViolationException(ExceptionalError error) : base(error) { }
}
