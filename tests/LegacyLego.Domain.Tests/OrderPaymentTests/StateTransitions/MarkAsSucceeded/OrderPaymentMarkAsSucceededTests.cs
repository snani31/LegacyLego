using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentMarkAsSucceededTests
{
    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_FromPending_ShouldSucceedAndChangeStatus(OrderPayment payment)
    {
        var statusBefore = payment.Status;

        var success = payment.MarkAsSucceeded("transactionId");
        var statusAfter = payment.Status;

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(PaymentStatus.Pending);
        await Assert.That(statusAfter).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_ShouldRaiseOrderPaymentSucceededDomainEvent(OrderPayment payment)
    {
        var success = payment.MarkAsSucceeded("transactionId");

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentSucceeded));
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_SucceededAfterSucceededWithSameTransactionId_ShouldResultSuccess(OrderPayment payment)
    {
        var firstSuccess = payment.MarkAsSucceeded("transactionId");
        payment.ClearDomainEvents();
        var secondSuccess = payment.MarkAsSucceeded("transactionId");

        await Assert.That(firstSuccess.IsSuccess).IsTrue();
        await Assert.That(secondSuccess.IsFailure).IsTrue();
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentSucceeded));
        await Assert.That(secondSuccess.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_SucceededAfterSucceededWithDifferentTransactionId_ShouldResultFailureWithWrongTransactionIdExchangeError(OrderPayment payment)
    {
        var firstSuccess = payment.MarkAsSucceeded("transactionId1");
        payment.ClearDomainEvents();
        var secondSuccess = payment.MarkAsSucceeded("transactionId2");

        await Assert.That(firstSuccess.IsSuccess).IsTrue();
        await Assert.That(secondSuccess.IsFailure).IsTrue();
        await Assert.That(secondSuccess.Error.Code).IsEqualTo(OrderPaymentErrors.WrongTransactionIdExchangeCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentSucceeded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_WithTransactionIdNull_ShouldThrowArgumentNullException(OrderPayment payment)
    {
        var action = () => { payment.MarkAsSucceeded(null!); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_WithTransactionIdEmpty_ShouldThrowArgumentException(OrderPayment payment)
    {
        var action = () => { payment.MarkAsSucceeded(String.Empty); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_WhenStatusIsFailure_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var failure = payment.MarkAsFailed();

        var success = payment.MarkAsSucceeded("transactionId");

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(success.IsFailure).IsTrue();
        await Assert.That(payment.DomainEvents).Count().IsEqualTo(2)
            .And.Contains(e => e.GetType() == typeof(OrderPaymentCreated))
            .And.Contains(e => e.GetType() == typeof(OrderPaymentFailed))
            .And.DoesNotContain(e => e.GetType() == typeof(OrderPaymentSucceeded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_WhenStatusIsRefundRequested_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var refundRequest = payment.MarkAsRefundRequested("transactionId");

        var success = payment.MarkAsSucceeded("transactionId");

        await Assert.That(refundRequest.IsSuccess).IsTrue();
        await Assert.That(success.IsFailure).IsTrue();
        await Assert.That(payment.DomainEvents).Count().IsEqualTo(2)
            .And.Contains(e => e.GetType() == typeof(OrderPaymentCreated))
            .And.Contains(e => e.GetType() == typeof(OrderPaymentRefundRequested))
            .And.DoesNotContain(e => e.GetType() == typeof(OrderPaymentSucceeded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_WhenStatusIsRefuned_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var refundRequest = payment.MarkAsRefundRequested("transactionId");
        var refund = payment.MarkAsRefunded("transactionId");

        var success = payment.MarkAsSucceeded("transactionId");

        await Assert.That(refundRequest.IsSuccess).IsTrue();
        await Assert.That(success.IsFailure).IsTrue();
        await Assert.That(payment.DomainEvents).Count().IsEqualTo(3)
            .And.Contains(e => e.GetType() == typeof(OrderPaymentCreated))
            .And.Contains(e => e.GetType() == typeof(OrderPaymentRefundRequested))
            .And.Contains(e => e.GetType() == typeof(OrderPaymentRefunded))
            .And.DoesNotContain(e => e.GetType() == typeof(OrderPaymentSucceeded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_ValuesAreSameAfterSuccess(OrderPayment payment)
    {
        var id = payment.Id;
        var orderID = payment.OrderId;
        var createdAtUtc = payment.CreatedAtUtc;

        var success = payment.MarkAsSucceeded("transactionId");

        await Assert.That(success.IsSuccess).IsTrue();

        await Assert.That(payment)
            .Member(o => o.Id, m => m.IsEqualTo(id))
            .And.Member(o => o.OrderId, m => m.IsEqualTo(orderID))
            .And.Member(o => o.CreatedAtUtc, m => m.IsEqualTo(createdAtUtc));
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_TransactionIdIsSameAfterSuccess(OrderPayment payment)
    {
        var transactionId = "transactionId";

        var success = payment.MarkAsSucceeded(transactionId);

        await Assert.That(success.IsSuccess).IsTrue();

        await Assert.That(payment.TransactionId).IsEqualTo(transactionId);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsSucceeded_ExternalSessionIsSameAfterSuccess(OrderPayment payment)
    {
        var transactionId = "transactionId";
        var session = ExternalSession.Create("id","url",  DateTime.UtcNow.AddMinutes(60)).Value;
        payment.AttachSession(session, DateTime.UtcNow);

        var success = payment.MarkAsSucceeded(transactionId);

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
        await Assert.That(payment.ExternalSession).IsEqualTo(session);
    }
}