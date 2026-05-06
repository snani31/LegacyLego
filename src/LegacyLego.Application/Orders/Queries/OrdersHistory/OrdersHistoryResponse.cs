using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public sealed record OrdersHistoryResponse(
    IReadOnlyList<OrderSummaryDto> Orders,
    int OrdersCount);