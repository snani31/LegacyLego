namespace LegacyLego.Domain.Tests.Common.Factories;

internal static class PriceDataFactory
{
    public static Price CreatePrice(decimal sum = 100m, string currencyCode = "RUB")
    {
        var currency = Currency.FromCode(currencyCode).Value;
        return Price.Create(sum, currency).Value;
    }
}