using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderCreated(
    OrderId OrderId,
    Guid ClientId,
    DateTime CreatedAt) : IDomainEvent;
