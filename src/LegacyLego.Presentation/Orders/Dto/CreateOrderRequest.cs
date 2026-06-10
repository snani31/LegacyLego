using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Presentation.Orders.Dto;

public sealed record CreateOrderRequest(
    string CurrencyCode,
    OrderAddressDto OrderAddress,
    List<OrderItemDto> Items);