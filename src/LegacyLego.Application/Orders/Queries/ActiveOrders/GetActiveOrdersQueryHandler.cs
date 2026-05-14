using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public class GetActiveOrdersQueryHandler(IOrderRepository repository) : IQueryHandler<GetActiveOrdersQuery, IReadOnlyList<OrderSummaryDto>>
{
    public async Task<Result<IReadOnlyList<OrderSummaryDto>>> HandleAsync(GetActiveOrdersQuery query, CancellationToken ct)
    {
        var specification = new ActiveOrderSpecification(query.UserId);

        var result = await repository.GetOrders(specification, ct);

        return Result<IReadOnlyList<OrderSummaryDto>>.Success(result);
    }
}