using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Expire;

public sealed record ExpirationOrderDetails
{
    public const string AlreadyExpiredDetailsCode = "Order.Expiretion.AlreadyExpired";
    public const string ExpiredSuccessfullyCode = "Order.Expiretion.ExpiredSuccessfully";
    public const string WrongStatusTransitionCode = "Order.Expiretion.WrongStatusTransition";

    public readonly string Code;
    public readonly Guid OrderId;
    public readonly string Message;
    public readonly string CurrentStatus;
    public readonly bool StateChanged;

    private ExpirationOrderDetails(string Code,
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

    internal static ExpirationOrderDetails GetAlreadyExpiredDetails(Guid orderId)
    {
        return new ExpirationOrderDetails(
            Code: AlreadyExpiredDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already expired",
            CurrentStatus: OrderStatus.Expired.ToString(),
            false);
    }

    internal static ExpirationOrderDetails GetExpiredSuccessfullyDetails(Guid orderId)
    {
        return new ExpirationOrderDetails(
            Code: ExpiredSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully expired",
            CurrentStatus: OrderStatus.Expired.ToString(),
            true);
    }

    internal static ExpirationOrderDetails GetWrongStatusTransitionDetails(Guid orderId, OrderStatus currentStatus)
    {
        return new ExpirationOrderDetails(
            Code: WrongStatusTransitionCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} Has a status:{currentStatus.ToString()} of not suitable for expiration",
            CurrentStatus: currentStatus.ToString(),
            false);
    }
}