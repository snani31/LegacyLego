namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderCreateTests
{
    private static OrderAddress DefaultAddress =>
    OrderAddress.Create("US", "Berlin", "New York", "90210").Value;

    private static List<OrderItem> DefaultItems => new List<OrderItem>()
    {
            OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m,Currency.Usd).Value).Value,
            OrderItem.Create("New Item2", 2, Guid.NewGuid(), Price.Create(200m, Currency.Usd).Value).Value,
            OrderItem.Create("New Item3", 3, Guid.NewGuid(), Price.Create(300m, Currency.Usd).Value).Value
    };

    [Test]
    public async Task Create_WithValidValues_ShouldResultSuccess()
    {
        var clientId = Guid.NewGuid();
        var items = DefaultItems;

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(items)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value)
            .Member(o => o.Address, m => m.IsEqualTo(DefaultAddress))
            .And.Member(o => o.ClientId.Value, m => m.EqualTo(clientId))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(items));
    }

    [Test]
    public async Task Create_OrderItemsListShouldNotBeSameReference()
    {
        var clientId = Guid.NewGuid();
        var items = DefaultItems;

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(items)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.Items).IsNotSameReferenceAs(items);
    }

    [Test]
    public async Task Create_OrderItemsListShouldNotMutate()
    {
        var items = DefaultItems;

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        items.Clear();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.Items).IsNotEquivalentTo(items);
    }

    #region Guard Clauses

    [Test]
    public async Task Create_WithAddressNull_ShouldThrowArgumentNullException()
    {
        var action = () =>
        {
            new OrderBuilder()
            .WithNullAddress()
            .WithItems(DefaultItems)
            .WithClientId(Guid.NewGuid())
            .BuildResult();
        };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithOrderItemsNull_ShouldThrowArgumentNullException()
    {
        var action = () =>
        {
            new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNullItems()
            .WithClientId(Guid.NewGuid())
            .BuildResult();
        };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithOrderItemsListContainsNull_ShouldThrowArgumentNullException()
    {
        var action = () =>
        {
            new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .AddNullItem()
            .WithClientId(Guid.NewGuid())
            .BuildResult();
        };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    #endregion

    #region Validation Invariants

    [Test]
    public async Task Create_WithOrderItemsEmptyList_ShouldResultFailure()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ItemsCountInvalidCode);
    }

    [Test]
    public async Task Create_WithOrderItemsCurrenciesMismatch_ShouldResultFailure()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value)
            .AddItem(OrderItem.Create("New Item2", 2, Guid.NewGuid(), Price.Create(200m, Currency.Rub).Value).Value)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ItemsCurrenciesMismatchCode);
    }

    [Test]
    public async Task Create_WithOrderItemsTotalPriceZero_ShouldResultFailureWithItemsTotalBelowZeroError()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value.MultiplyByQuantity(0)).Value)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ItemsTotalBelowZeroCode);
    }

    [Test]
    public async Task Create_WithOrderItemsZeroPrices_ShouldResultSuccess()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value.MultiplyByQuantity(0)).Value)
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value.MultiplyByQuantity(0)).Value)
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
    }

    #endregion

    [Test]
    public async Task Create_WithValidValues_ShouldReturnOrderWithPendingPaymentStatus()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.Status).IsEqualTo(Enums.OrderStatus.PendingPayment);
    }

    [Test]
    public async Task Create_CreationDateUtcShouldNotBeDefoult()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.CreationDateUtc).IsNotDefault();
    }

    [Test]
    public async Task Create_WithValidValues_ShouldRiseOrderCreatedDomainEvent()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderCreated));
    }

    [Test]
    public async Task Create_TotalPriceShouldBeEquivalentAsExpected()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value)
            .AddItem(OrderItem.Create("New Item2", 2, Guid.NewGuid(), Price.Create(200m, Currency.Usd).Value).Value)
            .AddItem(OrderItem.Create("New Item3", 3, Guid.NewGuid(), Price.Create(300m, Currency.Usd).Value).Value)
            .WithClientId(clientId)
            .BuildResult();

        var expected = 1400m;

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.TotalPrice.Sum).IsEqualTo(expected);
    }
}