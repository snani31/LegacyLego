namespace LegacyLego.Domain.Tests.Common.Builders;

public class OrderBuilder
{
    private OrderAddress? _address = null!;
    private Guid? _clientId = null!;
    private List<OrderItem>? _items = null!;

    public OrderBuilder WithAddress(OrderAddress address)
    {
        _address = address;
        return this;
    }

    public OrderBuilder WithNullAddress()
    {
        _address = null!;
        return this;
    }

    public OrderBuilder WithClientId(Guid clientId)
    {
        _clientId = clientId;
        return this;
    }

    public OrderBuilder WithEmptyClientId()
    {
        _clientId = Guid.Empty;
        return this;
    }

    public OrderBuilder WithItems(List<OrderItem> items)
    {
        _items = items;
        return this;
    }

    public OrderBuilder WithNullItems()
    {
        _items = null!;
        return this;
    }

    public OrderBuilder WithNoItems()
    {
        _items = new List<OrderItem>();
        return this;
    }

    public OrderBuilder AddItem(OrderItem item)
    {
        _items?.Add(item);
        return this;
    }

    public OrderBuilder AddNullItem()
    {
        _items?.Add(null!);
        return this;
    }

    public Result<Order> BuildResult()
    {
        return Order.Create(_address!, _clientId ?? Guid.Empty, _items!);
    }

    public Order BuildValue()
    {
        return Order.Create(_address!, _clientId ?? Guid.Empty, _items!).Value;
    }
}
