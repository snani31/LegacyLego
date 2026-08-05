using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentRegisterPaymentReceiptTests
{
    private const string DefaultTxId = "tx-123";
    private const string DifferentTxId = "tx-different-456";

    #region Happy Paths (Valid Transitions)

    [Test]
    public async Task RegisterPaymentReceipt_WhenAmountMatches_ShouldSetStatusToSucceededAndRaiseSucceededEvent()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment(100m);
        var exactPrice = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, exactPrice);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
        await Assert.That(payment.ActualAmount).IsEqualTo(exactPrice);
        await Assert.That(payment.TransactionId).IsEqualTo(DefaultTxId);
        await Assert.That(payment.DomainEvents)
            .HasSingleItem(e => e.GetType() == typeof(OrderPaymentSucceeded));
    }

    [Test]
    public async Task RegisterPaymentReceipt_WhenAmountMismatches_ShouldSetStatusToRefundRequestedAndRaiseMismatchedEvent()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment(amount: 100m);
        var mismatchedPrice = PriceDataFactory.CreatePrice(150m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, mismatchedPrice);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
        await Assert.That(payment.ActualAmount).IsEqualTo(mismatchedPrice);
        await Assert.That(payment.TransactionId).IsEqualTo(DefaultTxId);
        await Assert.That(payment.DomainEvents)
            .HasSingleItem(e => e.GetType() == typeof(OrderPaymentAmountMismatchedAndRefundRequested));
    }

    #endregion

    #region Invalid Status Transitions

    [Test]
    public async Task RegisterPaymentReceipt_WhenStatusIsSucceeded_ShouldReturnStatusTransitionFailure()
    {
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, price);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Succeeded);
    }

    [Test]
    public async Task RegisterPaymentReceipt_WhenStatusIsRefundRequested_ShouldReturnStatusTransitionFailure()
    {
        var payment = OrderPaymentDataFactory.CreateRefundRequestedOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, price);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.RefundRequested);
    }

    [Test]
    public async Task RegisterPaymentReceipt_WhenStatusIsRefunded_ShouldReturnStatusTransitionFailure()
    {
        var payment = OrderPaymentDataFactory.CreateRefundedOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, price);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Refunded);
    }

    [Test]
    public async Task RegisterPaymentReceipt_WhenStatusIsFailed_ShouldReturnStatusTransitionFailure()
    {
        var payment = OrderPaymentDataFactory.CreateFailedOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, price);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(OrderPaymentErrors.StatusTransitionFailureCode);
        await Assert.That(payment.Status).IsEqualTo(PaymentStatus.Failed);
    }

    #endregion

    #region Transaction ID Mismatch Checks

    [Test]
    public async Task RegisterPaymentReceipt_WhenTransactionIdAlreadySetAndDifferent_ShouldReturnWrongTransactionIdExchangeError()
    {
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment(txId: DefaultTxId);
        var price = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DifferentTxId, price);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(OrderPaymentErrors.WrongTransactionIdExchangeCode);
    }

    #endregion

    #region Guard Clauses

    [Test]
    public async Task RegisterPaymentReceipt_WithNullTransactionId_ShouldThrowArgumentNullException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var action = () => { _ = payment.RegisterPaymentReceipt(null!, price); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task RegisterPaymentReceipt_WithEmptyTransactionId_ShouldThrowArgumentException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var action = () => { _ = payment.RegisterPaymentReceipt(string.Empty, price); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task RegisterPaymentReceipt_WithWhiteSpaceTransactionId_ShouldThrowArgumentException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();
        var price = PriceDataFactory.CreatePrice(100m);

        var action = () => { _ = payment.RegisterPaymentReceipt("   ", price); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task RegisterPaymentReceipt_WithNullActualAmount_ShouldThrowArgumentNullException()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment();

        var action = () => { _ = payment.RegisterPaymentReceipt(DefaultTxId, null!); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    #endregion

    #region State Invariance Checks

    [Test]
    public async Task RegisterPaymentReceipt_PreservesExistingProperties()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment(100m);
        var id = payment.Id;
        var orderId = payment.OrderId;
        var createdAtUtc = payment.CreatedAtUtc;
        var exactPrice = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, exactPrice);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(payment)
            .Member(o => o.Id, m => m.IsEqualTo(id))
            .And.Member(o => o.OrderId, m => m.IsEqualTo(orderId))
            .And.Member(o => o.CreatedAtUtc, m => m.IsEqualTo(createdAtUtc));
    }

    [Test]
    public async Task RegisterPaymentReceipt_ExternalSessionIsSameAfterRegistration()
    {
        var payment = OrderPaymentDataFactory.CreateDefaultOrderPayment(100m);
        var session = ExternalSession.Create("session-1", "url", DateTime.UtcNow.AddMinutes(60)).Value;
        payment.AttachSession(session, DateTime.UtcNow);
        var exactPrice = PriceDataFactory.CreatePrice(100m);

        var result = payment.RegisterPaymentReceipt(DefaultTxId, exactPrice);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
        await Assert.That(payment.ExternalSession).IsEqualTo(session);
    }

    #endregion
}