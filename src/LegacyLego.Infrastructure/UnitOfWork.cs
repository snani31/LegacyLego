using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Domain.Shared;
using LegacyLego.Infrastructure.Caching.Abstractions;
using LegacyLego.Infrastructure.Context;
using LegacyLego.Infrastructure.Options;
using LegacyLego.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace LegacyLego.Infrastructure;

public sealed class UnitOfWork: IUnitOfWork
{
    private readonly OrderContext _orderContext;
    private readonly TimeProvider _timeProvider;
    private readonly ICacheInvalidator _cacheInvalidator;

    public UnitOfWork(
        OrderContext orderContext,
        TimeProvider timeProvider,
        ICacheInvalidator cacheInvalidator)
    {
        _orderContext = orderContext;
        _timeProvider = timeProvider;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OutboxingDomainEvents();

        var modifiedEntities = GetModifiedEntities();

        // 3. Сохраняем всё в БД в рамках единой транзакции
        var result = await _orderContext.SaveChangesAsync(cancellationToken);

        // 4. Если запись в БД прошла успешно — запускаем конвейер инвалидации
        if (result > 0 && modifiedEntities.Any())
        {
            await _cacheInvalidator.InvalidateAsync(modifiedEntities, cancellationToken);
        }

        return result;
    }

    private List<object> GetModifiedEntities()
    {
        return _orderContext.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(e => e.Entity)
            .ToList();
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
            type: domainEvent.GetType().AssemblyQualifiedName ?? domainEvent.GetType().FullName!,
            content: JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), OutboxSerializerOptions.Options),
            occurredOnUtc: _timeProvider.GetUtcNow().UtcDateTime
        )).ToList();
    }
}