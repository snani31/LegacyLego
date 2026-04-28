using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Aggregates;

public class OrderPayment : AggregateRoot<OrderPaymentId>
{
    public string? TransactionId { get; private set; }

    public OrderId OrderId { get; }

    public PaymentStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public ExternalSession? ExternalSession { get; private set; }

    public bool HasSession => ExternalSession is not null;

    public bool IsRefundRequested => Status is PaymentStatus.RefundRequested;

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

    public static Result<OrderPayment> Create(OrderId orderId)
    {
        var createdAt = DateTime.UtcNow;
        var status = PaymentStatus.Pending;
        var id = OrderPaymentId.New();

        var payment = new OrderPayment(id, orderId, createdAt, status);

        payment.Raise(new OrderPaymentCreated(id, orderId, createdAt));

        return Result<OrderPayment>.Success(payment);
    }

    public Result AttachSession(ExternalSession externalSession)
    {
        ExternalSession = externalSession;

        return Result.Success();
    }

    public Result MarkAsSucceeded(string transactionId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(transactionId);

        if (Status == PaymentStatus.Succeeded && TransactionId == transactionId)
            return Result.Success();

        if (TransactionId != null && TransactionId != transactionId)
            return Result.Failure(OrderPaymentErrors.GetWrongTransactionIdExchangeError(TransactionId, transactionId));

        var paymentAction = PaymentAction.Success;
        var nextStatus = PaymentStatus.Succeeded;

        if (Status is not PaymentStatus.Pending)
            return Result.Failure(OrderPaymentErrors.GetStatusTransitionFailureError(paymentAction, Status, nextStatus));

        Status = nextStatus;
        TransactionId = transactionId;

        base.Raise(new OrderPaymentSucceeded(Id, OrderId, TransactionId!));

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

    public Result MarkAsRefundRequested()
    {
        var paymentAction = PaymentAction.RefundRequest;
        var nextStatus = PaymentStatus.RefundRequested;

        if (Status is not PaymentStatus.Succeeded)
            return Result.Failure(OrderPaymentErrors.GetStatusTransitionFailureError(paymentAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderPaymentRefundRequested(Id, TransactionId!));

        return Result.Success();
    }

    public Result MarkAsRefunded(string transactionId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(transactionId);

        if (Status == PaymentStatus.Refunded && TransactionId == transactionId)
            return Result.Success();

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