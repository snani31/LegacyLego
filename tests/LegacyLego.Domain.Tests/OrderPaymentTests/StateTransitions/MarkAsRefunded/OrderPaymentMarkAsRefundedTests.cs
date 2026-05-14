using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentMarkAsRefundedTests
{
    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_FromPending_ShouldSucceedAndChangeStatus(OrderPayment payment)
    {
        var statusBefore = payment.Status;

        var refund = payment.MarkAsRefunded("transactionId");
        var statusAfter = payment.Status;

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(PaymentStatus.Pending);
        await Assert.That(statusAfter).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_ShouldRaiseOrderPaymentRefundedDomainEvent(OrderPayment payment)
    {
        var refund = payment.MarkAsRefunded("transactionId");

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefunded));
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_AfterSucceededWithSameTransactionId_ShouldResultSuccess(OrderPayment payment)
    {
        var success = payment.MarkAsSucceeded("transactionId");
        payment.ClearDomainEvents();
        var refund = payment.MarkAsRefunded("transactionId");

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_AfterSucceededWithDifferentTransactionId_ShouldResultFailureWithWrongTransactionIdExchangeError(OrderPayment payment)
    {
        var success = payment.MarkAsSucceeded("transactionId");
        payment.ClearDomainEvents();
        var refund = payment.MarkAsRefunded("different");

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderPaymentErrors.WrongTransactionIdExchangeCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_WithTransactionIdNull_ShouldThrowArgumentNullException(OrderPayment payment)
    {
        var action = () => { payment.MarkAsRefunded(null!); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_WithTransactionIdEmpty_ShouldThrowArgumentException(OrderPayment payment)
    {
        var action = () => { payment.MarkAsRefunded(String.Empty); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_WithTransactionIdWhiteSpace_ShouldThrowArgumentException(OrderPayment payment)
    {
        var action = () => { payment.MarkAsRefunded(" "); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_WhenStatusIsFailure_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var failure = payment.MarkAsFailed();

        var refund = payment.MarkAsRefunded("transactionId");

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(refund.IsFailure).IsTrue(); 
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_AfterRefundRequestedWithSameTransactionId_ShouldResultSuccess(OrderPayment payment)
    {
        var refundRequest = payment.MarkAsRefundRequested("transactionId");
        payment.ClearDomainEvents();
        var refund = payment.MarkAsRefunded("transactionId");

        await Assert.That(refundRequest.IsSuccess).IsTrue();
        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_AfterRefundRequestedWithDifferentTransactionId_ShouldResultFailureWithWrongTransactionIdExchangeError(OrderPayment payment)
    {
        var refundRequest = payment.MarkAsRefundRequested("transactionId");
        payment.ClearDomainEvents();
        var refund = payment.MarkAsRefunded("different");

        await Assert.That(refundRequest.IsSuccess).IsTrue();
        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderPaymentErrors.WrongTransactionIdExchangeCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_WhenStatusIsRefunded_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var firstRefund = payment.MarkAsRefunded("transactionId");
        payment.ClearDomainEvents();
        var secondRefund = payment.MarkAsRefunded("transactionId");

        await Assert.That(firstRefund.IsSuccess).IsTrue();
        await Assert.That(secondRefund.IsFailure).IsTrue();
        await Assert.That(secondRefund.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_ValuesAreSameAfterRefund(OrderPayment payment)
    {
        var id = payment.Id;
        var orderID = payment.OrderId;
        var createdAtUtc = payment.CreatedAtUtc;

        var refund = payment.MarkAsRefunded("transactionId");

        await Assert.That(refund.IsSuccess).IsTrue();

        await Assert.That(payment)
            .Member(o => o.Id, m => m.IsEqualTo(id))
            .And.Member(o => o.OrderId, m => m.IsEqualTo(orderID))
            .And.Member(o => o.CreatedAtUtc, m => m.IsEqualTo(createdAtUtc));
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_TransactionIdIsSameAfterRefund(OrderPayment payment)
    {
        var transactionId = "transactionId";

        var refund = payment.MarkAsRefunded(transactionId);

        await Assert.That(refund.IsSuccess).IsTrue();

        await Assert.That(payment.TransactionId).IsEqualTo(transactionId);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsRefunded_ExternalSessionIsSameAfterRefund(OrderPayment payment)
    {
        var session = ExternalSession.Create("id", "url",  DateTime.UtcNow.AddMinutes(60)).Value;
        payment.AttachSession(session, DateTime.UtcNow);

        var refund = payment.MarkAsRefunded("transactionId");

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
        await Assert.That(payment.ExternalSession).IsEqualTo(session);
    }
}