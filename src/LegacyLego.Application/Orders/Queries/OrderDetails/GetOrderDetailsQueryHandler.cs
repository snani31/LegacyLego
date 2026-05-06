using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.ActiveOrders;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Queries.OrderDetails;

public class GetOrderDetailsQueryHandler(IOrderRepository repository) : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    public async Task<Result<OrderDetailsDto>> HandleAsync(GetOrderDetailsQuery query, CancellationToken ct)
    {
        var specification = new OrderDetailsSpecification(query.UserId, query.OrderId);
        var order = await repository.GetOrder(specification, ct);

        if (order is null)
            return Result<OrderDetailsDto>.Failure(OrderErrors.GetNotFoundByOrderIdError(OrderId.From(query.OrderId)));

        return Result<OrderDetailsDto>.Success(order);
    }
}