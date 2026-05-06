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
    public OrderHistorySpecification(Guid clientId, OrderHistoryRequest filter)
        : base(OrderProjections.Summary)
    {
        AddFilter(o => o.ClientId == clientId);

        var historyStatuses = new[] { OrderStatus.Paid, OrderStatus.Cancelled, OrderStatus.Refunded };
        AddFilter(o => historyStatuses.Contains(o.Status));

        if (filter.MinPrice.HasValue)
            AddFilter(o => o.TotalPrice.Sum >= filter.MinPrice.Value);

        ApplySorting(filter.SortBy, filter.SortDescending);

        SetSkipNum(filter.SkipRecords);
        SetLimitNum(filter.TakeRecords);
    }

    private void ApplySorting(string? sortBy, bool isDescending)
    {
        Expression<Func<Order, object>> expression = sortBy?.ToLower() switch
        {
            "price" => o => o.TotalPrice.Sum,
            "date" => o => o.CreationDateUtc,
            _ => o => o.CreationDateUtc 
        };

        if (isDescending) AddOrderByDescending(expression);
        else AddOrderBy(expression);
    }

    public void DropPagination()
    {
        DropLimit();
        DropSkip();
    }
}