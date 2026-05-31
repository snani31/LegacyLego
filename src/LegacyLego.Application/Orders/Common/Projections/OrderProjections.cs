using LegacyLego.Application.Orders.Queries.OrderDetails;
using LegacyLego.Domain.Aggregates;
using System.Linq.Expressions;

namespace LegacyLego.Application.Orders.Common.Projections;

public static class OrderProjections
{
    public static Expression<Func<Order,OrderSummaryDto>> Summary =>
        order => new OrderSummaryDto(
            order.Id.Value,
            order.Status,
            order.Items.Sum(x => x.UnitPrice.Sum),
            order.Currency.Code,
            order.CreationDateUtc,
            order.Items.Count
        );

    public static Expression<Func<Order, OrderDetailsDto>> Details =>
            order => new OrderDetailsDto(
                order.Id.Value,
                order.Status,
                order.CreationDateUtc,
                new AddressDetailsDto(
                    order.Address.Country,
                    order.Address.City,
                    order.Address.Street,
                    order.Address.PostalCode),
                order.Items.Select(item => new OrderItemDetailsDto(
                    item.ProductId,
                    item.Title,
                    item.Quantity,
                    item.UnitPrice.Sum,
                    item.UnitPrice.Sum * item.Quantity)),
                order.TotalPrice.Sum,
                order.Currency.Code
            );
}