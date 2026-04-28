using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.ExceptionalErrors;
using LegacyLego.Application.Exceptions;
using LegacyLego.Application.Orders.Commands.Cancel;
using LegacyLego.Application.Orders.Commands.Pay;
using LegacyLego.Application.Orders.Commands.Refund;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Pay;

public sealed class PayOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<PayOrderCommand, PayOrderDetails>
{
    public async Task<Result<PayOrderDetails>> HandleAsync(PayOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<PayOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Pay();
        if (result.IsFailure && order.Status is OrderStatus.Paid)
            return Result<PayOrderDetails>.Success(PayOrderDetails.GetAlreadyPaidDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<PayOrderDetails>.Success(PayOrderDetails.GetWrongStatusTransitionDetails(orderIdGuid, order.Status));

        await unitOfWork.SaveChangesAsync(ct);
        return Result<PayOrderDetails>.Success(PayOrderDetails.GetPaidSuccessfullyDetails(orderIdGuid));
    }

}