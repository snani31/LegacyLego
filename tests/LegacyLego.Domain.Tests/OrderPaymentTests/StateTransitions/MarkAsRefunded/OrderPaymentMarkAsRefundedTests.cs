using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentMarkAsRefundedTests
{
    private const string DefaultTxId = "tx-123";
    private const string DifferentTxId = "tx-different-456";

    #region Happy Paths (Valid Transitions)

    [Test]
    public async Task MarkAsRefunded_FromPending_ShouldSucceedAndChangeStatus()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var statusBefore = payment.Status;

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(PaymentStatus.Pending);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    public async Task MarkAsRefunded_ShouldRaiseOrderPaymentRefundedDomainEvent()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefunded));
    }

    [Test]
    public async Task MarkAsRefunded_AfterSucceededWithSameTransactionId_ShouldResultSuccess()
    {
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment(txId: DefaultTxId);

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    public async Task MarkAsRefunded_AfterRefundRequestedWithSameTransactionId_ShouldResultSuccess()
    {
        var payment = OrderPaymentDataFactory.CreateRefundRequestedOrderPayment(txId: DefaultTxId);

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    #endregion

    #region Invalid Transitions & TransactionId Mismatch

    [Test]
    public async Task MarkAsRefunded_AfterSucceededWithDifferentTransactionId_ShouldResultFailureWithWrongTransactionIdExchangeError()
    {
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment(txId: DefaultTxId);

        var refund = payment.MarkAsRefunded(DifferentTxId);

        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderPaymentErrors.WrongTransactionIdExchangeCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    public async Task MarkAsRefunded_AfterRefundRequestedWithDifferentTransactionId_ShouldResultFailureWithWrongTransactionIdExchangeError()
    {
        var payment = OrderPaymentDataFactory.CreateRefundRequestedOrderPayment(txId: DefaultTxId);

        var refund = payment.MarkAsRefunded(DifferentTxId);

        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderPaymentErrors.WrongTransactionIdExchangeCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    public async Task MarkAsRefunded_WhenStatusIsFailed_ShouldResultFailureWithStatusTransitionFailureError()
    {
        // Arrange
        var payment = OrderPaymentDataFactory.CreateFailedOrderPayment();

        // Act
        var refund = payment.MarkAsRefunded(DefaultTxId);

        // Assert
        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Failed);
    }

    [Test]
    public async Task MarkAsRefunded_WhenStatusIsRefunded_ShouldResultFailureWithStatusTransitionFailureError()
    {
        var payment = OrderPaymentDataFactory.CreateRefundedOrderPayment(txId: DefaultTxId);

        var secondRefund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(secondRefund.IsFailure).IsTrue();
        await Assert.That(secondRefund.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaymentRefunded));
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    #endregion

    #region Guard Clauses

    [Test]
    public async Task MarkAsRefunded_WithTransactionIdNull_ShouldThrowArgumentNullException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var action = () => { _ = payment.MarkAsRefunded(null!); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task MarkAsRefunded_WithTransactionIdEmpty_ShouldThrowArgumentException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var action = () => { _ = payment.MarkAsRefunded(string.Empty); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task MarkAsRefunded_WithTransactionIdWhiteSpace_ShouldThrowArgumentException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var action = () => { _ = payment.MarkAsRefunded("   "); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    #endregion

    #region State Invariance Checks

    [Test]
    public async Task MarkAsRefunded_ValuesAreSameAfterRefund()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var id = payment.Id;
        var orderId = payment.OrderId;
        var createdAtUtc = payment.CreatedAtUtc;

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment)
            .Member(o => o.Id, m => m.IsEqualTo(id))
            .And.Member(o => o.OrderId, m => m.IsEqualTo(orderId))
            .And.Member(o => o.CreatedAtUtc, m => m.IsEqualTo(createdAtUtc));
    }

    [Test]
    public async Task MarkAsRefunded_TransactionIdIsSameAfterRefund()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.TransactionId).IsEqualTo(DefaultTxId);
    }

    [Test]
    public async Task MarkAsRefunded_ExternalSessionIsSameAfterRefund()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var session = ExternalSession.Create("session-id", "url", DateTime.UtcNow.AddMinutes(60)).Value;
        payment.AttachSession(session, DateTime.UtcNow);

        var refund = payment.MarkAsRefunded(DefaultTxId);

        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
        await Assert.That(payment.ExternalSession).IsEqualTo(session);
    }

    #endregion
}