using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Commands.Pay;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Expire;

public sealed class ExpireOrderCommandHandler(
IOrderRepository orderRepository,
IUnitOfWork unitOfWork) : ICommandHandler<ExpireOrderCommand, ExpirationOrderDetails>
{
    public async Task<Result<ExpirationOrderDetails>> HandleAsync(ExpireOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);
        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<ExpirationOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Expire();
        if (result.IsFailure && order.Status is OrderStatus.Expired) 
            return Result<ExpirationOrderDetails>.Success(ExpirationOrderDetails.GetAlreadyExpiredDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<ExpirationOrderDetails>.Success(ExpirationOrderDetails.GetWrongStatusTransitionDetails(orderIdGuid, order.Status));

        await unitOfWork.SaveChangesAsync(ct);
        return Result<ExpirationOrderDetails>.Success(ExpirationOrderDetails.GetExpiredSuccessfullyDetails(orderIdGuid));
    }
}