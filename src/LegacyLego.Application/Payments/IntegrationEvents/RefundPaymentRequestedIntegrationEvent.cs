using LegacyLego.Application.Abstractions.Messaging.Event.Integration;

namespace LegacyLego.Application.Payments.IntegrationEvents;

public sealed record RefundPaymentRequestedIntegrationEvent(string TransactionId) : IIntegrationEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}