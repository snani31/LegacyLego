using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class OrderItemErrors
{
    public static Error GetTitleInvalidError()
    {
        return new(
            Code: "OrderItem.TitleInvalid",
            Message: "В названии товара не должно быть пустой строки"
        );
    }

    public static Error GetQuantityBelowOneError(int quantity)
    {
        return new(
            Code: "OrderItem.QuantityBelowOne",
            Message: "Позиция заказа не может быть создана в количестве меньшем единице. " +
                     $"Значение {quantity} не соответствует правилам валидации"
        );
    }
}
