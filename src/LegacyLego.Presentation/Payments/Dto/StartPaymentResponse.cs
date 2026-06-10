namespace LegacyLego.Presentation.Payments.Dto;

public sealed record StartPaymentResponse(
    string CheckoutUrl,
    DateTime ExpiresAtUtc);