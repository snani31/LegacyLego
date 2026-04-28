using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentRefundRequested(
    OrderPaymentId Paymentid,
    string TransactionId) : IDomainEvent;