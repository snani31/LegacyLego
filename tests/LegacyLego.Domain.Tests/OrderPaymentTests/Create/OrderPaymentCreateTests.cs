namespace LegacyLego.Domain.Tests.OrderPaymentTests;

public class OrderPaymentCreateTests
{
    [Test]
    public async Task Create_WithValidValues_ShouldResultSuccess()
    {
        var id = OrderId.New();
        var now = DateTime.UtcNow;

        var r = OrderPayment.Create(id, now); 

        await Assert.That(r.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Create_WithValidValues_ShouldBeSameValues()
    {
        var id = OrderId.New();
        var now = DateTime.UtcNow;

        var r = OrderPayment.Create(id, now);

        await Assert.That(r.Value)
            .Member(v => v.OrderId, m => m.IsEqualTo(id))
            .And.Member(v => v.CreatedAtUtc, m => m.IsEqualTo(now))
            .And.Member(v => v.Id, m => m.IsNotNull());
    }

    [Test]
    public async Task Create_WithValidValues_ShouldReturnOrderPaymentInPendingStatus()
    {
        var id = OrderId.New();
        var now = DateTime.UtcNow;

        var r = OrderPayment.Create(id, now);

        await Assert.That(r.Value.Status).IsEqualTo(PaymentStatus.Pending);
    }

    [Test]
    public async Task Create_WithValidValues_ShouldRaiseOrderPaymentCreatedDomainEvent()
    {
        var id = OrderId.New();
        var now = DateTime.UtcNow;

        var r = OrderPayment.Create(id, now);

        await Assert.That(r.Value.Status).IsEqualTo(PaymentStatus.Pending);

        await Assert.That(r.IsSuccess).IsTrue();
        await Assert.That(r.Value.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaymentCreated));
    }

    #region Guard Clauses

    [Test]
    public async Task Create_WithAddressNull_ShouldThrowArgumentNullException()
    {
        var action = () => { var r = OrderPayment.Create(null!, DateTime.UtcNow); };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithDefaultDateTime_ShouldThrowArgumentException()
    {
        var action = () => { var r = OrderPayment.Create(OrderId.New(), default); };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Create_WithLocalCreatedAtTime_ShouldResultFailureWithCreationTimeWasNotUtcCode()
    {
        var id = OrderId.New();
        var nowlocal = DateTime.Now;

        var r = OrderPayment.Create(id, nowlocal);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(OrderPaymentErrors.CreationTimeWasNotUtcCode);
    }

    [Test]
    public async Task Create_WithUnspecifiedCreatedAtTime_ShouldResultFailureWithCreationTimeWasNotUtcCode()
    {
        var id = OrderId.New();
        var nowUnspecified = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

        var r = OrderPayment.Create(id, nowUnspecified);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(OrderPaymentErrors.CreationTimeWasNotUtcCode);
    }

    #endregion
}