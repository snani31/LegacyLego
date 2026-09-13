using LegacyLego.IntegrationTests.Infrastructure.Factories;
using LegacyLego.IntegrationTests.Tests.BaseCases.Fixtures;
using LegacyLego.Presentation.Payments.Dto;
using System.Net;
using System.Net.Http.Json;

namespace LegacyLego.IntegrationTests.Tests.BaseCases.OrderPayment;

[ClassDataSource<DefaultWebApplicationFactory>(Shared = SharedType.Keyed, Key = "Postgres-OrderPatyment")]
public class StartOrderPaymentTests : BaseIntegrationTest<DefaultWebApplicationFactory>
{
    public StartOrderPaymentTests(DefaultWebApplicationFactory factory): base(factory) { }

    [Test]
    public async Task StartPayment_WithValidPendingOrder_ReturnsOkWithCheckoutUrl()
    {
        // ARRANGE

        var currentUserId = Guid.NewGuid();
        var testClient = CreateClient(userId: currentUserId);

        var client = ClientFactory.Create(id: currentUserId);
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        await DbContext.SaveChangesAsync();

        // Очистка ChangeTracker, чтобы EF Core не отдавал объект из кеша памяти при запросах внутри контроллера
        DbContext.ChangeTracker.Clear();

        // ACT
        var response = await testClient.PostAsync($"/mock/{order.Id.Value}/pay", content: null);

        // ASSERT
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var paymentResponse = await response.Content.ReadFromJsonAsync<StartPaymentResponse>();

        await Assert.That(paymentResponse).IsNotNull();
        await Assert.That(paymentResponse!.CheckoutUrl).IsNotEmpty();
        await Assert.That(paymentResponse.ExpiresAtUtc).IsGreaterThan(DateTime.UtcNow);
    }

    [Test]
    public async Task StartPayment_WhenOrderDoesNotExist_ReturnsNotFound()
    {
        // ARRANGE
        var nonExistentOrderId = Guid.NewGuid();

        // ACT: Можно использовать стандартный Client, так как до проверки владельца код даже не дойдет
        var response = await Client.PostAsync($"/mock/{nonExistentOrderId}/pay", content: null);

        // ASSERT
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task StartPayment_WithAlreadyPaidOrder_ReturnsConflict()
    {
        // ARRANGE

        var currentUserId = Guid.NewGuid();
        var testClient = CreateClient(userId: currentUserId);

        var client = ClientFactory.Create(id: currentUserId);
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);
        order.Pay();

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        await DbContext.SaveChangesAsync();

        // Очистка ChangeTracker, чтобы EF Core не отдавал объект из кеша памяти при запросах внутри контроллера
        DbContext.ChangeTracker.Clear();

        // ACT
        var response = await testClient.PostAsync($"/mock/{order.Id.Value}/pay", content: null);

        // ASSERT
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Conflict);
    }

}