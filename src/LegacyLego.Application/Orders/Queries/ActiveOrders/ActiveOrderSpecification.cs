using LegacyLego.Application.Orders.Common;
using LegacyLego.Application.Orders.Common.Projections;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public class ActiveOrderSpecification : Specification<Order, OrderId, OrderSummaryDto>
{
    public ActiveOrderSpecification(Guid clientId) : base(OrderProjections.Summary)
    {
        AddFilter(order => order.ClientId == clientId);
        AddFilter(order => order.Status == OrderStatus.PendingPayment);
    }
}