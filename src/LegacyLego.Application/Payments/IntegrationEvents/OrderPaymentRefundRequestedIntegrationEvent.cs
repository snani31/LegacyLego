using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.IntegrationEvents;

public sealed record OrderPaymentRefundRequestedIntegrationEvent(
    OrderPaymentId PaymentId,
    Guid OrderId,
    decimal Amount,
    string Currency,       
    string TransactionId,
    DateTime OccurredOnUtc) : IIntegrationEvent;