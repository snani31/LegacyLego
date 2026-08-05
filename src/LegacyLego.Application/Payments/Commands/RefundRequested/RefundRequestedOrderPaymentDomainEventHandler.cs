using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Application.Payments.IntegrationEvents;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.DomainEvents;

namespace LegacyLego.Application.Orders.Commands.Create;

public class RefundRequestedOrderPaymentDomainEventHandler(
    TimeProvider timeProvider,
    IIntegrationEventPublisher eventPublisher)
: IDomainEventHandler<OrderPaymentAmountMismatchedAndRefundRequested>
{
    public async Task HandleAsync(OrderPaymentAmountMismatchedAndRefundRequested notification, CancellationToken ct)
    {
        var @event = new OrderPaymentRefundRequestedIntegrationEvent(
            PaymentId: notification.Paymentid,
            OrderId: notification.OrderId.Value,
            Amount: notification.ActualAmount.Sum,
            Currency: notification.ActualAmount.Currency.Code,
            TransactionId: notification.TransactionId,
            OccurredOnUtc: timeProvider.GetUtcNow().UtcDateTime);

        await eventPublisher.PublishAsync(@event, ct);
    }
}