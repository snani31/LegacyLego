using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Refund;

public sealed record RefundOrderDetails : ICustomLogSeverity
{
    public const string AlreadyRefundedDetailsCode = "Order.Refund.AlreadyRefunded";
    public const string RefundedSuccessfullyCode = "Order.Refund.RefundedSuccessfully";

    public string Code { get; }
    public Guid OrderId { get; }
    public string Message { get; }
    public string CurrentStatus { get; }
    public bool StateChanged { get; }

    public bool IsWarning => Code switch
    {
        AlreadyRefundedDetailsCode => true,
        RefundedSuccessfullyCode => false,
        _ => false
    };

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
}