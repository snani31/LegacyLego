using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Commands.Refund;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed class RefundOrderCommandHandler(
IOrderRepository orderRepository,
IUnitOfWork unitOfWork) : ICommandHandler<RefundOrderCommand, RefundOrderDetails>
{
    public async Task<Result<RefundOrderDetails>> HandleAsync(RefundOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<RefundOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Refund();
        if (result.IsFailure && order.Status is OrderStatus.Refunded)
            return Result<RefundOrderDetails>.Success(RefundOrderDetails.GetAlreadyRefundedDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<RefundOrderDetails>.Success(RefundOrderDetails.GetWrongStatusTransitionDetails(orderIdGuid, order.Status));

        await unitOfWork.SaveChangesAsync(ct);
        return Result<RefundOrderDetails>.Success(RefundOrderDetails.GetRefundedSuccessfullyDetails(orderIdGuid));
    }
}