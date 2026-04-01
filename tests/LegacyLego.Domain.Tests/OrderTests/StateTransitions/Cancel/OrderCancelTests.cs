using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderCancelTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_FromPending_ShouldSucceedAndChangeStatus(Order order)
    {
        var statusBefore = order.Status;

        var cancel = order.Cancel();
        var statusAfter = order.Status;

        await Assert.That(cancel.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(OrderStatus.PendingPayment);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_ShouldRiseOrderCancelledDomainEvent(Order order)
    {
        var cancel = order.Cancel();

        await Assert.That(cancel.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderCanceled));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterExpire_ShouldResultSuccess(Order order)
    {
        var expire = order.Expire();

        var cancel = order.Cancel();

        await Assert.That(cancel.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).Count().IsEqualTo(3)
            .And.Contains(e => e.GetType() == typeof(OrderCreated))
            .And.Contains(e => e.GetType() == typeof(OrderExpired))
            .And.Contains(e => e.GetType() == typeof(OrderCanceled));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterPay_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var payment = order.Pay();
        order.ClearDomainEvents();

        var cancel = order.Cancel();

        await Assert.That(cancel.IsFailure).IsTrue();
        await Assert.That(cancel.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderCanceled));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var paiment = order.Pay();

        var rafund = order.Refund();
        order.ClearDomainEvents();

        var cancel = order.Cancel();

        await Assert.That(cancel.IsFailure).IsTrue();
        await Assert.That(cancel.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderCanceled));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstCancel = order.Cancel();
        order.ClearDomainEvents();
        var secondCancel = order.Cancel();

        await Assert.That(firstCancel.IsSuccess).IsTrue();
        await Assert.That(secondCancel.IsFailure).IsTrue();
        await Assert.That(secondCancel.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderCanceled));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_ValuesAreSameAfterCancel(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var cancel = order.Cancel();

        await Assert.That(cancel.IsSuccess).IsTrue();

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