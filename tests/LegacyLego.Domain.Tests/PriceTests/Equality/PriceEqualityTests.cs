using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using TUnit.Core;

namespace LegacyLego.Domain.Tests.PriceTests;

public class PriceEqualityTests
{
    [Test]
    public async Task Equals_WithSamePrice_ShouldBeTrue()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1).IsEqualTo(p2);
    }

    [Test]
    public async Task Equals_WithDifferentValues_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Rub).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1).IsNotEqualTo(p2);
    }

    [Test]
    public async Task EqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1 == p2).IsTrue();
    }

    [Test]
    public async Task EqualsOperator_WithDifferentValues_ShouldBeFalse()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        await Assert.That(p1 == p2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1 != p2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithDifferentValues_ShouldBeTrue()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        await Assert.That(p1 != p2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualObjects_ShouldBeSame()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1.GetHashCode()).IsEqualTo(p2.GetHashCode());
    }

    [Test]
    public async Task GetHashCode_ForDifferentObjects_ShouldBeDifferent()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        await Assert.That(p1.GetHashCode()).IsNotEqualTo(p2.GetHashCode());
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnDifferentInstances()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task Equals_WithNull_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price.Equals(null)).IsFalse();
    }

    [Test]
    public async Task EqualsOperator_WithNull_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price == null).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithNull_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price != null).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentType_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price.Equals("not a price")).IsFalse();
    }

    [Test]
    public async Task Equals_WithNormalizedValues_ShouldBeTrue()
    {
        var p1 = Price.Create(100.000m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1).IsEqualTo(p2);
    }
}