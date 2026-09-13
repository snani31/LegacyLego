using LegacyLego.IntegrationTests.Infrastructure.Factories;
using LegacyLego.IntegrationTests.Tests.BaseCases.Fixtures;
using System.Net;

namespace LegacyLego.IntegrationTests.Tests.BaseCases.OrderPayment;

[ClassDataSource<DefaultWebApplicationFactory>(Shared = SharedType.Keyed, Key = "Postgres-OrderPatyment")]
public class OrderPaymentAuthorizationTests : BaseIntegrationTest<DefaultWebApplicationFactory>
{
    public OrderPaymentAuthorizationTests(DefaultWebApplicationFactory factory) : base(factory) { }

    [Test]
    public async Task StartPayment_WithWrongRole_ReturnsForbidden()
    {
        // ARRANGE: Клиент с неподходящей ролью 
        var wrongAuthClient = CreateClient("not-client");

        // ACT
        var response = await wrongAuthClient.PostAsync($"/mock/{Guid.NewGuid()}/pay", content: null);

        // ASSERT: Проверяем 403 Forbidden
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Forbidden);
    }

    [Test]
    public async Task StartPayment_WithoutAuth_ReturnsUnauthorized()
    {
        // ARRANGE: Полностью неавторизованный клиент
        var anonymousClient = CreateUnauthenticatedClient();

        // ACT
        var response = await anonymousClient.PostAsync($"/mock/{Guid.NewGuid()}/pay", content: null);

        // ASSERT: Проверяем 401 Unauthorized
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task StartPayment_WhenOrderBelongsToAnotherClient_ReturnsForbidden()
    {
        // ARRANGE
        var ownerUserId = Guid.NewGuid();
        var attackerUserId = Guid.NewGuid();

        // Запрос будет отправляться от лица attackerUserId
        var attackerClient = CreateClient(userId: attackerUserId);

        // Но заказ в БД создается для ownerUserId
        var owner = ClientFactory.Create(id: ownerUserId);
        var order = OrderFactory.CreatePendingOrder(clientId: owner.Id.Value);

        DbContext.Clients.Add(owner);
        DbContext.Orders.Add(order);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        // ACT: Атакующий пытается оплатить заказ владельца
        var response = await attackerClient.PostAsync($"/mock/{order.Id.Value}/pay", content: null);

        // ASSERT
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Forbidden);
    }
}