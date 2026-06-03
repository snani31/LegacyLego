using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Context;
using LegacyLego.Infrastructure.Outbox;
using System.Text.Json;

namespace LegacyLego.Infrastructure;

public sealed class UnitOfWork: IUnitOfWork
{
    private readonly OrderContext _orderContext;
    private readonly TimeProvider _timeProvider;

    public UnitOfWork(OrderContext orderContext, TimeProvider timeProvider)
    {
        _orderContext = orderContext;
        _timeProvider = timeProvider;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OutboxingDomainEvents();

        return await _orderContext.SaveChangesAsync(cancellationToken);
    }

    private void OutboxingDomainEvents()
    {
        var entitiesWithEvents = GetAllWithDomainEvents();

        var domainEvents = TakeDomainEvents(entitiesWithEvents);

        foreach (var entity in entitiesWithEvents)
            entity.ClearDomainEvents();

        var outboxMessages = ConvertDomainEventsToOutboxMessages(domainEvents);

        _orderContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }

    private List<IHasDomainEvents> GetAllWithDomainEvents()
    {
        return _orderContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(x => x.Entity)
            .Where(entity => entity.DomainEvents.Any())
            .ToList();
    }

    private List<IDomainEvent> TakeDomainEvents(List<IHasDomainEvents> hasEventsList)
    {
        return hasEventsList
            .SelectMany(entity => entity.DomainEvents)
            .ToList();
    }

    private List<OutboxMessage> ConvertDomainEventsToOutboxMessages(List<IDomainEvent> domainEvents)
    {
        return domainEvents.Select(domainEvent => new OutboxMessage(
            id: Guid.NewGuid(),
            type: domainEvent.GetType().Name,
            content: JsonSerializer.Serialize(domainEvent, domainEvent.GetType()),
            occurredOnUtc: _timeProvider.GetUtcNow().UtcDateTime
        )).ToList();
    }
}