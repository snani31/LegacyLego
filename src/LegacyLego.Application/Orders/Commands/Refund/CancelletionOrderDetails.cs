using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Refund;

public sealed record RefundOrderDetails
{
    public const string AlreadyRefundedDetailsCode = "Order.Refund.AlreadyRefunded";
    public const string RefundedSuccessfullyCode = "Order.Refund.RefundedSuccessfully";
    public const string WrongStatusTransitionCode = "Order.Refund.WrongStatusTransition";

    public readonly string Code;
    public readonly Guid OrderId;
    public readonly string Message;
    public readonly string CurrentStatus;
    public readonly bool StateChanged;

    private RefundOrderDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static RefundOrderDetails GetAlreadyRefundedDetails(Guid orderId)
    {
        return new RefundOrderDetails(
            Code: AlreadyRefundedDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already refunded",
            CurrentStatus: OrderStatus.Refunded.ToString(),
            false);
    }

    internal static RefundOrderDetails GetRefundedSuccessfullyDetails(Guid orderId)
    {
        return new RefundOrderDetails(
            Code: RefundedSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully refunded",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            true);
    }

    internal static RefundOrderDetails GetWrongStatusTransitionDetails(Guid orderId, OrderStatus currentStatus)
    {
        return new RefundOrderDetails(
            Code: WrongStatusTransitionCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} Has a status:{currentStatus.ToString()} of not suitable for refund",
            CurrentStatus: currentStatus.ToString(),
            false);
    }
}