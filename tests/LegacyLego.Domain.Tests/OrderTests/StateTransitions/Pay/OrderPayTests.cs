using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderPayTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_FromPending_ShouldSucceedAndChangeStatus(Order order)
    {
        var statusBefore = order.Status;

        var payment = order.Pay();
        var statusAfter = order.Status;

        await Assert.That(payment.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(OrderStatus.PendingPayment);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Paid);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_ShouldRiseOrderPaidDomainEvent(Order order)
    {
        var payment = order.Pay();

        await Assert.That(payment.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterPay_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstPay = order.Pay();
        order.ClearDomainEvents();
        var secondPay = order.Pay();

        await Assert.That(firstPay.IsSuccess).IsTrue();
        await Assert.That(secondPay.IsFailure).IsTrue();
        await Assert.That(secondPay.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Paid);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterExpire_ShouldResultSuccess(Order order)
    {
        var expire = order.Expire();

        var paiment = order.Pay();

        await Assert.That(expire.IsSuccess).IsTrue();
        await Assert.That(paiment.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).Count().IsEqualTo(3)
            .And.Contains(e => e.GetType() == typeof(OrderCreated))
            .And.Contains(e => e.GetType() == typeof(OrderExpired))
            .And.Contains(e => e.GetType() == typeof(OrderPaid));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Paid);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstPayment = order.Pay();

        var refund = order.Refund();
        order.ClearDomainEvents();

        var secondPayment = order.Pay();

        await Assert.That(firstPayment.IsSuccess).IsTrue();
        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(secondPayment.IsFailure).IsTrue();
        await Assert.That(secondPayment.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var cancell = order.Cancel();
        order.ClearDomainEvents();

        var payment = order.Pay();

        await Assert.That(cancell.IsSuccess).IsTrue();
        await Assert.That(payment.IsFailure).IsTrue();
        await Assert.That(payment.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_ValuesAreSameAfterPayment(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var payment = order.Pay();

        await Assert.That(payment.IsSuccess).IsTrue();

        await Assert.That(order)
            .Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.Id, m => m.IsEqualTo(orderID))
            .And.Member(o => o.ClientId, m => m.IsEqualTo(clientID))
            .And.Member(o => o.CreationDateUtc, m => m.IsEqualTo(orderCreatedAt))
            .And.Member(o => o.Address, m => m.IsEqualTo(orderAddress))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(afterCreatingItems));
    }
}