using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using TUnit.Assertions;
using TUnit.Assertions.Exceptions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace LegacyLego.Domain.Tests.CurrencyTests.FromCode;

public class OrderItemEqualityTests
{
    [Test]
    public async Task Create_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(
            tytle,
            quantity,
            guid,
            p).Value;

        var item2 = OrderItem.Create(
            tytle,
            quantity,
            guid,
            p).Value;


        await Assert.That(item1).Is.EqualTo(item2);
        await Assert.That(ReferenceEquals(item1, item2)).Is.False();
    }

    [Test]
    public async Task EqualsOperator_WitSameValues_ShouldBeTrue()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(
            tytle,
            quantity,
            guid,
            p).Value;

        var item2 = OrderItem.Create(
            tytle,
            quantity,
            guid,
            p).Value;

        await Assert.That(item1 == item2).Is.True();
    }

    [Test]
    public async Task EqualsOperator_WithDifferentValues_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Rub).Value;

        var item1 = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p1).Value;

        var item2 = OrderItem.Create(
            "New Item2",
            2,
            Guid.NewGuid(),
            p2).Value;

        await Assert.That(item1 == item2).Is.False();
    }

    [Test]
    public async Task NotEqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(
            tytle,
            quantity,
            guid,
            p).Value;

        var item2 = OrderItem.Create(
            tytle,
            quantity,
            guid,
            p).Value;

        await Assert.That(item1 != item2).Is.False();
    }

    [Test]
    public async Task NotEqualsOperator_WithDifferentValues_ShouldBeTrue()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Rub).Value;

        var item1 = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p1).Value;

        var item2 = OrderItem.Create(
            "New Item2",
            2,
            Guid.NewGuid(),
            p2).Value;

        await Assert.That(item1 != item2).Is.True();
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnDifferentInstances()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(
            tytle,
            quantity,
            guid,
            p).Value;

        var item2 = OrderItem.Create(
            tytle,
            quantity,
            guid,
            p).Value;

        await Assert.That(ReferenceEquals(item1, item2)).Is.False();
    }

    [Test]
    public async Task Equals_WithNull_ShouldBeFalse()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p).Value;

        await Assert.That(item.Equals(null)).Is.False();
    }

    [Test]
    public async Task EqualsOperator_WithNull_ShouldBeFalse()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p).Value;

        await Assert.That(item == null).Is.False();
    }

    [Test]
    public async Task NotEqualsOperator_WithNull_ShouldBeFalse()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p).Value;

        await Assert.That(item != null).Is.True();
    }

    [Test]
    public async Task Equals_WithDifferentType_ShouldBeFalse()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p).Value;

        await Assert.That(item.Equals("not a order item")).Is.False();
    }

    [Test]
    public async Task EqualsOperator_WithBothNull_ShouldBeTrue()
    {
        OrderItem? a = null;
        OrderItem? b = null;

        await Assert.That(a == b).Is.True();
    }

    [Test]
    public async Task NotEqualsOperator_WithBothNull_ShouldBeFalse()
    {
        OrderItem? a = null;
        OrderItem? b = null;

        await Assert.That(a != b).Is.False();
    }

    [Test]
    public async Task Equals_WithSameReference_ShouldBeTrue()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        var item = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p).Value;

        await Assert.That(item.Equals(item)).Is.True();
    }

    [Test]
    public async Task Equals_WithDifferentTitle_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(10m, Currency.Usd).Value;

        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create(
            "New Item",
            1,
            guid,
            p1).Value;

        var item2 = OrderItem.Create(
            "New Item2",
            1,
            guid,
            p2).Value;

        await Assert.That(item1).Is.Not.EqualTo(item2);
        await Assert.That(item1 == item2).Is.False();
        await Assert.That(item1 != item2).Is.True();
    }

    [Test]
    public async Task Equals_WithDifferentQuantity_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(10m, Currency.Usd).Value;

        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create(
            "New Item",
            1,
            guid,
            p1).Value;

        var item2 = OrderItem.Create(
            "New Item",
            2,
            guid,
            p2).Value;

        await Assert.That(item1).Is.Not.EqualTo(item2);
        await Assert.That(item1 == item2).Is.False();
        await Assert.That(item1 != item2).Is.True();
    }

    [Test]
    public async Task Equals_WithDifferentProductId_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p1).Value;

        var item2 = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p2).Value;

        await Assert.That(item1).Is.Not.EqualTo(item2);
        await Assert.That(item1 == item2).Is.False();
        await Assert.That(item1 != item2).Is.True();
    }

    [Test]
    public async Task Equals_WithDifferentUnitPrice_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Rub).Value;

        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create(
            "New Item",
            1,
            guid,
            p1).Value;

        var item2 = OrderItem.Create(
            "New Item",
            1,
            guid,
            p2).Value;

        await Assert.That(item1).Is.Not.EqualTo(item2);
        await Assert.That(item1 == item2).Is.False();
        await Assert.That(item1 != item2).Is.True();
    }
}