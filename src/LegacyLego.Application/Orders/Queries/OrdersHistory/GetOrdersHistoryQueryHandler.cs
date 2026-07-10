using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Options;
using LegacyLego.Application.Orders.Queries.OrdersHistory;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public class GetOrdersHistoryQueryHandler(
    IOrderRepository repository,
    ICursorSerializer cursorSerializer,
    IOptions<OrderHistoryOptions> options) : IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse>
{
    public async Task<Result<OrdersHistoryResponse>> HandleAsync(GetOrdersHistoryQuery query, CancellationToken ct)
    {
        var pageSize = options.Value.PageSize;
        var takeLimit = pageSize + 1;

        DateTime? cursorDate = null;
        Guid? cursorId = null;
        OrderId? cursorOrderId = null;

        if (!string.IsNullOrWhiteSpace(query.Filter.Cursor))
        {
            var parseResult = cursorSerializer.Deserialize<(DateTime Date, Guid Id)>(query.Filter.Cursor);

            if (parseResult.IsFailure) 
                return Result<OrdersHistoryResponse>.Failure(parseResult.Error);

            (cursorDate, cursorId) = parseResult.Value;

            cursorOrderId = OrderId.From(cursorId.Value);
        }

        var specification = new OrderHistorySpecification(
             clientId: query.UserId,
             cursorDate: cursorDate,
             cursorOrderId: cursorOrderId,
             limit: takeLimit);

        var orders = await repository.GetOrdersAsync(specification, ct);

        string? nextCursor = null;

        if (orders.Count == takeLimit)
        {
            var lastPagedOrder = orders[pageSize - 1];
            nextCursor = cursorSerializer.Serialize((lastPagedOrder.CreatedAt, lastPagedOrder.OrderId));
        }

        var resultOrders = orders.Count > pageSize
        ? orders.Take(pageSize).ToList()
        : orders;

        var result = new OrdersHistoryResponse(resultOrders, nextCursor);

        return Result<OrdersHistoryResponse>.Success(result);
    }
}