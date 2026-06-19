using LegacyLego.Application.Abstractions.ExceptionHandling;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Diagnostics;

public sealed class DomainExceptionMapper : IExceptionMapper
{
    public bool TryMap(Exception exception, out AppFailureDescription? description)
    {
        if (exception is DomainException domainException)
        {
            description = new AppFailureDescription(
                Kind: ExceptionFailureKind.DomainLevelException,
                Title: "Критическое нарушение бизнес-состояния системы",
                Detail: domainException.Error.Message,
                ErrorCode: domainException.Error.Code
            );
            return true;
        }

        description = null;
        return false;
    }
}