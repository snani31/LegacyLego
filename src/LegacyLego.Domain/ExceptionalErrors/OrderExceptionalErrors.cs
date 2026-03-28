using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class OrderExceptionalErrors
{
    public const string ContainsNoItemsErrorCode = "OrderContainsNoItems";
    public const string FrozenTotalPriceNotCalculatedErrorCode = "Order.FrozenTotalPriceNotCalculated";
    public const string WrongOrderStatusToGetTotalPriceErrorCode = "Order.WrongOrderStatusToGetTotalPrice";
    public static ExceptionalError GetOrderContainsNoItemsError()
    {
        return new(
            Code: ContainsNoItemsErrorCode,
            Message: $"Order не может не содержать ни 1 позиции при расчёте TotalPrice"
        );
    }

    public static ExceptionalError GetFrozenTotalPriceNotCalculatedError(OrderStatus status)
    {
        return new(
            Code: FrozenTotalPriceNotCalculatedErrorCode,
            Message: $"При обращении к полю _frozenTotalPrice произошла ошибка: значение свойства не расчитано." +
            $"в том случае, если текущий Order Status имеет значение {status.ToString()}, занчение _frozenTotalPrice уже должно быть рассчитано"
        );
    }

    public static ExceptionalError GetWrongOrderStatusToGetTotalPriceError(OrderStatus status)
    {
        return new(
            Code: WrongOrderStatusToGetTotalPriceErrorCode,
            Message: $"Для статуса {status.ToString()} рассчитать значение TotalPrice невозможно"
        );
    }
}