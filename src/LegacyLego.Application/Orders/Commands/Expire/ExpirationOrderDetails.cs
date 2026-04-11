using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Expire;

public sealed record ExpirationOrderDetails
{
    public const string AlreadyExpiredDetailsCode = "Order.Expiretion.AlreadyExpired";
    public const string ExpiredSuccessfullyCode = "Order.Expiretion.ExpiredSuccessfully";
    public const string WrongStatusTransitionCode = "Order.Expiretion.WrongStatusTransition";
    
    private readonly string code;
    private readonly Guid orderId;
    private readonly string message;
    private readonly string currentStatus;
    private readonly bool stateChanged;

    private ExpirationOrderDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        code = Code;
        orderId = OrderId;
        message = Message;
        currentStatus = CurrentStatus;
        stateChanged = StateChanged;
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