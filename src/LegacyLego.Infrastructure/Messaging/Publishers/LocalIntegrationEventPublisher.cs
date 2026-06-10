using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Infrastructure.Messaging.Abstractions;

namespace LegacyLego.Infrastructure.Messaging.Publishers;

public sealed class LocalIntegrationEventPublisher : IIntegrationEventPublisher
{
    private readonly IIntegrationEventBus _bus;

    public LocalIntegrationEventPublisher(IIntegrationEventBus bus) => _bus = bus;

    public Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken ct)
        => _bus.DispatchAsync(integrationEvent, ct);
}