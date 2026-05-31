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

    public Currency Currency { get; }

    private Price? _frozenTotalPrice;

    public Price TotalPrice
    {
        get
        {
            return Status switch
            {
                OrderStatus.PendingPayment or OrderStatus.Expired
                    => CalculateTotalPrice(Items),
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

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public OrderAddress Address { get; }

    public DateTime CreationDateUtc { get; }

    private decimal? FrozenTotalSum => _frozenTotalPrice?.Sum;
    /// <summary>
    /// Приватный конструктор, используемый фабричным методом для создания нового
    /// валидного с точки зрения бизнеса заказа 
    /// </summary>
    /// <param name="id"> идентификатор заказа</param>
    /// <param name="clientId">идентификатор клиента, создавшего заказ</param>
    /// <param name="currency">валюты заказа</param>
    /// <param name="status">статус заказа</param>
    /// <param name="items">список позиций заказа</param>
    /// <param name="address">адрес доставки заказа</param>
    /// <param name="creationDateUtc">время создания заказа в формате Utc</param>
    private Order(
        OrderId id,
        Guid clientId,
        Currency currency,
        OrderStatus status,
        List<OrderItem> items,
        OrderAddress address,
        DateTime creationDateUtc) 
        : base(id)
    {
        ClientId = clientId;
        Status = status;
        _items = items;
        Address = address;
        CreationDateUtc = creationDateUtc;
        Currency = currency;
    }
    /// <summary>
    /// Приватный конструктор, используемый для материализации объекта Order
    /// EF ORM системой в соответствии с конфигурациями (Не используется бизнесом!)
    /// </summary>
    /// <param name="id"> идентификатор заказа</param>
    /// <param name="clientId">идентификатор клиента, создавшего заказ</param>
    /// <param name="currency">валюты заказа</param>
    /// <param name="status">статус заказа</param>
    /// <param name="frozenTotalSum">decimal занчение общей стоимсоти заказа</param>
    /// <param name="address">адрес доставки заказа</param>
    /// <param name="creationDateUtc">время создания заказа в формате Utc</param>
    private Order(
        OrderId id,
        Guid clientId,
        OrderStatus status,
        OrderAddress address,
        Currency currency,
        decimal? frozenTotalSum,
        DateTime creationDateUtc)
        : base(id)
    {
        ClientId = clientId;
        Status = status;
        _items = new List<OrderItem>();
        Address = address;
        Currency = currency;
        CreationDateUtc = creationDateUtc;

        _frozenTotalPrice = frozenTotalSum.HasValue
            ? Price.Create(frozenTotalSum.Value, currency).Value
            : null;
    }


    /// <summary>
    /// Фабричный метод для создания нового заказа в системе.
    /// Инкапсулирует первичные бизнес-правила создания заказа.
    /// </summary>
    /// <param name="clientId">Идентификатор клиента, совершающего заказ.</param>
    /// <param name="address">Валидный адрес доставки (Value Object).</param>
    /// <returns>
    /// Экземпляр <see cref="Result{Order}"/>, содержащий объект заказа при успехе,
    /// либо ошибку доменной логики Result.Failure (например, если нарушены базовые контракты).
    /// </returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если параметр 
    /// <paramref name="address"/> или парметр <paramref name="items"/> равен null.</exception>
    /// <exception cref="ArgumentException">Выбрасывается, если параметр коллекции 
    /// <paramref name="items"/> содержит в коллекции хоть 1 тгдд элемент.</exception>
    public static Result<Order> Create(
        OrderAddress address,
        Guid clientId,
        List<OrderItem> items)
    {
        ArgumentNullException.ThrowIfNull(items,nameof(items));
        ArgumentNullException.ThrowIfNull(address, nameof(address));

        if (items.Any(x => x is null))
            throw new ArgumentException("Items collection contains null");

        if (clientId == Guid.Empty)
            return Result<Order>.Failure(OrderErrors.GetClientIdGuidInvalidError(clientId));

        var itemsCount = items.Count;
        // общее количество позиций не меньше одной
        if (itemsCount < 1)
            return Result<Order>.Failure(OrderErrors.GetItemsCountInvalidError(itemsCount));

        var firstCurrency = items.First().UnitPrice.Currency;

        if (items.Any(x => !x.UnitPrice.Currency.Equals(firstCurrency)))
            return Result<Order>.Failure(OrderErrors.GetItemsCurrenciesMismatchError());

        var total = CalculateTotalPrice(items);

        // общая стоимость всех позиций заказа больше нуля
        if (total.Sum <= 0)
            return Result<Order>.Failure(OrderErrors.GetItemsTotalBelowZeroError(total.Sum));

        var createdAt = DateTime.UtcNow;
        var orderId = OrderId.New();

        var order = new Order(
            id: orderId,
            clientId: clientId,
            currency: firstCurrency,
            status: OrderStatus.PendingPayment,
            items: items,
            address: address,
            creationDateUtc: createdAt);

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
        _frozenTotalPrice = CalculateTotalPrice(Items);

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
        _frozenTotalPrice = CalculateTotalPrice(Items);

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

    private static Price CalculateTotalPrice(IReadOnlyList<OrderItem> items)
    {
        // при текущих инвариантах это невозможно, но станет актуально в случае, если добавятся функции добавления/удаления позиции товара
        if (items.Count == 0)
            throw new InvariantViolationException(
                OrderExceptionalErrors.GetOrderContainsNoItemsError());

        var currency = items.First().UnitPrice.Currency;

        var total = Price.Zero(currency);

        foreach (var item in items)
            total = total.Plus(item.GetTotalPrice());

        return total;
    }
}