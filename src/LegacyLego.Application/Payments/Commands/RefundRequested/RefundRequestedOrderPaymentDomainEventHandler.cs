using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.DomainEvents;

namespace LegacyLego.Application.Orders.Commands.Create;

public class RefundRequestedOrderPaymentDomainEventHandler(IIntegrationEventPublisher eventPublisher)
: IDomainEventHandler<OrderPaymentRefundRequested>
{
    public async Task HandleAsync(OrderPaymentRefundRequested notification, CancellationToken ct)
    {
        var ivent = PaymentIntegrationEventMapper.Map(notification);
        await eventPublisher.PublishAsync(ivent, ct);
    }
}