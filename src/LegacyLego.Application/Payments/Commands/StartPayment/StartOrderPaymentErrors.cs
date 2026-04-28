using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public static class StartOrderPaymentErrors
{
    public const string OrderIsNotInPendingPaymentCode = "StartOrderPatyment.OrderIsNotInPendingPayment";
    public const string ForOrderIsAlreadyExistsSuccessedPaymentCode = "StartOrderPatyment.ForOrderIsAlreadyExistsSuccessedPayment";
    public const string CanNotFindPendingPaymentAfterCheckConstraintCode = "StartOrderPatyment.CanNotFindPendingPaymentAfterCheckConstraint";

    public static Error GetOrderIsNotInPendingPaymentError(Guid orderId, OrderStatus status)
    {
        return new(
            Code: OrderIsNotInPendingPaymentCode,
            Message: $"The order being processed with OrderId: {orderId} is not waiting payment. Its in {status} status now");
    }

    public static Error GetForOrderIsAlreadyExistsSuccessedPaymentError(Guid orderId)
    {
        return new(
            Code: ForOrderIsAlreadyExistsSuccessedPaymentCode,
            Message: $"For order being processed with OrderId: {orderId} is already exists successed payment");
    }

    public static Error GetCanNotFindPendingPaymentAfterCheckConstraintError(Guid orderId)
    {
        return new(
            Code: CanNotFindPendingPaymentAfterCheckConstraintCode,
            Message: $"For order being processed with OrderId: {orderId} can not find existing pending payment after ConstraintCheck");
    }
}