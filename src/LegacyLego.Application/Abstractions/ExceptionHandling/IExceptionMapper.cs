namespace LegacyLego.Application.Abstractions.ExceptionHandling;

public interface IExceptionMapper
{
    public bool TryMap(Exception exception, out AppFailureDescription? description);
}