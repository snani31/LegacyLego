using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Exceptions;

public class InvalidDomainStateException : DomainException
{
    public InvalidDomainStateException(ExceptionalError error) : base(error) { }
}