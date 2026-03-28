using LegacyLego.Domain.Errors;
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using TUnit.Assertions;
using TUnit.Assertions.Exceptions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace LegacyLego.Domain.Tests.OrderItemTests.Create;

public class OrderItemCreateTests
{
    [Test]
    public async Task Create_WithValidValues_ShouldPreserveCurrency()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(
            tytle,
            quantity,
            guid,
            p);

        await Assert.That(r.IsSuccess).Is.True();
        await Assert.That(r.Value).Has.Member(r => r.ProductId).EqualTo(guid)
            .And.Has.Member(r => r.Title).EqualTo(tytle)
            .And.Has.Member(r => r.Quantity).EqualTo(quantity)
            .And.Has.Member(r => r.UnitPrice).EqualTo(p);
    }

    [Test]
    public async Task Create_WithUntrimmedTitle_ShouldRetutnFailure()
    {
        var untrimmed = "    untrimmed     ";
        var trimmed = untrimmed.Trim();
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(
            untrimmed,
            3,
            Guid.NewGuid(),
            p);

        await Assert.That(r.IsSuccess).Is.True();
        await Assert.That(r.Value.Title).Is.EqualTo(trimmed);
    }

    [Test]
    public async Task Create_WithNullTitle_ShouldRetutnFailureWithTitleInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(
            null!,
            3,
            Guid.NewGuid(),
            p);

        await Assert.That(r.IsFailure).Is.True();
        await Assert.That(r.Error.Code).Is.EqualTo(OrderItemErrors.TitleInvalidCode);
    }

    [Test]
    public async Task Create_WithWhiteTitle_ShouldRetutnFailureWithTitleInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(
            "",
            3,
            Guid.NewGuid(),
            p);

        await Assert.That(r.IsFailure).Is.True();
        await Assert.That(r.Error.Code).Is.EqualTo(OrderItemErrors.TitleInvalidCode);
    }

    [Test]
    public async Task Create_WithEmpthyGuid_ShouldRetutnFailureWithProductIDGuidInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(
            "New Item",
            3,
            Guid.Empty,
            p);

        await Assert.That(r.IsFailure).Is.True();
        await Assert.That(r.Error.Code).Is.EqualTo(OrderItemErrors.ProductIDGuidInvalidCode);
    }

    [DataDrivenTest]
    [Arguments(0)]
    [Arguments(-10)]
    public async Task Create_InvalidQuantity_ShouldRetutnFailureWithQuantityBelowOne(int quantity)
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(
            "New Item",
            quantity,
            Guid.NewGuid(),
            p);

        await Assert.That(r.IsFailure).Is.True();
        await Assert.That(r.Error.Code).Is.EqualTo(OrderItemErrors.QuantityBelowOneCode);
    }

    [Test]
    public async Task Create_WithMinimalValidQuantity_ShouldReturnSuccess()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p);

        await Assert.That(r.IsSuccess).Is.True();
    }

    [Test]
    public async Task Create_WithMinimalValidQuantity_ShouldThrowArgumentNullException()
    {
        var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
        {
            var r = OrderItem.Create(
                "New Item",
                3,
                Guid.NewGuid(),
                null!);
        });

        await Assert.That(exception).Is.Not.Null();
    }

    [Test]
    public async Task Create_WithZeroPrice_ShouldReturnSuccess()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(
            "New Item",
            1,
            Guid.NewGuid(),
            p.MultiplyByQuantity(0));

        await Assert.That(r.IsSuccess).Is.True();
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create(
            "New Item",
            1,
            guid,
            p).Value;

        var item2 = OrderItem.Create(
            "New Item",
            1,
            guid,
            p).Value;


        await Assert.That(item1).Is.EqualTo(item2);
        await Assert.That(ReferenceEquals(item1, item2)).Is.False();
    }

    [Test]
    public async Task Create_EqualObjects_ShouldHaveSameHashCode()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create(
            "New Item",
            1,
            guid,
            p).Value;

        var item2 = OrderItem.Create(
            "New Item",
            1,
            guid,
            p).Value;

        await Assert.That(item1.GetHashCode()).Is.EqualTo(item2.GetHashCode());
    }
}