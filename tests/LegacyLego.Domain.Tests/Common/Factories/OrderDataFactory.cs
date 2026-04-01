namespace LegacyLego.Domain.Tests.Common.Factories;

internal static class OrderDataFactory
{
    public static Order CreateDefaultOrder()
    {
        var address = OrderAddress.Create("US", "Berlin", "New York", "90210").Value;
        var items = new List<OrderItem>
        {
            OrderItem.Create("Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value
        };
        return Order.Create(address, Guid.NewGuid(), items).Value;
    }
}
