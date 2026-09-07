using Docker.DotNet.Models;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.IntegrationTests.Infrastructure.Authentication;
using LegacyLego.IntegrationTests.Tests.BaseCases.Fixtures;
using LegacyLego.Presentation.Orders.Dto;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;

namespace LegacyLego.IntegrationTests.Tests.BaseCases.Orders;

[ClassDataSource<DefaultWebApplicationFactory>(Shared = SharedType.Keyed, Key = "Postgres-Pool-1")]
public class CreateOrderTests : BaseIntegrationTest
{
    // TUnit автоматически внедрит DefaultWebApplicationFactory через конструктор
    public CreateOrderTests(DefaultWebApplicationFactory factory)
        : base(factory)
    {

    }

    [Test]
    public async Task CreateOrder_WithValidRequest_ReturnsCreatedAndSavesToDatabase()
    {
        // ARRANGE
        var request = new CreateOrderRequest(
            CurrencyCode: "USD",
            OrderAddress: new OrderAddressDto("USA", "New York", "5th Avenue", "10001"),
            Items: new List<OrderItemDto>
            {
                new OrderItemDto("Lego Star Wars Millenium Falcon", 1, Guid.NewGuid(), 799.99m)
            }
        );

        // ACT
        var response = await Client.PostAsJsonAsync("/orders", request);

        if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.BadRequest)
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Error Output: {errorDetails}");
        }

        // ASSERT: Проверяем HTTP-ответ
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Created);

        var createdOrderId = await response.Content.ReadFromJsonAsync<Guid>();
        await Assert.That(createdOrderId).IsNotEqualTo(Guid.Empty);

        var savedOrder = await DbContext.Orders
            .FirstOrDefaultAsync(o => o.Id == OrderId.From(createdOrderId));

        await Assert.That(savedOrder).IsNotNull();
        await Assert.That(savedOrder!.ClientId.Value).IsEqualTo(TestAuthHandler.TestUserId);
    }

    [Test]
    public async Task CreateOrder_WithEmptyItems_ReturnsBadRequest()
    {
        // ARRANGE: Корзина пуста
        var request = new CreateOrderRequest("USD", new OrderAddressDto("USA", "NY", "St", "100"), Items: new());

        // ACT
        var response = await Client.PostAsJsonAsync("/orders", request);

        if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.BadRequest)
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Error Output: {errorDetails}");
        }

        // ASSERT
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
    }
}