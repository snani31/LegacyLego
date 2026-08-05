using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentAttachSessionTests
{
    private static readonly Price DefaultPrice = PriceDataFactory.CreatePrice();

    [Test]
    public async Task Attach_WhenStatusIsPending_ShouldReturnSuccess()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;
        var attach = payment.AttachSession(session, now);

        await Assert.That(attach.IsSuccess).IsTrue();
        await Assert.That(payment.HasSession).IsTrue();
    }

    [Test]
    public async Task Attach_WhenStatusIsSucceeded_ShouldReturnFailureWithWrongStatusForExternalSessionTransitionCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPaymentDataFactory.CreateSucceededOrderPayment(txId: "transactionId");
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var attach = payment.AttachSession(session, now);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.WrongStatusForExternalSessionTransitionCode);
        await Assert.That(payment.HasSession).IsFalse();
    }

    [Test]
    public async Task Attach_WhenStatusIsFailed_ShouldReturnFailureWithWrongStatusForExternalSessionTransitionCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var failure = payment.MarkAsFailed();
        var attach = payment.AttachSession(session, now);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.WrongStatusForExternalSessionTransitionCode);
        await Assert.That(payment.HasSession).IsFalse();
    }

    [Test]
    public async Task Attach_WhenStatusIsRefundRequested_ShouldReturnFailureWithWrongStatusForExternalSessionTransitionCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPaymentDataFactory.CreateRefundRequestedOrderPayment(txId: "transactionId");
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var attach = payment.AttachSession(session, now);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.WrongStatusForExternalSessionTransitionCode);
        await Assert.That(payment.HasSession).IsFalse();
    }

    [Test]
    public async Task Attach_WhenStatusIsRefunded_ShouldReturnFailureWithWrongStatusForExternalSessionTransitionCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var refund = payment.MarkAsRefunded("transactionId");
        var attach = payment.AttachSession(session, now);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.WrongStatusForExternalSessionTransitionCode);
        await Assert.That(payment.HasSession).IsFalse();
    }

    #region Guard Clauses

    [Test]
    public async Task Attach_WithNullNewSession_ShouldThrowArgumentNullException()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;

        var action = () => { payment.AttachSession(null!, now); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Attach_WithLocalNowTime_ShouldResultFailureWithNowTimeWasNotUtcForAttachSessionCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now).Value;

        var attach = payment.AttachSession(session, DateTime.Now);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.NowTimeWasNotUtcForAttachSessionCode);
    }

    [Test]
    public async Task Attach_WithUnspecifiedNowTime_ShouldResultFailureWithNowTimeWasNotUtcForAttachSessionCode()
    {
        var now = DateTime.UtcNow;
        var nowUnspecified = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var attach = payment.AttachSession(session, nowUnspecified);

        await Assert.That(attach.IsFailure).IsTrue();
        await Assert.That(attach.Error.Code).IsEqualTo(OrderPaymentErrors.NowTimeWasNotUtcForAttachSessionCode);
    }

    #endregion

    [Test]
    public async Task Attach_WhenSessionStillActive_ShouldReturnFailureWithEnsuredSessionIsNotExpiredTransitionFailureCode()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(60)).Value;

        var firstAttach = payment.AttachSession(session, now);
        var secondAttach = payment.AttachSession(session, now);

        await Assert.That(secondAttach.IsFailure).IsTrue();
        await Assert.That(secondAttach.Error.Code).IsEqualTo(OrderPaymentErrors.EnsuredSessionIsNotExpiredTransitionFailureCode);
    }

    [Test]
    public async Task Attach_WhenSessionExpired_ShouldSucceed()
    {
        var now = DateTime.UtcNow;
        var payment = OrderPayment.Create(OrderId.New(), DefaultPrice, now).Value;
        var session = ExternalSession.Create("id", "url", now.AddMinutes(10)).Value;

        var firstAttach = payment.AttachSession(session, now.AddHours(24));
        var secondAttach = payment.AttachSession(session, now.AddHours(24));

        await Assert.That(secondAttach.IsSuccess).IsTrue();
    }
}