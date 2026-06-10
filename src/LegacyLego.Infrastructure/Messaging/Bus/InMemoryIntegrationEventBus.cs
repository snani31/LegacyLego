using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Infrastructure.Messaging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace LegacyLego.Infrastructure.Messaging.Bus;

public sealed class InMemoryIntegrationEventBus : IIntegrationEventBus
{
    private readonly IServiceProvider _serviceProvider;

    private static readonly ConcurrentDictionary<Type, object> WrapperCache = new();

    public InMemoryIntegrationEventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IIntegrationEvent @event, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(@event, nameof(@event));

        var eventType = @event.GetType();

        var wrapper = WrapperCache.GetOrAdd(eventType, type =>
        {
            var concreteWrapperType = typeof(IntegrationEventWrapper<>).MakeGenericType(type);
            return Activator.CreateInstance(concreteWrapperType)!;
        });

        await ((IntegrationEventWrapper)wrapper).HandleAsync(@event, _serviceProvider, ct);
    }
}

file abstract class IntegrationEventWrapper
{
    public abstract Task HandleAsync(IIntegrationEvent @event, IServiceProvider serviceProvider, CancellationToken ct);
}

file sealed class IntegrationEventWrapper<TIntegrationEvent> : IntegrationEventWrapper
    where TIntegrationEvent : IIntegrationEvent
{
    public override async Task HandleAsync(IIntegrationEvent @event, IServiceProvider serviceProvider, CancellationToken ct)
    {
        var consumers = serviceProvider.GetServices<IIntegrationEventConsumer<TIntegrationEvent>>();

        foreach (var consumer in consumers)
        {
            if (consumer is null) continue;

            await consumer.HandleAsync((TIntegrationEvent)@event, ct);
        }
    }
}