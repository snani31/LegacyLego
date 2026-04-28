using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentRefunded(
    OrderPaymentId Paymentid,
    string TransactionId) : IDomainEvent;
