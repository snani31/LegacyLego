using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Common;

public sealed record OrderSummaryDto(
    Guid OrderId,
    OrderStatus Status,
    decimal TotalAmount,
    string Currency,
    DateTime CreatedAt,
    int ItemsCount
);