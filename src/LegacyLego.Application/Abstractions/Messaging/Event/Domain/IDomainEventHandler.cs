using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging.Event.Domain;

public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public Task HandleAsync(TDomainEvent notification, CancellationToken cancellationToken);
}
