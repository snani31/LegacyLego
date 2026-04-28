using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentCreated(
    OrderPaymentId Paymentid,
    OrderId OrderId,
    DateTime CreatedAt) : IDomainEvent;
