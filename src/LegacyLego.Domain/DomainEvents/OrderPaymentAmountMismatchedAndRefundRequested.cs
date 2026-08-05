using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentAmountMismatchedAndRefundRequested(
    OrderPaymentId Paymentid,
    OrderId OrderId,
    Price ExpectedAmount,
    Price ActualAmount,
    string TransactionId) : IDomainEvent;