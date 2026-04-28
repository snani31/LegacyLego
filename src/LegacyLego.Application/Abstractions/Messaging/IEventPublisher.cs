using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging;

public interface IEventPublisher
{
    public Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken ct = default)
        where TEvent : IDomainEvent;
}