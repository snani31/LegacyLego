using LegacyLego.Application.Abstractions.ExceptionHandling;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Diagnostics;

public sealed class InfrastructureExceptionMapper : IExceptionMapper
{
    public bool TryMap(Exception exception, out AppFailureDescription? description)
    {
        if (exception is DbUpdateException)
        {
            description = new AppFailureDescription(
                Kind: ExceptionFailureKind.InfrastructureLevelException,
                Title: "Хранилище данных временно недоступно",
                Detail: "Ошибка при выполнении операции с базой данных PostgreSQL."
            );
            return true;
        }

        description = null;
        return false;
    }
}