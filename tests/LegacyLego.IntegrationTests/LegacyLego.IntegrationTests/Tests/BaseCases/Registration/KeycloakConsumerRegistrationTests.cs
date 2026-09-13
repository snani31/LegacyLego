using LegacyLego.Application.Dto;
using LegacyLego.Infrastructure.Context;
using LegacyLego.Infrastructure.Options;
using LegacyLego.IntegrationTests.Infrastructure.Factories;
using LegacyLego.IntegrationTests.Infrastructure.Messaging;
using LegacyLego.IntegrationTests.Infrastructure.Pulling;
using LegacyLego.IntegrationTests.Tests.BaseCases.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LegacyLego.IntegrationTests.Tests.BaseCases.Registration;

[ClassDataSource<KeycloakRegistrationWebApplicationFactory>(Shared = SharedType.Keyed, Key = "RabbitMQ-Registration")]
public class KeycloakConsumerRegistrationTests : BaseIntegrationTest<KeycloakRegistrationWebApplicationFactory>
{
    private const string TOPIC_EXCHANGE_NAME = "amq.topic";
    private const string REGISTRATION_ROUTING_KEY = "KK.EVENT.INTEGRATION.TEST.LOCAL.USER.REGISTER";

    public KeycloakConsumerRegistrationTests(KeycloakRegistrationWebApplicationFactory factory) : base(factory) { }

    [Test]
    public async Task OnRegisterEventReceived_CreatesNewClientInDatabase()
    {
        // ARRANGE
        var registerEvent = KeycloakEventFactory.CreateUserRegistered();
        var userId = registerEvent.UserId;
        var expectedEmail = registerEvent.Details!.Email;

        // Регистрация данных по конкретному UserId (100% Thread-Safe)
        Factory.IdentityProviderFake.SetupProfile(registerEvent.ToExternalUserProfile());

        // ACT
        await Factory.PublishEventAsync(
            exchange: TOPIC_EXCHANGE_NAME,
            routingKey: REGISTRATION_ROUTING_KEY,
            eventPayload: registerEvent);

        // ASSERT проверяем наличие записи клиента в бд
        var isCreated = await TestPoller.WaitForDbContextAsync<OrderContext>(
            Factory.Services,
            dbContext => dbContext.Clients.AnyAsync(c => c.Id == userId));

        await Assert.That(isCreated).IsTrue();

        var createdClient = await DbContext.Clients.FirstOrDefaultAsync(c => c.Id == userId);
        await Assert.That(createdClient).IsNotNull();
        await Assert.That(createdClient!.Email.Value).IsEqualTo(expectedEmail);

        await Assert.That(Factory.IdentityProviderFake.WasCalledFor(userId)).IsTrue();
    }

    [Test]
    public async Task OnRegisterEventReceived_DuplicateEvent_IsIdempotentAndDoesNotDuplicate()
    {
        // ARRANGE
        var registerEvent = KeycloakEventFactory.CreateUserRegistered();
        var userId = registerEvent.UserId;

        Factory.IdentityProviderFake.SetupProfile(registerEvent.ToExternalUserProfile());

        // ACT — Отправляем событие дубликатом дважды
        await Factory.PublishEventAsync(
            exchange: TOPIC_EXCHANGE_NAME,
            routingKey: REGISTRATION_ROUTING_KEY,
            eventPayload: registerEvent);
        await Factory.PublishEventAsync(
            exchange: TOPIC_EXCHANGE_NAME,
            routingKey: REGISTRATION_ROUTING_KEY,
            eventPayload: registerEvent);

        var isCreated = await TestPoller.WaitForDbContextAsync<OrderContext>(
            Factory.Services,
            async dbContext => await dbContext.Clients.CountAsync(c => c.Id == userId) == 1);

        await Assert.That(isCreated).IsTrue();
        var clientCount = await DbContext.Clients.CountAsync(c => c.Id == userId);
        await Assert.That(clientCount).IsEqualTo(1);
    }

    [Test]
    public async Task OnRegisterEventReceived_WhenIdentityProviderFails_DoesNotCreateClient()
    {
        var registerEvent = KeycloakEventFactory.CreateUserRegistered();
        var userId = registerEvent.UserId;

        Factory.IdentityProviderFake.SimulateErrorFor(userId, new HttpRequestException("Keycloak unavailable"));

        // количество ретраев после отказа + первое обращение до отказа
        var expectedCallCount = Factory.RabbitMqOptions.RetryCount + 1;
        var maxRetryDurationSeconds = (Factory.RabbitMqOptions.RetryCount * Factory.RabbitMqOptions.RetryIntervalSeconds) + 3;

        // ACT
        await Factory.PublishEventAsync(
            exchange: TOPIC_EXCHANGE_NAME,
            routingKey: REGISTRATION_ROUTING_KEY,
            eventPayload: registerEvent);

        // ASSERT Проверяем выполнение политики ретраев с динамическим таймаутом и числом попыток
        var processedAllRetries = await TestPoller.WaitForAsync(
            predicate: () => Factory.IdentityProviderFake.GetCallCountFor(userId) == expectedCallCount,
            timeout: TimeSpan.FromSeconds(maxRetryDurationSeconds));

        await Assert.That(processedAllRetries)
            .IsTrue()
            .Because($"Expected {expectedCallCount} calls to IdentityProvider " +
            $"(1 initial + {Factory.RabbitMqOptions.RetryCount} retries based on configuration)");

        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
        var clientExists = await dbContext.Clients.AnyAsync(c => c.Id == userId);
        
        // проверяем отсутствие записи клиента в бд (в случае неудачного сценария)
        await Assert.That(clientExists).IsFalse();
    }

    [Test]
    public async Task OnRegisterEventReceived_WhenDomainInvariantFails_DoesNotCreateClientAndDoesNotRetry()
    {
        // ARRANGE
        var rabbitOptions = Factory.Services.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

        var registerEvent = KeycloakEventFactory.CreateUserRegisteredWithInvalidEmail();
        var userId = registerEvent.UserId;

        // От провайдера придёт набор данных с навалидным значением (email)
        Factory.IdentityProviderFake.SetupProfile(registerEvent.ToExternalUserProfile());

        // ACT
        await Factory.PublishEventAsync(
            exchange: TOPIC_EXCHANGE_NAME,
            routingKey: REGISTRATION_ROUTING_KEY,
            eventPayload: registerEvent);

        // ASSERT 
        var wasCalled = await TestPoller.WaitForAsync(
            predicate: () => Factory.IdentityProviderFake.WasCalledFor(userId),
            timeout: TimeSpan.FromSeconds(3));

        await Assert.That(wasCalled).IsTrue();

        var callCount = Factory.IdentityProviderFake.GetCallCountFor(userId);
        await Assert.That(callCount)
            .IsEqualTo(1)
            .Because("Business/Domain errors should not trigger MassTransit retries");

        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
        var clientExists = await dbContext.Clients.AnyAsync(c => c.Id == userId);

        await Assert.That(clientExists).IsFalse();
    }
}