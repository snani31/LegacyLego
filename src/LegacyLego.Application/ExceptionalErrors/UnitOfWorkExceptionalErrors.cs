using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.ExceptionalErrors;

public static class UnitOfWorkExceptionalErrors
{
    public const string DatabaseSaveErrorCode = "UnitOfWork.SaveError";

    public static ExceptionalError GetDatabaseSaveError(Guid orderId, string internalMessage)
    {
        return new(
            Code: DatabaseSaveErrorCode,
            Message: $"Критическая ошибка при сохранении заказа {orderId}. Внутренняя ошибка: {internalMessage}"
        );
    }
}