using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Orders.Queries.OrdersHistory;
using LegacyLego.Infrastructure.Context;
using LegacyLego.IntegrationTests.Infrastructure.Extensions;
using LegacyLego.IntegrationTests.Infrastructure.Factories;
using LegacyLego.IntegrationTests.Tests.Cache.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LegacyLego.IntegrationTests.Tests.Cache.Order;

[ClassDataSource<CacheWebApplicationFactory>(Shared = SharedType.Keyed, Key = "OrderDetails-Cache-Pool")]
public class OrdersHistoryCacheTests : BaseIntegrationTest<CacheWebApplicationFactory>
{
    public OrdersHistoryCacheTests(CacheWebApplicationFactory factory) : base(factory) { }

    [Test]
    public async Task GetOrdersHistory_WhenCacheIsEmpty_PopulatesRedisCacheWithDefaultCursor()
    {
        // ARRANGE
        var currentUserId = Guid.NewGuid();
        var client = ClientFactory.Create(id: currentUserId);
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);
        order.Pay();

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        await DbContext.SaveChangesAsync();

        var httpClient = CreateClient(userId: currentUserId);

        // ACT
        var response = await httpClient.GetAsync("/orders/history");
        var history = await response.ReadJsonAsync<OrdersHistoryResponse>();

        // ASSERT
        await Assert.That(response.IsSuccessStatusCode).IsTrue();
        await Assert.That(history!.Orders).Count().IsEqualTo(1);

        var groupVersion = await Factory.GetRedisStringAsync($"orders:{currentUserId}:version");
        var hasDefaultCacheKey = await Factory.KeyExistsInRedisAsync($"orders:{currentUserId}:v1:cursor:first");

        await Assert.That(groupVersion).IsEqualTo("1");
        await Assert.That(hasDefaultCacheKey).IsTrue();
    }

    [Test]
    public async Task GetOrdersHistory_WhenCacheIsWarmed_ReturnsCachedListWithoutReadingDb()
    {
        // ARRANGE
        var currentUserId = Guid.NewGuid();
        var client = ClientFactory.Create(id: currentUserId);
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);
        order.Pay();

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        await DbContext.SaveChangesAsync();

        var httpClient = CreateClient(userId: currentUserId);

        // Прогреваем кэш (сохраняется список из 1 заказа)
        await httpClient.GetAsync("/orders/history");

        // Добавляем второй заказ напрямую в БД в обход UoW (без инвалидации)
        var newOrder = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);
        DbContext.Orders.Add(newOrder);
        await DbContext.SaveChangesAsync();

        // ACT
        var response = await httpClient.GetAsync("/orders/history");
        var history = await response.ReadJsonAsync<OrdersHistoryResponse>();

        // ASSERT: Из кэша возвращается старый список из 1 заказа
        await Assert.That(response.IsSuccessStatusCode).IsTrue();
        await Assert.That(history!.Orders).Count().IsEqualTo(1);
    }

    [Test]
    public async Task SaveChangesAsync_OnOrderUpdate_IncrementsGroupCacheVersionInRedis()
    {
        // ARRANGE
        var currentUserId = Guid.NewGuid();
        var client = ClientFactory.Create(id: currentUserId);
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);
        order.Pay();

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        await DbContext.SaveChangesAsync();

        var httpClient = CreateClient(userId: currentUserId);
        await httpClient.GetAsync("/orders/history"); // Кэшируется под версией "1"

        // ACT: меняем заказ через UoW (триггерит инвалидатор группы)
        using (var scope = Factory.Services.CreateScope())
        {
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var scopedDb = scope.ServiceProvider.GetRequiredService<OrderContext>();

            var orderToUpdate = await scopedDb.Orders.FirstAsync(o => o.Id == order.Id);
            orderToUpdate.Refund();

            await uow.SaveChangesAsync();
        }

        // ASSERT: Версия группы пользователя выросла до "2"
        var groupVersion = await Factory.GetRedisStringAsync($"orders:{currentUserId}:version");
        await Assert.That(groupVersion).IsEqualTo("2");

        // Повторный запрос прогревает свежий кэш v2
        var response = await httpClient.GetAsync("/orders/history");
        await Assert.That(response.IsSuccessStatusCode).IsTrue();

        var hasV2CacheKey = await Factory.KeyExistsInRedisAsync($"orders:{currentUserId}:v2:cursor:first");
        await Assert.That(hasV2CacheKey).IsTrue();
    }

    [Test]
    public async Task GetOrdersHistory_WithDifferentCursors_CachesSeparatelyUnderSameGroupVersion()
    {
        // ARRANGE
        var currentUserId = Guid.NewGuid();
        var client = ClientFactory.Create(id: currentUserId);
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);
        order.Pay();

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        await DbContext.SaveChangesAsync();

        var serializer = Factory.Services.GetRequiredService<ICursorSerializer>();
        var nextCursor = serializer.Serialize((order.CreationDateUtc, order.Id.Value));

        var httpClient = CreateClient(userId: currentUserId);

        // ACT: делаем два запроса с разными курсорами
        await httpClient.GetAsync("/orders/history"); // cursor:first
        await httpClient.GetAsync($"/orders/history?cursor={Uri.EscapeDataString(nextCursor)}");

        // ASSERT: В рамках ОДНОЙ версии v1 создались два независимых кэш-ключа
        var hasFirstPageCache = await Factory.KeyExistsInRedisAsync($"orders:{currentUserId}:v1:cursor:first");
        var hasNextPageCache = await Factory.KeyExistsInRedisAsync($"orders:{currentUserId}:v1:cursor:{nextCursor}");

        await Assert.That(hasFirstPageCache).IsTrue();
        await Assert.That(hasNextPageCache).IsTrue();
    }
}