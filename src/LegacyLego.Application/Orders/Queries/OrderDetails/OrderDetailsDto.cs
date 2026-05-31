using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Queries.OrderDetails;

public sealed record OrderDetailsDto(
    Guid OrderId,
    OrderStatus Status,
    DateTime CreatedAt,
    AddressDetailsDto DeliveryAddress,
    IEnumerable<OrderItemDetailsDto> Items,
    decimal TotalAmount,
    string Currency
);

public sealed record OrderItemDetailsDto(
    Guid ProductId,
    string Title,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);

public sealed record AddressDetailsDto(
    string Country,
    string City,
    string Street,
    string PostalCode
);