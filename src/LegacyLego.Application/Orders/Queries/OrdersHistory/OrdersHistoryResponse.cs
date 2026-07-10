using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public record OrdersHistoryResponse(
    IReadOnlyCollection<OrderSummaryDto> Orders,
    string? NextCursor);