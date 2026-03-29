using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using TUnit.Core;

namespace LegacyLego.Domain.Tests.CurrencyTests;

public class CurrencyEqualityTests
{
    [Test]
    public async Task Equals_WithSameCode_ShouldBeTrue()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("usd").Value;

        await Assert.That(c1.Equals(c2)).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentCode_ShouldBeFalse()
    {
        var usd = Currency.FromCode("USD").Value;
        var rub = Currency.FromCode("RUB").Value;

        await Assert.That(usd.Equals(rub)).IsFalse();
    }

    [Test]
    public async Task Equals_ShouldBeConsistentWithEqualsOperator()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("USD").Value;

        await Assert.That(c1 == c2).IsTrue();
    }

    [Test]
    public async Task Equals_ShouldBeConsistentWithNotEqualsOperator()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("RUB").Value;

        await Assert.That(c1 != c2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualObjects_ShouldBeSame()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("usd").Value;

        await Assert.That(c1.GetHashCode()).IsEqualTo(c2.GetHashCode());
    }

    [Test]
    public async Task Equals_ShouldDependOnlyOnCode()
    {
        var usd = Currency.FromCode("USD").Value;

        await Assert.That(usd.Code).IsEqualTo("USD");
    }
}