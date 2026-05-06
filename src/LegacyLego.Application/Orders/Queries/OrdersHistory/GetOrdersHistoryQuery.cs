using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.OrdersHistory;


namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public sealed record GetOrdersHistoryQuery(Guid UserId, OrderHistoryRequest Filter) : IQuery<OrdersHistoryResponse>;