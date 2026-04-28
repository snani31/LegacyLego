using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed class CancelOrderCommandHandler(
IOrderRepository orderRepository,
IUnitOfWork unitOfWork) : ICommandHandler<CancelOrderCommand, CancelletionOrderDetails>
{
    public async Task<Result<CancelletionOrderDetails>> HandleAsync(CancelOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<CancelletionOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Cancel();
        if (result.IsFailure && order.Status is OrderStatus.Cancelled)
            return Result<CancelletionOrderDetails>.Success(CancelletionOrderDetails.GetAlreadyCancelledDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<CancelletionOrderDetails>.Success(CancelletionOrderDetails.GetWrongStatusTransitionDetails(orderIdGuid, order.Status));

        await unitOfWork.SaveChangesAsync(ct);
        return Result<CancelletionOrderDetails>.Success(CancelletionOrderDetails.GetCancelledSuccessfullyDetails(orderIdGuid));
    }
}