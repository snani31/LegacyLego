using LegacyLego.Application.Abstractions.Messaging.Event.Integration;

namespace LegacyLego.Infrastructure.Messaging.Abstractions;

public interface IIntegrationEventConsumer<in TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    public Task HandleAsync(TIntegrationEvent notification, CancellationToken ct);
}