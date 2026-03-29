using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using System.Numerics;
using TUnit.Core;

namespace LegacyLego.Domain.Tests.PriceTests;

public class PricePlusTests
{
    [Test]
    public async Task Plus_WithValidPrices_ShouldReturnExpectedPrice()
    {
        decimal initSum = 100m;
        decimal expectedSum = 200m;

        Price expectedPrice = Price.Create(expectedSum, Currency.Usd).Value;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.Plus(p2);

        await Assert.That(pSum).IsEqualTo(expectedPrice);

        await Assert.That(ReferenceEquals(pSum, p1)).IsFalse();
        await Assert.That(ReferenceEquals(pSum, p2)).IsFalse();
    }

    [Test]
    public async Task Plus_WithUnnormalizedPrices_ShouldReturnExpectedPrice()
    {
        decimal initSum = 10.5551m;
        decimal expectedSum = 21.12m;

        Price expectedPrice = Price.Create(expectedSum, Currency.Usd).Value;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.Plus(p2);

        await Assert.That(pSum).IsEqualTo(expectedPrice);

        await Assert.That(ReferenceEquals(pSum, p1)).IsFalse();
        await Assert.That(ReferenceEquals(pSum, p2)).IsFalse();
    }

    [Test]
    public async Task Plus_WithDifferentCurrencies_ShouldThrowInvariantViolationExceptionWithCurrencyMismatchCode()
    {
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Rub).Value;

        var exception = await Assert.That(() => p1.Plus(p2))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(ex => ex.Code, code => code.EqualTo(PriceExceptionalErrors.CurrencyMismatchErrorCode));
    }

    [Test]
    public async Task Plus_WithSumDecimalOverflow_ShouldThrowInvariantViolationExceptionWithDecimalSumOverflowCode()
    {
        decimal initSum = decimal.MaxValue;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var exception = await Assert.That(() => p1.Plus(p2))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(err => err.Code, code => code.EqualTo(PriceExceptionalErrors.DecimalSumOverflowErrorCode));
    }

    [Test]
    public async Task Plus_WithBoundaryValues_ShouldNotOverflow()
    {
        var p1 = Price.Create(decimal.MaxValue - 1, Currency.Usd).Value;
        var p2 = Price.Create(1m, Currency.Usd).Value;

        var result = p1.Plus(p2);

        await Assert.That(result.Sum).IsEqualTo(decimal.MaxValue);
    }

    [Test]
    public async Task Plus_ShouldNotModifyOriginalPrices()
    {
        decimal initSum = 100m;
        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.Plus(p2);

        await Assert.That(p1.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);

        await Assert.That(p1.Sum).IsEqualTo(initSum);
        await Assert.That(p2.Sum).IsEqualTo(initSum);
    }

    [Test]
    public async Task Plus_WithMultiplyByQuantityAsParametersNormalized_ShouldReturnExpectedPrice()
    {
        decimal initSum = 100m;
        decimal expectedSum = 600m;

        Price expectedPrice = Price.Create(expectedSum, Currency.Usd).Value;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.MultiplyByQuantity(2).Plus(p2.MultiplyByQuantity(4));

        await Assert.That(pSum).IsEqualTo(expectedPrice);
    }

    [Test]
    public async Task Plus_WithMultiplyByQuantityAsParametersZero_ShouldReturnZeroPrice()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        var pSum = p1.MultiplyByQuantity(0).Plus(p2.MultiplyByQuantity(0));

        await Assert.That(pSum.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(pSum.Sum).IsEqualTo(0);
    }

    [Test]
    public async Task Plus_WithZeroPrice_ShouldReturnSamePrice()
    {
        var p = Price.Create(100m, Currency.Usd).Value;
        var zero = p.MultiplyByQuantity(0);

        var result = p.Plus(zero);

        await Assert.That(result).IsEqualTo(p);
    }

    [Test]
    public async Task Plus_ShouldBeCommutative()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        var r1 = p1.Plus(p2);
        var r2 = p2.Plus(p1);

        await Assert.That(r1).IsEqualTo(r2);
    }

    [Test]
    public async Task Plus_ShouldBeAssociative()
    {
        var p1 = Price.Create(10.555m, Currency.Usd).Value;
        var p2 = Price.Create(20.111m, Currency.Usd).Value;
        var p3 = Price.Create(30.333m, Currency.Usd).Value;

        var r1 = p1.Plus(p2).Plus(p3);
        var r2 = p1.Plus(p2.Plus(p3));

        await Assert.That(r1).IsEqualTo(r2);
    }

    [Test]
    public async Task Plus_ShouldBeCommutativeWithZero()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var zero = p1.MultiplyByQuantity(0);

        var r1 = p1.Plus(zero);

        await Assert.That(r1).IsEqualTo(p1);
    }
}