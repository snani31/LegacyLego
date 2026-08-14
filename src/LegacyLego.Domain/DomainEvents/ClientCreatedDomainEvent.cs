using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record ClientCreatedDomainEvent(
    ClientId ClientId,
    string Email,
    DateTime OccurredOnUtc) : IDomainEvent;