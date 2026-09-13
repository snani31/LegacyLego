using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.IntegrationTests.Infrastructure.Factories;
using LegacyLego.IntegrationTests.Tests.BaseCases.Fixtures;
using LegacyLego.Presentation.Mock.Common.Dto.Request;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;

namespace LegacyLego.IntegrationTests.Tests.BaseCases.OrderPayment;

[ClassDataSource<DefaultWebApplicationFactory>(Shared = SharedType.Keyed, Key = "Postgres-OrderPatyment")]
public class ProcessPaymentWebhookTests : BaseIntegrationTest<DefaultWebApplicationFactory>
{
    public ProcessPaymentWebhookTests(DefaultWebApplicationFactory factory): base(factory) { }

    [Test]
    public async Task HandleWebhook_WithValidSuccessStatus_UpdatesPaymentToSucceeded()
    {
        // ARRANGE
        var client = ClientFactory.Create();
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);

        var expectedPrice = Price.Create(799.99m, Currency.FromCode("USD").Value).Value;
        var externalSessionId = $"ext_{Guid.NewGuid():N}";
        var transactionId = $"tx_{Guid.NewGuid():N}";

        var payment = OrderPaymentFactory.CreatePendingPayment(order.Id, expectedPrice, externalSessionId);

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        DbContext.OrderPayments.Add(payment);

        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var webhookPayload = new PaymentProviderWebhookRequest(
            ExternalSessionId: externalSessionId,
            TransactionId: transactionId,
            OrderId: order.Id.Value,
            Amount: 799.99m,
            Currency: "USD",
            Status: "success"
        );

        // Вебхуки шлются внешним провайдером без заголовков пользователя
        var webhookClient = CreateUnauthenticatedClient();

        // ACT
        var response = await webhookClient.PostAsJsonAsync("/mock/api/webhooks/payment", webhookPayload);

        // ASSERT: Проверка HTTP-ответа
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        // ASSERT: Проверка состояния агрегата OrderPayment в БД
        var updatedPayment = await DbContext.OrderPayments
            .FirstOrDefaultAsync(p => p.Id == payment.Id);

