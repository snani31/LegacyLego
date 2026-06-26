using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Orders.Commands.Expire;
using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Orders.Commands.Create;

public class CreateOrderDomainEventHandler(ICommandBackgroundJobService jobService)
: IDomainEventHandler<OrderCreated>
{
    public Task HandleAsync(OrderCreated notification, CancellationToken ct)
    {
        jobService.Schedule(new ExpireOrderCommand(notification.OrderId.Value), TimeSpan.FromMinutes(10));
        return Task.CompletedTask;
    }
}