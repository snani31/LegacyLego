using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Payments.Common;

public record PaymentWebhook(
    string ExternalSessionId,
    string? TransactionId,
    Guid OrderId,
    decimal Amount,
    string Currency,
    PaymentStatus Status);