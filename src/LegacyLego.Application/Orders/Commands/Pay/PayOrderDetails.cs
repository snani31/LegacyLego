using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed record PayOrderDetails
{
    public const string AlreadyPaidDetailsCode = "Order.Payment.AlreadyPaid";
    public const string PaidSuccessfullyCode = "Order.Payment.PaidSuccessfully";

    public readonly string Code;
    public readonly Guid OrderId;
    public readonly string Message;
    public readonly string CurrentStatus;
    public readonly bool StateChanged;

    private PayOrderDetails(string Code,
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

    internal static PayOrderDetails GetAlreadyPaidDetails(Guid orderId)
    {
        return new PayOrderDetails(
            Code: AlreadyPaidDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already paid",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            false);
    }

    internal static PayOrderDetails GetPaidSuccessfullyDetails(Guid orderId)
    {
        return new PayOrderDetails(
            Code: PaidSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully paid",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            true);
    }
}