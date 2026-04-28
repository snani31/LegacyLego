using LegacyLego.Domain.Shared;
namespace LegacyLego.Application.Exceptions;

public class UniqueConstraintViolation : InfrastructureException
{
    public UniqueConstraintViolation(ExceptionalError error) : base(error) { }
}