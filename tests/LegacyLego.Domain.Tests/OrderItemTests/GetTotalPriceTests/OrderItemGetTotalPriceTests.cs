using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using System.Numerics;
using TUnit.Assertions;
using TUnit.Assertions.Exceptions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using TUnit.Engine.Hooks;

namespace LegacyLego.Domain.Tests.CurrencyTests.FromCode;

public class OrderItemGetTotalPriceTests
{
    [Test]
    public async Task GetTotalPrice_WithNormalizedPrice_ShouldEqualsExpected()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create(
            "Item",
            3,
            Guid.NewGuid(),
            p).Value;

        var expectedPrice = Price.Create(30m, Currency.Usd).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total).Is.EqualTo(expectedPrice);
    }

    [Test]
    public async Task GetTotalPrice_CurrencyShouldStayConsistent()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create(
            "Item",
            3,
            Guid.NewGuid(),
            p).Value;

        var expectedPrice = Price.Create(30m, Currency.Usd).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total.Currency).Is.EqualTo(item.UnitPrice.Currency);
    }

    [Test]
    public async Task GetTotalPrice_WithUnnormalizedPrice_ShouldEqualsExpected()
    {
        // gonna be normalized here at first to 10.56
        var p = Price.Create(10.5552m, Currency.Usd).Value;

        var item = OrderItem.Create(
            "Item",
            3,
            Guid.NewGuid(),
            p).Value;

        var expectedPrice = Price.Create(31.68m, Currency.Usd).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total).Is.EqualTo(expectedPrice);
    }

    [Test]
    public async Task GetTotalPrice_UnitPriceSholdStayConsistent()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create(
            "Item",
            3,
            Guid.NewGuid(),
            p).Value;

        item.GetTotalPrice();

        await Assert.That(item.UnitPrice).Is.EqualTo(p);
        await Assert.That(ReferenceEquals(item.UnitPrice, p)).Is.True();
    }

    [Test]
    public async Task GetTotalPrice_ItemImmutability()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var originalItem = OrderItem.Create(
            "Item",
            3,
            Guid.NewGuid(),
            p).Value;

        var item = originalItem;

        item.GetTotalPrice();

        await Assert.That(item).Is.EqualTo(originalItem);
    }

    [Test]
    public async Task GetTotalPrice_WithQuantityOne_ShouldReturnUnitPrice()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create(
            "Item",
            1,
            Guid.NewGuid(),
            p).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total).Is.EqualTo(p);
    }
}
