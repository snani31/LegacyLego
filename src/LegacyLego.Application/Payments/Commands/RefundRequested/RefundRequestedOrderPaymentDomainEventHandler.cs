using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Application.Payments.IntegrationEvents;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.DomainEvents;

namespace LegacyLego.Application.Orders.Commands.Create;

public class RefundRequestedOrderPaymentDomainEventHandler(
    TimeProvider timeProvider,
    IOrderRepository orderRepository,
    IIntegrationEventPublisher eventPublisher)
: IDomainEventHandler<OrderPaymentRefundRequested>
{
    public async Task HandleAsync(OrderPaymentRefundRequested notification, CancellationToken ct)
    {
        // TODO Уязвимость ! По логике ЖЦ у нас ТОЧНО есть этот order, но даже так не проверять его на null - катастрофа
        // исправить позже Dead Letter Queue
        var order = await orderRepository.GetByIdAsync(notification.OrderId, ct);

        var @event = new OrderPaymentRefundRequestedIntegrationEvent(
            PaymentId: notification.Paymentid,
            OrderId: notification.OrderId.Value,
            Amount: order!.TotalPrice.Sum,
            Currency: order.Currency.Code,
            TransactionId: notification.TransactionId,
            OccurredOnUtc: timeProvider.GetUtcNow().UtcDateTime);

        await eventPublisher.PublishAsync(@event, ct);
    }
}