        await Assert.That(updatedPayment).IsNotNull();
        await Assert.That(updatedPayment!.Status).IsEqualTo(PaymentStatus.Succeeded);
        await Assert.That(updatedPayment.TransactionId).IsEqualTo(transactionId);
        await Assert.That(updatedPayment.ActualAmount).IsEqualTo(expectedPrice);
    }

    [Test]
    public async Task HandleWebhook_WithValidReapetedRequest_IdempotencyWithSucceed()
    {
        // ARRANGE
        var client = ClientFactory.Create();
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);

        var expectedPrice = Price.Create(799.99m, Currency.FromCode("USD").Value).Value;
        var externalSessionId = $"ext_{Guid.NewGuid():N}";
        var transactionId = $"tx_{Guid.NewGuid():N}";

        var payment = OrderPaymentFactory.CreatePendingPayment(order.Id, expectedPrice, externalSessionId);

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        DbContext.OrderPayments.Add(payment);

        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var webhookPayload = new PaymentProviderWebhookRequest(
            ExternalSessionId: externalSessionId,
            TransactionId: transactionId,
            OrderId: order.Id.Value,
            Amount: 799.99m,
            Currency: "USD",
            Status: "success"
        );

        // Вебхуки шлются внешним провайдером без заголовков пользователя
        var webhookClient = CreateUnauthenticatedClient();

        // ACT
        var response = await webhookClient.PostAsJsonAsync("/mock/api/webhooks/payment", webhookPayload);
        // Второй аналогичный запрос
        var secondResponse = await webhookClient.PostAsJsonAsync("/mock/api/webhooks/payment", webhookPayload);

        // ASSERT
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        // Если ранее идентичный запрос уже был обработан как OK, то и этот OK (идемпотентная операция)
        await Assert.That(secondResponse.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var updatedPayment = await DbContext.OrderPayments
            .FirstOrDefaultAsync(p => p.Id == payment.Id);

        await Assert.That(updatedPayment).IsNotNull();
        await Assert.That(updatedPayment!.Status).IsEqualTo(PaymentStatus.Succeeded);
        await Assert.That(updatedPayment.TransactionId).IsEqualTo(transactionId);
        await Assert.That(updatedPayment.ActualAmount).IsEqualTo(expectedPrice);
    }

    [Test]
    public async Task HandleWebhook_WithValidFailedStatus_UpdatesPaymentToFailed()
    {
        // ARRANGE
        var client = ClientFactory.Create();
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);

        var expectedPrice = Price.Create(799.99m, Currency.FromCode("USD").Value).Value;
        var externalSessionId = $"ext_{Guid.NewGuid():N}";
        var transactionId = $"tx_{Guid.NewGuid():N}";

        var payment = OrderPaymentFactory.CreatePendingPayment(order.Id, expectedPrice, externalSessionId);

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        DbContext.OrderPayments.Add(payment);

        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var webhookPayload = new PaymentProviderWebhookRequest(
            ExternalSessionId: externalSessionId,
            TransactionId: null, // для неудачной операции оплаты не создаётся TransactionId
            OrderId: order.Id.Value,
            Amount: 799.99m,
            Currency: "USD",
            Status: "fail"
        );

        var webhookClient = CreateUnauthenticatedClient();

        // ACT
        var response = await webhookClient.PostAsJsonAsync("/mock/api/webhooks/payment", webhookPayload);

        // ASSERT
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var updatedPayment = await DbContext.OrderPayments
            .FirstOrDefaultAsync(p => p.Id == payment.Id);

        await Assert.That(updatedPayment).IsNotNull();
        await Assert.That(updatedPayment!.Status).IsEqualTo(PaymentStatus.Failed);
        await Assert.That(updatedPayment.TransactionId).IsNull();
        await Assert.That(updatedPayment.ActualAmount).IsNull(); // так как оплаты не было, то ActualAmount не должен быть инициализирован
    }

    [Test]
    public async Task HandleWebhook_WithUnknownStatus_ReturnsBadRequest()
    {
        // ARRANGE
        var client = ClientFactory.Create();
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);

        var expectedPrice = Price.Create(799.99m, Currency.FromCode("USD").Value).Value;
        var externalSessionId = $"ext_{Guid.NewGuid():N}";
        var transactionId = $"tx_{Guid.NewGuid():N}";

        var payment = OrderPaymentFactory.CreatePendingPayment(order.Id, expectedPrice, externalSessionId);

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        DbContext.OrderPayments.Add(payment);

        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var webhookPayload = new PaymentProviderWebhookRequest(
            ExternalSessionId: externalSessionId,
            TransactionId: transactionId,
            OrderId: order.Id.Value,
            Amount: 799.99m,
            Currency: "USD",
            Status: "unknown-status" // Неизвестный статус вэбхука
        );

        var webhookClient = CreateUnauthenticatedClient();

        // ACT
        var response = await webhookClient.PostAsJsonAsync("/mock/api/webhooks/payment", webhookPayload);

        // ASSERT
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task HandleWebhook_WithUnknownIdentifiers_ReturnsNotFound()
    {
        // ARRANGE
        var client = ClientFactory.Create();
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);

        var expectedPrice = Price.Create(799.99m, Currency.FromCode("USD").Value).Value;
        var externalSessionId = $"ext_{Guid.NewGuid():N}";
        var transactionId = $"tx_{Guid.NewGuid():N}";

        var payment = OrderPaymentFactory.CreatePendingPayment(order.Id, expectedPrice, externalSessionId);

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        DbContext.OrderPayments.Add(payment);

        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var webhookPayload = new PaymentProviderWebhookRequest(
            ExternalSessionId: "unknown", //  неизвестные идентификаторы
            TransactionId: "unknown",
            OrderId: order.Id.Value,
            Amount: 799.99m,
            Currency: "USD",
            Status: "success" 
        );

        var webhookClient = CreateUnauthenticatedClient();

        // ACT
        var response = await webhookClient.PostAsJsonAsync("/mock/api/webhooks/payment", webhookPayload);

        // ASSERT
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task HandleWebhook_WithMismatchedAmount_UpdatesPaymentToRefundRequestedAndReturnsConflict()
    {
        // ARRANGE
        var client = ClientFactory.Create();
        var order = OrderFactory.CreatePendingOrder(clientId: client.Id.Value);

        var expectedPrice = Price.Create(799.99m, Currency.FromCode("USD").Value).Value;
        var externalSessionId = $"ext_{Guid.NewGuid():N}";
        var transactionId = $"tx_{Guid.NewGuid():N}";

        var payment = OrderPaymentFactory.CreatePendingPayment(order.Id, expectedPrice, externalSessionId);

        DbContext.Clients.Add(client);
        DbContext.Orders.Add(order);
        DbContext.OrderPayments.Add(payment);

        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        // Неверная сумма: 500.00m вместо 799.99m
        var webhookPayload = new PaymentProviderWebhookRequest(
            ExternalSessionId: externalSessionId,
            TransactionId: transactionId,
            OrderId: order.Id.Value,
            Amount: 500.00m,
            Currency: "USD",
            Status: "success"
        );

        var webhookClient = CreateUnauthenticatedClient();

        // 
        var response = await webhookClient.PostAsJsonAsync("/mock/api/webhooks/payment", webhookPayload);

        // ASSERT
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var updatedPayment = await DbContext.OrderPayments
            .FirstOrDefaultAsync(p => p.Id == payment.Id);

        await Assert.That(updatedPayment).IsNotNull();
        await Assert.That(updatedPayment!.Status).IsEqualTo(PaymentStatus.RefundRequested);
        await Assert.That(updatedPayment.ActualAmount).IsNotNull()
            .And.Member(a => a.Sum, s => s.IsEqualTo(500.00m));
    }
}