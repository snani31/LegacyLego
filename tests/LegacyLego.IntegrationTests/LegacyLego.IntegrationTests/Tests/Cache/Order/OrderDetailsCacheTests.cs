using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Orders.Queries.OrderDetails;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Context;
using LegacyLego.IntegrationTests.Infrastructure.Extensions;
using LegacyLego.IntegrationTests.Infrastructure.Factories;
using LegacyLego.IntegrationTests.Tests.Cache.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace LegacyLego.IntegrationTests.Tests.Cache.Order;

[ClassDataSource<CacheWebApplicationFactory>(Shared = SharedType.Keyed, Key = "OrderDetails-Cache-Pool")]
public class OrderDetailsCacheTests : BaseIntegrationTest<CacheWebApplicationFactory>
{
    public OrderDetailsCacheTests(CacheWebApplicationFactory factory) : base(factory) { }

    [Test]
    public async Task GetOrderDetails_WhenCacheIsEmpty_PopulatesRedisCache()
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
        var response = await httpClient.GetAsync($"/orders/{order.Id.Value}");
        var details = await response.ReadJsonAsync<OrderDetailsDto>();

        // ASSERT
        await Assert.That(response.IsSuccessStatusCode).IsTrue();
        await Assert.That(details!.OrderId).IsEqualTo(order.Id.Value);

        var version = await Factory.GetRedisStringAsync($"order:{order.Id.Value}:version");
        var hasCacheKey = await Factory.KeyExistsInRedisAsync($"order:{order.Id.Value}:v1:details");

        await Assert.That(version).IsEqualTo("1");
        await Assert.That(hasCacheKey).IsTrue();
    }

    [Test]
    public async Task GetOrderDetails_WhenCacheIsWarmed_ReturnsDataFromCacheWithoutReadingDb()
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

        // Прогреваем кэш первичным запросом
        await httpClient.GetAsync($"/orders/{order.Id.Value}");

        // "Портим" данные в БД напрямую
        var dirtyAddress = OrderAddress.Create("Ruined City", "Ghost Street", "13", "00000").Value;
        await DbContext.Orders
            .Where(o => o.Id == order.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(o => o.Address, dirtyAddress));

        // ACT
        var response = await httpClient.GetAsync($"/orders/{order.Id.Value}");
        var details = await response.ReadJsonAsync<OrderDetailsDto>();

        // ASSERT: из кэша вернулся старый адрес "New York"
        await Assert.That(response.IsSuccessStatusCode).IsTrue();
        await Assert.That(details!.DeliveryAddress.City).IsEqualTo("New York");
    }

    [Test]
    public async Task SaveChangesAsync_OnOrderUpdate_IncrementsCacheVersionInRedis()
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
        await httpClient.GetAsync($"/orders/{order.Id.Value}"); // Версия становится "1"

        // ACT: меняем состояние через UoW
        using (var scope = Factory.Services.CreateScope())
        {
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var scopedDb = scope.ServiceProvider.GetRequiredService<OrderContext>();

            var orderToUpdate = await scopedDb.Orders.FirstAsync(o => o.Id == order.Id);
            orderToUpdate.Refund();

            await uow.SaveChangesAsync();
        }

        // ASSERT: версия в Redis инкрементировалась до "2"
        var version = await Factory.GetRedisStringAsync($"order:{order.Id.Value}:version");
        await Assert.That(version).IsEqualTo("2");
    }

    [Test]
    public async Task SaveChangesAsync_OnSingleOrderUpdate_DoesNotIncrementOtherOrdersVersion()
    {
        // ARRANGE
        var currentUserId = Guid.NewGuid();
        var client = ClientFactory.Create(id: currentUserId);

        var orderA = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);
        var orderB = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);
        orderA.Pay();
        orderB.Pay();

        DbContext.Clients.Add(client);
        DbContext.Orders.AddRange(orderA, orderB);
        await DbContext.SaveChangesAsync();

        var httpClient = CreateClient(userId: currentUserId);

        // Прогреваем кэш обоих заказов (оба получают версию "1")
        await httpClient.GetAsync($"/orders/{orderA.Id.Value}");
        await httpClient.GetAsync($"/orders/{orderB.Id.Value}");

        // ACT: меняем состояние ТОЛЬКО первого заказа через UoW
        using (var scope = Factory.Services.CreateScope())
        {
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var scopedDb = scope.ServiceProvider.GetRequiredService<OrderContext>();

            var orderToUpdate = await scopedDb.Orders.FirstAsync(o => o.Id == orderA.Id);
            orderToUpdate.Refund();

            // группа заказа A обновлена версия +1 (с 1 до 2)
            // группа списка заказов текущего клиента обновлена версия +1 (с 0 до 1)
            await uow.SaveChangesAsync();
        }

        // ASSERT
        var versionOrderA = await Factory.GetRedisStringAsync($"order:{orderA.Id.Value}:version");
        var versionOrderB = await Factory.GetRedisStringAsync($"order:{orderB.Id.Value}:version");
        var versionUserGroup = await Factory.GetRedisStringAsync($"orders:{client.Id.Value}:version");

        // Заказ A и группа обновлены
        await Assert.That(versionOrderA).IsEqualTo("2");
        await Assert.That(versionUserGroup).IsEqualTo("1");

        // Заказ B остался нетронутым в кэше
        await Assert.That(versionOrderB).IsEqualTo("1");
    }
}