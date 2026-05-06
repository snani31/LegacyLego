using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.OrderDetails;


namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public sealed record GetOrderDetailsQuery(Guid UserId, Guid OrderId) : IQuery<OrderDetailsDto>;