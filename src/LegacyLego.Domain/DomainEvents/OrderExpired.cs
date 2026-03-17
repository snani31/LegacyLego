using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderExpired(
    OrderId OrderId,
    DateTime ExpiredAt) : IDomainEvent;
