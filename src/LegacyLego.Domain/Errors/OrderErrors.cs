using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class OrderErrors
{
    public static Error GetStatusTransitionFailureError(
        OrderAction action,
        OrderStatus actualStatus,
        OrderStatus nextStatus)
    {
        return new(
            Code: "Order.StatusTransitionFailure",
            Message: $"Action {action.ToString()} не позволяет перевести Order из статуса {actualStatus.ToString()} в {nextStatus.ToString()}");
    }

    public static Error GetItemsCountInvalidError(int itemsCount)
    {
        return new(
            Code: "Order.ItemsCountInvalid",
            Message: $"Невозможно создать заказ с общим количеством позиций {itemsCount}, должна быть хотя бы 1 позиция");
    }

    public static Error GetItemsCurrenciesMismatchError()
    {
        return new(
            Code: "Order.ItemsCurrenciesMismatch",
            Message: "Стоимости всех позиций заказа не должны быть представлены разными валютами");
    }

    public static Error GetItemsTotalBelowZeroError(decimal total)
    {
        return new(
            Code: "Order.ItemsTotalBelowZero",
            Message: $"Общая стоимость всех позиций заказа не должна быть меньше 0, {total} не подходит");
    }
}
