using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Domain.Exceptions;

namespace LegacyLego.Domain.Aggregates;

public class Order : AggregateRoot<OrderId>
{
    public Guid ClientId { get; }

    private Price? _frozenTotalPrice;

    public Price TotalPrice
    {
        get
        {
            return Status switch
            {
                OrderStatus.PendingPayment or OrderStatus.Expired
                    => CalculateTotalPrice(),
                OrderStatus.Paid or OrderStatus.Cancelled or OrderStatus.Refunded
                    => _frozenTotalPrice ?? throw new InvalidDomainStateException(
                        OrderExceptionalErrors.GetFrozenTotalPriceNotCalculatedError(Status)),
                _ => throw new InvalidDomainStateException(
                        OrderExceptionalErrors.GetWrongOrderStatusToGetTotalPriceError(Status)),
            };
        }
    }

    public OrderStatus Status { get; private set; }


    private readonly List<OrderItem> _items;

    public IReadOnlyList<OrderItem> Items => _items;

    public OrderAddress Address { get; }

    public DateTime CreationDateUtc { get; }

    private Order(
        OrderId id,
        Guid clientId,
        OrderStatus status,
        List<OrderItem> items,
        OrderAddress address,
        DateTime creationDateUtc) 
        : base(id)
    {
        ClientId = clientId;
        Status = status;
        _items = new List<OrderItem>(items);
        Address = address;
        CreationDateUtc = creationDateUtc;
    }

    public static Result<Order> Create(
        OrderAddress address,
        Guid clientId,
        List<OrderItem> items)
    {
        if (items is null)
            throw new ArgumentNullException(nameof(items));

        var itemsCount = items.Count;

        // общее количество позиций не меньше одной
        if (itemsCount < 1)
            return Result<Order>.Failure(OrderErrors.GetItemsCountInvalidError(itemsCount));

        var distinctCurrenciesCount = items.Select(x => x.UnitPrice.Currency).Distinct().Count();

        //  позиций заказа не представлены разными валютами
        if (distinctCurrenciesCount != 1)
            return Result<Order>.Failure(OrderErrors.GetItemsCurrenciesMismatchError());

        var currency = items.First().UnitPrice.Currency;

        var total = Price.Zero(currency);

        foreach (var item in items)
        {
            total = total.Plus(item.GetTotalPrice());
        }

        // общая стоимость всех позиций заказа больше нуля
        if (total.Sum <= 0)
            return Result<Order>.Failure(OrderErrors.GetItemsTotalBelowZeroError(total.Sum));

        var createdAt = DateTime.UtcNow;
        var orderId = OrderId.New();

        var order = new Order(
            orderId,
            clientId,
            OrderStatus.PendingPayment,
            items,
            address,
            createdAt);

        order.Raise(new OrderCreated(orderId, clientId, createdAt));

        return Result<Order>.Success(order);

    }

    public Result Pay()
    {
        var orderAction = OrderAction.Pay;
        var nextStatus = OrderStatus.Paid;

        if (Status is not (OrderStatus.PendingPayment or OrderStatus.Expired))
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;
        _frozenTotalPrice = CalculateTotalPrice();

        base.Raise(new OrderPaid(Id,DateTime.UtcNow));

        return Result.Success();
    }

    public Result Cancel()
    {
        var orderAction = OrderAction.Cancel;
        var nextStatus = OrderStatus.Cancelled;

        if (Status is not (OrderStatus.PendingPayment or OrderStatus.Expired))
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;
        _frozenTotalPrice = CalculateTotalPrice();

        base.Raise(new OrderCanceled(Id, DateTime.UtcNow));

        return Result.Success();
    }

    public Result Expire()
    {
        var orderAction = OrderAction.Expire;
        var nextStatus = OrderStatus.Expired;

        if (Status is not OrderStatus.PendingPayment)
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderExpired(Id, DateTime.UtcNow));

        return Result.Success();
    }

    public Result Refund()
    {
        var orderAction = OrderAction.Refund;
        var nextStatus = OrderStatus.Refunded;

        if (Status is not OrderStatus.Paid)
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderRefunded(Id, DateTime.UtcNow));

        return Result.Success();
    }

    private Price CalculateTotalPrice()
    {
        // при текущих инвариантах это невозможно, но станет актуально в случае, если добавятся функции добавления/удаления позиции товара
        if (_items.Count == 0)
            throw new InvariantViolationException(
                OrderExceptionalErrors.GetOrderContainsNoItemsError());

        var currency = Items.First().UnitPrice.Currency;

        var total = Price.Zero(currency);

        foreach (var item in Items)
            total = total.Plus(item.GetTotalPrice());

        return total;
    }
}