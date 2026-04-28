using LegacyLego.Application.Payments.IntegrationEvents;
using LegacyLego.Domain.DomainEvents;

namespace LegacyLego.Application.Payments.Common;

public static class PaymentIntegrationEventMapper
{
    public static RefundPaymentRequestedIntegrationEvent Map(OrderPaymentRefundRequested domainEvent)
    {
        return new RefundPaymentRequestedIntegrationEvent(
            domainEvent.TransactionId
        );
    }
}