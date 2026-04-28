namespace LegacyLego.Application.Abstractions.Messaging.Event.Integration;

public interface IIntegrationEventPublisher
{
    public Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken ct);
}