using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentMarkAsFailedTests
{
    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsFailed_FromPending_ShouldSucceedAndChangeStatus(OrderPayment payment)
    {
        var statusBefore = payment.Status;

        var failure = payment.MarkAsFailed();
        var statusAfter = payment.Status;

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(PaymentStatus.Pending);
        await Assert.That(statusAfter).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsFailed_ShouldRaiseOrderPaymentFailedDomainEvent(OrderPayment payment)
    {
        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentFailed));
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsFailed_WithFailedStatus_ShouldResultFailure(OrderPayment payment)
    {
        var firstFailure = payment.MarkAsFailed();
        payment.ClearDomainEvents();
        var secondFailure = payment.MarkAsFailed();

        await Assert.That(firstFailure.IsSuccess).IsTrue();
        await Assert.That(secondFailure.IsFailure).IsTrue();
        await Assert.That(secondFailure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsFailed_WithSuccessedStatus_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var success = payment.MarkAsSucceeded("transactionId");

        var failure = payment.MarkAsFailed();

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(failure.IsFailure).IsTrue();
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(failure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsFailed_WithRefundRequestedStatus_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var refundRequest = payment.MarkAsRefundRequested("transactionId");

        var failure = payment.MarkAsFailed();

        await Assert.That(refundRequest.IsSuccess).IsTrue();
        await Assert.That(failure.IsFailure).IsTrue();
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(failure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsFailed_WithRefundedStatus_ShouldResultFailureWithStatusTransitionFailureError(OrderPayment payment)
    {
        var refundRequest = payment.MarkAsRefundRequested("transactionId");
        var refund = payment.MarkAsRefunded("transactionId");

        var failure = payment.MarkAsFailed();

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(failure.IsFailure).IsTrue();
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(failure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsFailed_ValuesAreSameAfterFailure(OrderPayment payment)
    {
        var id = payment.Id;
        var orderID = payment.OrderId;
        var createdAtUtc = payment.CreatedAtUtc;

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsSuccess).IsTrue();

        await Assert.That(payment)
            .Member(o => o.Id, m => m.IsEqualTo(id))
            .And.Member(o => o.OrderId, m => m.IsEqualTo(orderID))
            .And.Member(o => o.CreatedAtUtc, m => m.IsEqualTo(createdAtUtc));
    }

    [Test]
    [MethodDataSource(typeof(OrderPaymentDataFactory), nameof(CreateDefaultOrderPayment))]
    public async Task MarkAsFailed_ExternalSessionIsSameAfterFailure(OrderPayment payment)
    {
        var session = ExternalSession.Create("id","url",  DateTime.UtcNow.AddMinutes(60)).Value;
        payment.AttachSession(session, DateTime.UtcNow);

        var success = payment.MarkAsFailed();

        await Assert.That(success.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
        await Assert.That(payment.ExternalSession).IsEqualTo(session);
    }
}