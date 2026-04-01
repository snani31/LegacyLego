namespace LegacyLego.Domain.Tests.OrderItemTests;

public class OrderItemCreateTests
{
    [Test]
    public async Task Create_WithValidValues_ShouldPreserveCurrency()
    {
        var guid = Guid.NewGuid();
        var title = "New Item";
        var quantity = 3;
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(title, quantity, guid, p);

        await Assert.That(r.IsSuccess).IsTrue();
        await Assert.That(r.Value)
            .Member(x => x.ProductId, m => m.IsEqualTo(guid))
            .And.Member(x => x.Title, m => m.IsEqualTo(title))
            .And.Member(x => x.Quantity, m => m.IsEqualTo(quantity))
            .And.Member(x => x.UnitPrice, m => m.IsEqualTo(p));
    }

    [Test]
    public async Task Create_WithUntrimmedTitle_ShouldReturnSuccess()
    {
        var untrimmed = "    untrimmed     ";
        var trimmed = untrimmed.Trim();
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(untrimmed, 3, Guid.NewGuid(), p);

        await Assert.That(r.IsSuccess).IsTrue();
        await Assert.That(r.Value.Title).IsEqualTo(trimmed);
    }

    [Test]
    public async Task Create_WithNullTitle_ShouldReturnFailureWithTitleInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var action = () => OrderItem.Create(null!, 3, Guid.NewGuid(), p);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithWhiteTitle_ShouldReturnFailureWithTitleInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var action = () => OrderItem.Create("", 3, Guid.NewGuid(), p);

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Create_WithEmptyGuid_ShouldReturnFailureWithProductIDGuidInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", 3, Guid.Empty, p);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(OrderItemErrors.ProductIDGuidInvalidCode);
    }

    [Test]
    [Arguments(0)]
    [Arguments(-10)]
    public async Task Create_InvalidQuantity_ShouldReturnFailureWithQuantityBelowOne(int quantity)
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", quantity, Guid.NewGuid(), p);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(OrderItemErrors.QuantityBelowOneCode);
    }

    [Test]
    public async Task Create_WithMinimalValidQuantity_ShouldReturnSuccess()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", 1, Guid.NewGuid(), p);

        await Assert.That(r.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Create_WithNullUnitPrice_ShouldThrowArgumentNullException()
    {
        var action = () => OrderItem.Create("New Item", 3, Guid.NewGuid(), null!);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithZeroPrice_ShouldReturnSuccess()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", 1, Guid.NewGuid(), p.MultiplyByQuantity(0));

        await Assert.That(r.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item", 1, guid, p).Value;
        var item2 = OrderItem.Create("New Item", 1, guid, p).Value;

        await Assert.That(item1).IsEqualTo(item2);
        await Assert.That(ReferenceEquals(item1, item2)).IsFalse();
    }

    [Test]
    public async Task Create_EqualObjects_ShouldHaveSameHashCode()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item", 1, guid, p).Value;
        var item2 = OrderItem.Create("New Item", 1, guid, p).Value;

        await Assert.That(item1.GetHashCode()).IsEqualTo(item2.GetHashCode());
    }
}