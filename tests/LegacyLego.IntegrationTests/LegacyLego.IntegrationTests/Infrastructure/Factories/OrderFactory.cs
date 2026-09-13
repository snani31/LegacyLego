using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.IntegrationTests.Infrastructure.Factories;

public static class OrderFactory
{
    public static Order CreatePendingOrder(
        Guid clientId,
        string currencyCode = "USD",
        decimal priceAmount = 799.99m)
    {
        var targetClientId = ClientId.From(clientId);
        var currency = Currency.FromCode(currencyCode).Value;

        var address = OrderAddress.Create("USA", "New York", "5th Avenue", "10001").Value;
        var price = Price.Create(priceAmount, currency).Value;

        var item = OrderItem.Create(
            title: "Lego Star Wars Millenium Falcon",
            quantity: 1,
            productId: Guid.NewGuid(),
            unitPrice: price).Value;

        var orderResult = Order.Create(address, targetClientId.Value, new List<OrderItem>() { item });

        if (orderResult.IsFailure)
        {
            throw new InvalidOperationException($"Не удалось создать тестовый заказ: {orderResult.Error.Message}");
        }

        return orderResult.Value;
    }
}