using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Aggregates;

public class OrderPayment : AggregateRoot<OrderPaymentId>
{
    public string? TransactionId { get; private set; }

    public OrderId OrderId { get; }

    public PaymentStatus Status { get; private set; }

    public Price ExpectedAmount { get; }

    public Price? ActualAmount { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public ExternalSession? ExternalSession { get; private set; }

    public bool HasSession => ExternalSession is not null;

    public bool IsRefundRequested => Status is PaymentStatus.RefundRequested;

    private OrderPayment(
        OrderPaymentId id,
        OrderId orderId,
        DateTime createdAtUtc,
        PaymentStatus status,
        Price expectedAmount) : base(id)
    {
        OrderId = orderId;
        Status = status;
        ExpectedAmount = expectedAmount;
        CreatedAtUtc = createdAtUtc;
    }
    /// <summary>
    /// Для материализации ORM
    /// </summary>
    /// <param name="id"></param>
    /// <param name="orderId"></param>
    /// <param name="createdAtUtc"></param>
    /// <param name="status"></param>
    private OrderPayment(
    OrderPaymentId id,
    OrderId orderId,
    DateTime createdAtUtc,
    PaymentStatus status) : base(id)
    {
        OrderId = orderId;
        Status = status;
        CreatedAtUtc = createdAtUtc;
    }

    public static Result<OrderPayment> Create(OrderId orderId, Price expectedAmount, DateTime createdAt)
    {
        ArgumentNullException.ThrowIfNull(orderId, nameof(orderId));
        ArgumentNullException.ThrowIfNull(expectedAmount, nameof(expectedAmount));

        if (createdAt == default) throw new ArgumentException("Date must be provided.", nameof(createdAt));
        if (createdAt.Kind is not DateTimeKind.Utc)
            return Result<OrderPayment>.Failure(
                OrderPaymentErrors.GetCreationTimeWasNotUtcError(createdAt.Kind));

        var status = PaymentStatus.Pending;
        var id = OrderPaymentId.New();

        var payment = new OrderPayment(id, orderId, createdAt, status, expectedAmount);

        payment.Raise(new OrderPaymentCreated(id, orderId, expectedAmount, createdAt));

        return Result<OrderPayment>.Success(payment);
    }

    public Result AttachSession(ExternalSession newSession, DateTime nowUtc)
    {
        ArgumentNullException.ThrowIfNull(newSession, nameof(newSession));

        if (nowUtc.Kind is not DateTimeKind.Utc)
            return Result.Failure(
                OrderPaymentErrors.GetNowTimeWasNotUtcForAttachSessionError(nowUtc.Kind));

        if (HasSession && !ExternalSession!.IsExpired(nowUtc))
            return Result.Failure(
                OrderPaymentErrors.GetEnsuredSessionIsNotExpiredTransitionFailureError(
                    ExternalSession.ExternalId,
                    newSession.ExternalId,
                    Id));

        if (Status is not PaymentStatus.Pending)
            return Result.Failure(
                OrderPaymentErrors.GetWrongStatusForExternalSessionTransitionError(Id,
                    Status,
                    newSession.ExternalId));

        ExternalSession = newSession;

        return Result.Success();
    }

    public Result RegisterPaymentReceipt(string transactionId, Price actualAmount)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(transactionId);
        ArgumentNullException.ThrowIfNull(actualAmount);

        if (TransactionId != null && TransactionId != transactionId)
            return Result.Failure(
                OrderPaymentErrors.GetWrongTransactionIdExchangeError(TransactionId, transactionId));

        if (Status is not PaymentStatus.Pending)
            return Result.Failure(
                OrderPaymentErrors.GetStatusTransitionFailureError(PaymentAction.Success, Status, PaymentStatus.Succeeded));

        TransactionId = transactionId;
        ActualAmount = actualAmount;

        if (ActualAmount != ExpectedAmount)
        {
            Status = PaymentStatus.RefundRequested;

            base.Raise(new OrderPaymentAmountMismatchedAndRefundRequested(
                Id,
                OrderId,
                ExpectedAmount,
                ActualAmount,
                TransactionId));

            return Result.Success();
        }

        Status = PaymentStatus.Succeeded;
        base.Raise(new OrderPaymentSucceeded(Id, OrderId, ActualAmount, TransactionId));

        return Result.Success();
    }

    public Result MarkAsFailed()
    {
        var paymentAction = PaymentAction.Fail;
        var nextStatus = PaymentStatus.Failed;

        if (Status is not PaymentStatus.Pending)
            return Result.Failure(OrderPaymentErrors.GetStatusTransitionFailureError(paymentAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderPaymentFailed(Id));

        return Result.Success();
    }

    public Result MarkAsRefunded(string transactionId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(transactionId);

        if (TransactionId != null && TransactionId != transactionId)
            return Result.Failure(OrderPaymentErrors.GetWrongTransactionIdExchangeError(TransactionId, transactionId));

        var paymentAction = PaymentAction.Refund;
        var nextStatus = PaymentStatus.Refunded;

        if (Status is not PaymentStatus.RefundRequested
            && Status is not PaymentStatus.Succeeded
            && Status is not PaymentStatus.Pending)
        {
            return Result.Failure(
                OrderPaymentErrors.GetStatusTransitionFailureError(
                    paymentAction, Status, nextStatus));
        }

        TransactionId ??= transactionId;
        Status = nextStatus;

        base.Raise(new OrderPaymentRefunded(Id, TransactionId!));

        return Result.Success();
    }
}