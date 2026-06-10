using LegacyLego.Application.Abstractions.Messaging.Event.Integration;

namespace LegacyLego.Infrastructure.Messaging.Abstractions;

public interface IIntegrationEventBus
{
    public Task DispatchAsync(
        IIntegrationEvent @event,
        CancellationToken ct = default);
}