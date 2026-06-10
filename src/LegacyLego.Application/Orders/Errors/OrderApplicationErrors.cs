using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Errors;

public static class OrderApplicationErrors
{
    public const string UnauthorizedAccessToOrderByClientIdCode = "Order.UnauthorizedAccessToOrderByClientId";

    public static Error GetUnauthorizedAccessToOrderByClientIdError(OrderId orderId, Guid clientId)
    {
        return new(
            Code: UnauthorizedAccessToOrderByClientIdCode,
            Message: $"Запрещено обращение к заказу: {orderId.Value} по следующему идентификатору клиента: {clientId}"
        );
    }
}