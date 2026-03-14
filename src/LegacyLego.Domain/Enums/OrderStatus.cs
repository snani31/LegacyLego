namespace LegacyLego.Domain.Enums;

public enum OrderStatus : byte
{
    PendingPayment,
    Paid,
    Cancelled,
    Expired,
    Refunded
}