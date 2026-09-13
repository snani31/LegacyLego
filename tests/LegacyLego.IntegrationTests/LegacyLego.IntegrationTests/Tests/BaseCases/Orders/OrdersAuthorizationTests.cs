using LegacyLego.Application.Orders.Common;
using LegacyLego.IntegrationTests.Tests.BaseCases.Fixtures;
using LegacyLego.Presentation.Orders.Dto;
using System.Net;
using System.Net.Http.Json;

namespace LegacyLego.IntegrationTests.Tests.BaseCases.Orders;

[ClassDataSource<DefaultWebApplicationFactory>(Shared = SharedType.Keyed, Key = "Postgres-Pool-1")]
public class OrdersAuthorizationTests : BaseIntegrationTest<DefaultWebApplicationFactory>
{
    public OrdersAuthorizationTests(DefaultWebApplicationFactory factory) : base(factory) { }

    [Test]
    public async Task CreateOrder_WithWrongRole_ReturnsForbidden()
    {
        // ARRANGE: Клиент с неподходящей ролью 
        var wrongAuthClient = CreateClient("not-client");

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
        var response = await wrongAuthClient.PostAsJsonAsync("/orders", request);

        // ASSERT: Проверяем 403 Forbidden
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Forbidden);
    }

    [Test]
    public async Task CreateOrder_WithoutAuth_ReturnsUnauthorized()
    {
        // ARRANGE: Полностью неавторизованный клиент
        var anonymousClient = CreateUnauthenticatedClient();

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
        var response = await anonymousClient.PostAsJsonAsync("/orders", request);

        // ASSERT: Проверяем 401 Unauthorized
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Unauthorized);
    }
}