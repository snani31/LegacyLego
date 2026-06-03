using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Domain.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace LegacyLego.Infrastructure.Messaging;

public sealed class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    private static readonly ConcurrentDictionary<Type, object> WrapperCache = new();

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(domainEvents, nameof(domainEvents));

        foreach (var domainEvent in domainEvents)
        {
            if (domainEvent is null) continue;

            var domainEventType = domainEvent.GetType();

            var wrapper = WrapperCache.GetOrAdd(domainEventType, type =>
            {
                var concreteWrapperType = typeof(DomainEventWrapper<>).MakeGenericType(type);
                return Activator.CreateInstance(concreteWrapperType)!;
            });

            await ((DomainEventWrapper)wrapper).HandleAsync(domainEvent, serviceProvider, ct);
        }
    }
}

file abstract class DomainEventWrapper
{
    public abstract Task HandleAsync(IDomainEvent domainEvent, IServiceProvider provider, CancellationToken ct);
}

file sealed class DomainEventWrapper<TDomainEvent> : DomainEventWrapper
    where TDomainEvent : IDomainEvent
{
    public override async Task HandleAsync(IDomainEvent domainEvent, IServiceProvider provider, CancellationToken ct)
    {
        var handlers = provider.GetServices<IDomainEventHandler<TDomainEvent>>();

        foreach (var handler in handlers)
        {
            if (handler is null) continue;

            await handler.HandleAsync((TDomainEvent)domainEvent, ct);
        }
    }
}