namespace LegacyLego.Domain.Enums;

public enum PaymentStatus : byte
{
    Pending,
    Succeeded,
    Failed,
    Refunded,
    RefundRequested
}