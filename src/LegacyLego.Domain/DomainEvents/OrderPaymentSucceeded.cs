using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentSucceeded(
    OrderPaymentId Paymentid,
    OrderId OrderId,
    Price PaidAmount,
    string TransactionId) : IDomainEvent;
