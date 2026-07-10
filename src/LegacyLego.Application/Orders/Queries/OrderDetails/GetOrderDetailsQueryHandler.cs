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
        var orderId = OrderId.From(query.OrderId);

        var specification = new OrderDetailsSpecification(query.UserId, orderId);

        var orderDetails = await repository.GetOrderAsync(specification, ct);

        if (orderDetails is null)
            return Result<OrderDetailsDto>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        return Result<OrderDetailsDto>.Success(orderDetails);
    }
}