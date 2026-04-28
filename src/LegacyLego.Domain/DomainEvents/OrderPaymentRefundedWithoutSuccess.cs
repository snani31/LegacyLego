using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentRefundedWithoutSuccess(
    OrderPaymentId Paymentid,
    string TransactionId) : IDomainEvent;
