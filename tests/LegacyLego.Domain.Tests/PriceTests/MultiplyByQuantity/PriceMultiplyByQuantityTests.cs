using TUnit.Core;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.ExceptionalErrors;

namespace LegacyLego.Domain.Tests.PriceTests;

public class PriceMultiplyByQuantityTests
{
    [Test]
    public async Task MultiplyByQuantity_WithValidFactor_ShouldReturnExpectedPrice()
    {
        int factor = 3;
        decimal initSum = 100m;
        decimal expectedSum = initSum * factor;

        var p1 = Price.Create(initSum,Currency.Usd).Value;

        var p2 = p1.MultiplyByQuantity(3);

        await Assert.That(p2.Sum).IsEqualTo(expectedSum);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task MultiplyByQuantity_WithBelowZeroFactor_ShouldThrowInvariantViolationException()
    {
        int factor = -2;
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var exception = await Assert.That(() => p1.MultiplyByQuantity(factor))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(ex => ex.Code,code => code.EqualTo(PriceExceptionalErrors.MultiplyBelowZeroErrorCode));
    }

    [Test]
    public async Task MultiplyByQuantity_WithZeroFactor_ShouldReturnValidPrice()
    {
        int factor = 0;
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var p2 = p1.MultiplyByQuantity(factor);

        await Assert.That(p2.Sum).IsEqualTo(0);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task MultiplyByQuantity_WithMinimalValidFactor_ShouldReturnValidPrice()
    {
        int factor = 1;
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var p2 = p1.MultiplyByQuantity(factor);

        await Assert.That(p2.Sum).IsEqualTo(initSum);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task MultiplyByQuantity_DecimalOverflow_ShouldThrowInvariantViolationException()
    {
        int factor = 2;
        decimal initSum = decimal.MaxValue;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var exception = await Assert.That(() => p1.MultiplyByQuantity(factor))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(ex => ex.Code, code => code.EqualTo(PriceExceptionalErrors.DecimalSumOverflowErrorCode));
    }

    [Test]
    public async Task MultiplyByQuantity_ShouldNormalizeResult()
    {
        int factor = 3;
        decimal initSum = 10.555m;

        var p = Price.Create(initSum, Currency.Usd).Value;

        var result = p.MultiplyByQuantity(factor);

        await Assert.That(result.Sum).IsEqualTo(31.68m);
    }

    [Test]
    public async Task MultiplyByQuantity_ShouldNotModifyOriginalPrice()
    {
        int factor = 3;
        decimal initSum = 100m;

        var p = Price.Create(initSum, Currency.Usd).Value;

        var result = p.MultiplyByQuantity(factor);

        await Assert.That(p).IsEqualTo(Price.Create(initSum, Currency.Usd).Value);
    }
}