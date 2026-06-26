using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class OrderItem : ValueObject
{
    public string Title { get; }

    public int Quantity { get; }

    public Guid ProductId { get; }

    public Price UnitPrice { get; }

    private OrderItem(
        string title,
        int quantity,
        Guid productId,
        Price unitPrice)
    {
        Title = title;
        Quantity = quantity;
        ProductId = productId;
        UnitPrice = unitPrice;
    }

    private OrderItem(
        string title,
        int quantity,
        Guid productId)
    {
        Title = title;
        Quantity = quantity;
        ProductId = productId;
    }

    public static Result<OrderItem> Create(
        string title,
        int quantity,
        Guid productId,
        Price unitPrice)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
        ArgumentNullException.ThrowIfNull(unitPrice, nameof(unitPrice));

        title = title.Trim();

        if (productId == Guid.Empty)
            return Result<OrderItem>.Failure(OrderItemErrors.GetProductIDGuidInvalidError(productId));

        if (quantity < 1)
            return Result<OrderItem>.Failure(OrderItemErrors.GetQuantityBelowOneError(quantity));

        return Result<OrderItem>.Success(new OrderItem(title, quantity, productId, unitPrice));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return ProductId;
        yield return Title;
        yield return Quantity;
        yield return UnitPrice;
    }

    public Price GetTotalPrice()
    {
        return UnitPrice.MultiplyByQuantity(Quantity);
    }
}