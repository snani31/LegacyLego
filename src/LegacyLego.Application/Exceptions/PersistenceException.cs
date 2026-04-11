using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Exceptions;

public class PersistenceException : InfrastructureException
{
    public PersistenceException(ExceptionalError error) : base(error) { }
}