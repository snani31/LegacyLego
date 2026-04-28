using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Orders.Commands.Expire;
using LegacyLego.Application.Orders.Commands.Pay;
using LegacyLego.Domain.DomainEvents;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public class OrderPaymentSucceededDomainEventHandler(ICommandDispatcher dispatcher)
: IDomainEventHandler<OrderPaymentSucceeded>
{
    public async Task HandleAsync(OrderPaymentSucceeded notification, CancellationToken ct)
    {
        var command = new PayOrderCommand(notification.OrderId.Value);

        await dispatcher.DispatchAsync(command, ct);
    }
}