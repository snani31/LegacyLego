namespace LegacyLego.Presentation.Mock.Common.Dto.Request;

public sealed record PaymentProviderWebhookRequest(
    string ExternalSessionId,
    string? TransactionId, // Nullable! Может быть null, если транзакция не создалась
    Guid OrderId,
    decimal Amount,
    string Currency,
    string Status); // "success", "failed", "refunded"