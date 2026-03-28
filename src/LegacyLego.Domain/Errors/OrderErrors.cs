using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Errors;

public static class OrderErrors
{
    public const string StatusTransitionFailureCode = "Order.StatusTransitionFailure";
    public const string ItemsCountInvalidCode = "Order.ItemsCountInvalid";
    public const string ItemsCurrenciesMismatchCode = "Order.ItemsCurrenciesMismatch";
    public const string ItemsTotalBelowZeroCode = "Order.ItemsTotalBelowZero";

    public static Error GetStatusTransitionFailureError(
        OrderAction action,
        OrderStatus actualStatus,
        OrderStatus nextStatus)
    {
        return new(
            Code: StatusTransitionFailureCode,
            Message: $"Action {action.ToString()} не позволяет перевести Order из статуса {actualStatus.ToString()} в {nextStatus.ToString()}");
    }

    public static Error GetItemsCountInvalidError(int itemsCount)
    {
        return new(
            Code: ItemsCountInvalidCode,
            Message: $"Невозможно создать заказ с общим количеством позиций {itemsCount}, должна быть хотя бы 1 позиция");
    }

    public static Error GetItemsCurrenciesMismatchError()
    {
        return new(
            Code: ItemsCurrenciesMismatchCode,
            Message: "Стоимости всех позиций заказа не должны быть представлены разными валютами");
    }

    public static Error GetItemsTotalBelowZeroError(decimal total)
    {
        return new(
            Code: ItemsTotalBelowZeroCode,
            Message: $"Общая стоимость всех позиций заказа не должна быть меньше 0, {total} не подходит");
    }
}