using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentMarkAsFailedTests
{
    [Test]
    public async Task MarkAsFailed_FromPending_ShouldSucceedAndChangeStatus()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var statusBefore = payment.Status;

        var failure = payment.MarkAsFailed();
        var statusAfter = payment.Status;

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(PaymentStatus.Pending);
        await Assert.That(statusAfter).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    public async Task MarkAsFailed_ShouldRaiseOrderPaymentFailedDomainEvent()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentFailed));
    }

    [Test]
    public async Task MarkAsFailed_WithFailedStatus_ShouldResultFailure()
    {
        var payment = OrderPaymentDataFactory.CreateFailedOrderPayment();

        var secondFailure = payment.MarkAsFailed();

        await Assert.That(secondFailure.IsFailure).IsTrue();
        await Assert.That(secondFailure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    public async Task MarkAsFailed_WithSucceededStatus_ShouldResultFailureWithStatusTransitionFailureError()
    {
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment();

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsFailure).IsTrue();
        await Assert.That(failure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    public async Task MarkAsFailed_WithRefundRequestedStatus_ShouldResultFailureWithStatusTransitionFailureError()
    {
        var payment = OrderPaymentDataFactory.CreateRefundRequestedOrderPayment();

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsFailure).IsTrue();
        await Assert.That(failure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    public async Task MarkAsFailed_WithRefundedStatus_ShouldResultFailureWithStatusTransitionFailureError()
    {
        var payment = OrderPaymentDataFactory.CreateRefundedOrderPayment();

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsFailure).IsTrue();
        await Assert.That(failure.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentFailed));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    public async Task MarkAsFailed_ValuesAreSameAfterFailure()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var id = payment.Id;
        var orderId = payment.OrderId;
        var createdAtUtc = payment.CreatedAtUtc;

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(payment)
            .Member(o => o.Id, m => m.IsEqualTo(id))
            .And.Member(o => o.OrderId, m => m.IsEqualTo(orderId))
            .And.Member(o => o.CreatedAtUtc, m => m.IsEqualTo(createdAtUtc));
    }

    [Test]
    public async Task MarkAsFailed_ExternalSessionIsSameAfterFailure()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var session = ExternalSession.Create("id", "url", DateTime.UtcNow.AddMinutes(60)).Value;
        payment.AttachSession(session, DateTime.UtcNow);

        var failure = payment.MarkAsFailed();

        await Assert.That(failure.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
        await Assert.That(payment.ExternalSession).IsEqualTo(session);
    }
}