namespace LegacyLego.Application.Payments.Common;

public sealed record PaymentSession(
    Guid PaymentId,
    string ExternalSessionId,
    string CheckoutUrl,
    DateTime ExpiresAtUtc);