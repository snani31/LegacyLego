using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.OrdersHistory;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public class GetOrdersHistoryQueryHandler(IOrderRepository repository) : IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse>
{
    public async Task<Result<OrdersHistoryResponse>> HandleAsync(GetOrdersHistoryQuery query, CancellationToken ct)
    {
        var specification = new OrderHistorySpecification(query.UserId,query.Filter);
        var orders = await repository.GetOrdersAsync(specification, ct);

        specification.DropPagination();
        var count = await repository.GetOrdersCountAsync(specification, ct);

        var result = new OrdersHistoryResponse(orders,count);

        return Result<OrdersHistoryResponse>.Success(result);
    }
}