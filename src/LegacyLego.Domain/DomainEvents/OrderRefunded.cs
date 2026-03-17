using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderRefunded(
    OrderId OrderId,
    DateTime RefundedAt) : IDomainEvent;
