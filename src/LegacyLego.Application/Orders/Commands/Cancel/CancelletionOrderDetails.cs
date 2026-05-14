using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed record CancelletionOrderDetails
{
    public const string AlreadyCancelledDetailsCode = "Order.Cancelletion.AlreadyCancelled";
    public const string CancelledSuccessfullyCode = "Order.Cancelletion.CancelledSuccessfully";

    public readonly string Code;
    public readonly Guid OrderId;
    public readonly string Message;
    public readonly string CurrentStatus;
    public readonly bool StateChanged;

    private CancelletionOrderDetails(string Code,
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

    internal static CancelletionOrderDetails GetAlreadyCancelledDetails(Guid orderId)
    {
        return new CancelletionOrderDetails(
            Code: AlreadyCancelledDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already cancelled",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            false);
    }

    internal static CancelletionOrderDetails GetCancelledSuccessfullyDetails(Guid orderId)
    {
        return new CancelletionOrderDetails(
            Code: CancelledSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully cancelled",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            true);
    }
}