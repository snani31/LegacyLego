using LegacyLego.Domain.Errors;
using LegacyLego.Domain.ValueObjects;
using TUnit.Assertions;
using TUnit.Assertions.Exceptions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace LegacyLego.Domain.Tests.CurrencyTests.FromCode;

public class PriceCreateTests
{
    [Test]
    public async Task Create_ShouldPreserveCurrency()
    {
        var result = Price.Create(10m, Currency.Eur);

        await Assert.That(result.Value.Currency).Is.EqualTo(Currency.Eur);
    }

    [Test]
    public void Create_WithNullCurrency_ShouldThrowArgumentNullException()
    {
        var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
        {
            Price.Create(100m, null!);
        });
    }

    [Test]
    public async Task Create_WithValidParameters_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(100m, currency);

        await Assert.That(result.IsSuccess).Is.True();
        await Assert.That(result.Value.Sum).Is.EqualTo(100m);
        await Assert.That(result.Value.Currency).Is.EqualTo(currency);
    }

    [Test]
    public async Task Create_WithNegativeSum_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(-100m, currency);

        await Assert.That(result.IsFailure).Is.True();
        await Assert.That(result.Error.Code).Is.EqualTo(PriceErrors.SumBelowZeroCode);
    }

    [Test]
    public async Task Create_WithZeroSum_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(0m, currency);

        await Assert.That(result.IsFailure).Is.True();
        await Assert.That(result.Error.Code).Is.EqualTo(PriceErrors.SumBelowZeroCode);
    }

    [Test]
    public async Task Create_WithZeroNormalizedSum_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(0.0004m, currency);

        await Assert.That(result.IsFailure).Is.True();
        await Assert.That(result.Error.Code).Is.EqualTo(PriceErrors.SumBelowZeroCode);
    }

    [Test]
    public async Task Create_WithMaximalDecimalSum_ShoulReturnSuccess()
    {
        var currency = Currency.Usd;
        var result = Price.Create(decimal.MaxValue, currency);

        await Assert.That(result.IsSuccess).Is.True();
        await Assert.That(result.Value.Sum).Is.EqualTo(decimal.MaxValue);
        await Assert.That(result.Value.Currency).Is.EqualTo(currency);
    }

    [Test]
    public async Task Create_ShouldNormalizeSumAccordingToCurrencyScale()
    {
        var currency = Currency.Usd;

        // 10.555 → 10.56 (banker's rounding)
        var result = Price.Create(10.555m, currency);

        await Assert.That(result.IsSuccess).Is.True();
        await Assert.That(result.Value.Sum).Is.EqualTo(10.56m);
    }

    [Test]
    public async Task Create_WithMinimalPositiveValue_ShouldReturnSuccess()
    {
        var currency = Currency.Usd;

        var result = Price.Create(0.01m, currency);

        await Assert.That(result.IsSuccess).Is.True();
        await Assert.That(result.Value.Sum).Is.EqualTo(0.01m);
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var currency = Currency.Usd;

        var p1 = Price.Create(10m, currency).Value;
        var p2 = Price.Create(10m, currency).Value;

        await Assert.That(p1).Is.EqualTo(p2);
        await Assert.That(ReferenceEquals(p1, p2)).Is.False();
    }

    [Test]
    public async Task EqualObjects_ShouldHaveSameHashCode()
    {
        var currency = Currency.Usd;

        var p1 = Price.Create(10m, currency).Value;
        var p2 = Price.Create(10m, currency).Value;

        await Assert.That(p1.GetHashCode()).Is.EqualTo(p2.GetHashCode());
    }
}
