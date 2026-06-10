namespace LegacyLego.Presentation.Mock.Common.Dto.Request;

public sealed record PaymentProviderWebhookRequest(
    string TransactionId,
    Guid OrderId,
    decimal Amount,
    string Currency,
    string Status); // "success", "failed", "refunded"