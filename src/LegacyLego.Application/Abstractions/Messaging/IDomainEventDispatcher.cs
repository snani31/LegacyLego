using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging;

public interface IDomainEventDispatcher
{
    public Task DispatchAsync(
        IDomainEvent domainEvents,
        CancellationToken ct = default);
}