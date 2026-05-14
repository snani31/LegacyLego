using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentMarkAsRefundRequestedTests
{
    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_FromPending_ShouldSucceedAndChangeStatus(OrderPayment payment)
    {
        var statusBefore = payment.Status;

        var refundRequest = payment.MarkAsRefundRequested("transactionId");
        var statusAfter = payment.Status;

        await Assert.That(refundRequest.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(PaymentStatus.Pending);
        await Assert.That(statusAfter).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_ShouldRaiseOrderPaymentRefundRequestedDomainEvent(OrderPayment payment)
    {
        var success = payment.MarkAsRefundRequested("transactionId");

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefundRequested));
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_AfterSucceededWithSameTransactionId_ShouldResultSuccess(OrderPayment payment)
    {
        var success = payment.MarkAsSucceeded("transactionId");
        payment.ClearDomainEvents();
        var refundRequest = payment.MarkAsRefundRequested("transactionId");

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(refundRequest.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefundRequested));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_AfterSucceededWithDifferentTransactionId_ShouldResultFailureWithWrongTransactionIdExchangeError(OrderPayment payment)
    {
        var success = payment.MarkAsSucceeded("transactionId");
        payment.ClearDomainEvents();
        var refundRequest = payment.MarkAsRefundRequested("different");

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(refundRequest.IsFailure).IsTrue();
        await Assert.That(refundRequest.Error.Code).IsEqualTo(OrderPaymentErrors.WrongTransactionIdExchangeCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefundRequested));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_WithTransactionIdNull_ShouldThrowArgumentNullException(OrderPayment payment)
    {
        var action = () => { payment.MarkAsRefundRequested(null!); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_WithTransactionIdEmpty_ShouldThrowArgumentException(OrderPayment payment)
    {
        var action = () => { payment.MarkAsRefundRequested(String.Empty); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_WhenStatusIsFailure_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var failure = payment.MarkAsFailed();

        var refundRequest = payment.MarkAsRefundRequested("transactionId");

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(refundRequest.IsFailure).IsTrue(); 
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefundRequested));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_WhenStatusIsRefundRequested_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var firstRefundRequest = payment.MarkAsRefundRequested("transactionId");
        payment.ClearDomainEvents();
        var secondRefundRequest = payment.MarkAsRefundRequested("transactionId");

        await Assert.That(firstRefundRequest.IsSuccess).IsTrue();
        await Assert.That(secondRefundRequest.IsFailure).IsTrue();
        await Assert.That(secondRefundRequest.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefundRequested));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_WhenStatusIsRefunded_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var refund = payment.MarkAsRefunded("transactionId");
        payment.ClearDomainEvents();
        var refundRequest = payment.MarkAsRefundRequested("transactionId");

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(refundRequest.IsFailure).IsTrue();
        await Assert.That(refundRequest.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefundRequested));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_ValuesAreSameAfterRefundRequest(OrderPayment payment)
    {
        var id = payment.Id;
        var orderID = payment.OrderId;
        var createdAtUtc = payment.CreatedAtUtc;

        var success = payment.MarkAsRefundRequested("transactionId");

        await Assert.That(success.IsSuccess).IsTrue();

        await Assert.That(payment)
            .Member(o => o.Id, m => m.IsEqualTo(id))
            .And.Member(o => o.OrderId, m => m.IsEqualTo(orderID))
            .And.Member(o => o.CreatedAtUtc, m => m.IsEqualTo(createdAtUtc));
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_TransactionIdIsSameAfterRefundRequest(OrderPayment payment)
    {
        var transactionId = "transactionId";

        var success = payment.MarkAsRefundRequested(transactionId);

        await Assert.That(success.IsSuccess).IsTrue();

        await Assert.That(payment.TransactionId).IsEqualTo(transactionId);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefundRequested_ExternalSessionIsSameAfterRefundRequest(OrderPayment payment)
    {
        var transactionId = "transactionId";
        var session = ExternalSession.Create("id","url",  DateTime.UtcNow.AddMinutes(60)).Value;
        payment.AttachSession(session, DateTime.UtcNow);

        var success = payment.MarkAsRefundRequested(transactionId);

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
        await Assert.That(payment.ExternalSession).IsEqualTo(session);
    }
}