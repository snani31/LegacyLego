using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderRefundTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_FromPending_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var refund = order.Refund();
        var statusAfter = order.Status;

        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.PendingPayment);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterExpire_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var expire = order.Expire();
        order.ClearDomainEvents();
        var refund = order.Refund();

        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Expired);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterPay_ShouldResultSuccess(Order order)
    {
        var payment = order.Pay();

        var statusBefore = order.Status;

        var refund = order.Refund();

        var statusAfter = order.Status;

        await Assert.That(refund.IsSuccess).IsTrue();

        await Assert.That(order.DomainEvents).Count().IsEqualTo(3)
            .And.Contains(e => e.GetType() == typeof(OrderCreated))
            .And.Contains(e => e.GetType() == typeof(OrderPaid))
            .And.Contains(e => e.GetType() == typeof(OrderRefunded));

        await Assert.That(statusBefore).IsEqualTo(OrderStatus.Paid);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var payment = order.Pay();

        var firstRefund = order.Refund();
        order.ClearDomainEvents();

        var secondRefund = order.Refund();

        await Assert.That(firstRefund.IsSuccess).IsTrue();
        await Assert.That(secondRefund.IsFailure).IsTrue();

        await Assert.That(secondRefund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));

        await Assert.That(order.Status).IsEqualTo(OrderStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var cancell = order.Cancel();
        order.ClearDomainEvents();

        var refund = order.Refund();

        await Assert.That(refund.IsFailure).IsTrue();

        await Assert.That(refund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));

        await Assert.That(order.Status).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_ValuesAreSameAfterRefund(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var payment = order.Pay();
        var refund = order.Refund();

        await Assert.That(refund.IsSuccess).IsTrue();

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