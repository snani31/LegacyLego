namespace LegacyLego.Application.Abstractions.Messaging.Event.Integration;

public interface IIntegrationEvent
{
    public DateTime OccurredOnUtc { get; }
}