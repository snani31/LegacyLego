using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices.IClientProvisioningService;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Create;

public sealed class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IClientProvisioningService clientProvisioningService,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(CreateOrderCommand command, CancellationToken ct)
    {
        // JIT-провижининг: гарантируем наличие клиента в БД
        var provisioningResult = await clientProvisioningService.EnsureExistsAsync(command.UserProfile, ct);
        if (provisioningResult.IsFailure)
            return Result<Guid>.Failure(provisioningResult.Error);

        var client = provisioningResult.Value.Client;

        var currencyResult = Currency.FromCode(command.CurrencyCode);
        if (currencyResult.IsFailure) return Result<Guid>.Failure(currencyResult.Error);
        var currency = currencyResult.Value;

        var addressResult = OrderAddress.Create(
            command.OrderAddress.Country,
            command.OrderAddress.City,
            command.OrderAddress.Street,
            command.OrderAddress.PostalCode);
        if (addressResult.IsFailure) return Result<Guid>.Failure(addressResult.Error);
        var address = addressResult.Value;

        var itemsResult = CreateItems(command.Items, currency);
        if (itemsResult.IsFailure) return Result<Guid>.Failure(itemsResult.Error);
        var items = itemsResult.Value;
       
        var orderResult = Order.Create(address, client.Id, items);
        if (orderResult.IsFailure) return Result<Guid>.Failure(orderResult.Error);
        var order = orderResult.Value;

        orderRepository.Add(order);
        await unitOfWork.SaveChangesAsync(ct);

        return Result<Guid>.Success(order.Id.Value);
    }

    private static Result<List<OrderItem>> CreateItems(IEnumerable<OrderItemDto> requests, Currency currency)
    {
        var items = new List<OrderItem>();
        foreach (var request in requests)
        {
            var priceResult = Price.Create(request.UnitPriceAmount, currency);
            if (priceResult.IsFailure) return Result<List<OrderItem>>.Failure(priceResult.Error);

            var itemResult = OrderItem.Create(request.Title, request.Quantity, request.ProductId, priceResult.Value);
            if (itemResult.IsFailure) return Result<List<OrderItem>>.Failure(itemResult.Error);

            items.Add(itemResult.Value);
        }
        return Result<List<OrderItem>>.Success(items);
    }
}