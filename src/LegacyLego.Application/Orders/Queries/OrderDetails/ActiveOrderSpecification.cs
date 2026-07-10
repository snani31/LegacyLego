using LegacyLego.Application.Orders.Common.Projections;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Queries.OrderDetails;

public class OrderDetailsSpecification : Specification<Order, OrderId, OrderDetailsDto>
{
    public OrderDetailsSpecification(Guid clientId, OrderId orderId) : base(OrderProjections.Details)
    {
        AddFilter(order => order.ClientId == clientId);
        AddFilter(order => order.Id == orderId);
    }
}