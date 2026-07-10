using LegacyLego.Application.Orders.Common;
using LegacyLego.Application.Orders.Common.Projections;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using System.Linq.Expressions;

namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public class OrderHistorySpecification : Specification<Order, OrderId, OrderSummaryDto>
{
    public OrderHistorySpecification(Guid clientId, DateTime? cursorDate, OrderId? cursorOrderId, int limit)
        : base(OrderProjections.Summary)
    {
        AddFilter(o => o.ClientId == clientId);

        var historyStatuses = new[] { OrderStatus.Paid, OrderStatus.Cancelled, OrderStatus.Refunded };
        AddFilter(o => historyStatuses.Contains(o.Status));

        // Keyset Pagination
        if (cursorDate.HasValue && cursorOrderId is not null)
        {
            // дата меньше курсорной, ИЛИ (дата равна курсорной, но ID меньше курсорного)
            AddFilter(o => o.CreationDateUtc < cursorDate.Value ||
                          (o.CreationDateUtc == cursorDate.Value && o.Id < cursorOrderId));
        }

        AddOrderByDescending(o => o.CreationDateUtc);
        AddOrderByDescending(o => o.Id);

        SetLimitNum(limit);
    }
}