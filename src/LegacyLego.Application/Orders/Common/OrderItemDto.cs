namespace LegacyLego.Application.Orders.Common;

public sealed record OrderItemDto(
    string Title,
    int Quantity,
    Guid ProductId,
    decimal UnitPriceAmount);