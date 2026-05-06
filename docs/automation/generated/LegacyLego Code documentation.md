# Введение
## Назначение

**LegacyLego** - Пет-проект, разрабатываемый для практики веб-разработки middle уровня с использованием DDD + Clean Architecture подхода.
В данном документе будут описаны листинги кода проекта на его актуальной версии.

---
## Версия

Актуальная версия проекта: 1.6.2
## Проекты

Все существующие на данный момент проекты в решении `LegacyLego.slnx`:

1) **LegacyLego.Domain** - Содержит доменную логику проекта, является ядром всей системы и существует, чтобы описывать бизнес-логику на уровне кода;
2) **LegacyLego.Domain.Tests** - Содержит модульные тесты **LegacyLego.Domain**.
3) **LegacyLego.Application** - Описывает use-case сценарии, обеспечивающие логистику и оркестрацию системы в отношении данных и базовые контракты для будущей инфраструктуры.

---


## Древовидная структура решения

```txt title="TreeStructure"
.
├── docker
│   ├── nginx
│   │   └── nginx.conf
│   └── docker-compose.yaml
├── docs
│   ├── architecture
│   │   └── 'state transition matrix.docx'
│   ├── automation
│   │   ├── generated
│   │   │   └── 'LegacyLego Code documentation.md'
│   │   ├── templates
│   │   │   └── 'LegacyLego Code documentation template.md'
│   │   └── automation-guide.md
│   ├── decisions
│   │   └── 0001-automation-of-code-listings.md
│   └── diagrams
│       ├── out
│       │   ├── ActivityDiagramOrderLifeCycle_v2.jpg
│       │   ├── OrderClassDiagram.jpg
│       │   ├── OrderClassDiagram_v1.jpg
│       │   ├── SolutionStructureTreeDiagram.jpg
│       │   └── StoreOrderingSystemDiagram_v2.jpg
│       └── src
│           ├── ActivityDiagramOrderLifeCycle.drawio
│           ├── OrderClassDiagram.drawio
│           ├── SolutionStructureTreeDiagram.drawio
│           └── StoreOrderingSystemDiagram.drawio
├── src
│   ├── LegacyLego.Application
│   │   ├── Abstractions
│   │   │   ├── Data
│   │   │   │   └── IUnitOfWork.cs
│   │   │   ├── ExternalServices
│   │   │   │   ├── IBackgroundJobService.cs
│   │   │   │   └── IPaymentProvider.cs
│   │   │   └── Messaging
│   │   │       ├── Command
│   │   │       │   ├── IBaseCommand.cs
│   │   │       │   ├── ICommand.cs
│   │   │       │   └── ICommandHandler.cs
│   │   │       ├── Event
│   │   │       │   ├── Domain
│   │   │       │   │   └── IDomainEventHandler.cs
│   │   │       │   └── Integration
│   │   │       │       ├── IIntegrationEvent.cs
│   │   │       │       └── IIntegrationEventPublisher.cs
│   │   │       ├── Query
│   │   │       │   ├── IQuery.cs
│   │   │       │   └── IQueryHandler.cs
│   │   │       ├── ICommandDispatcher.cs
│   │   │       └── IEventPublisher.cs
│   │   ├── Common
│   │   ├── Errors
│   │   │   └── PaymentProviderErrors.cs
│   │   ├── ExceptionalErrors
│   │   │   └── UnitOfWorkExceptionalErrors.cs
│   │   ├── Exceptions
│   │   │   ├── InfrastructureException.cs
│   │   │   ├── PersistenceException.cs
│   │   │   └── UniqueConstraintViolation.cs
│   │   ├── Orders
│   │   │   ├── Commands
│   │   │   │   ├── Cancel
│   │   │   │   │   ├── CancelletionOrderDetails.cs
│   │   │   │   │   ├── CancelOrderCommand.cs
│   │   │   │   │   └── CancelOrderCommandHandler.cs
│   │   │   │   ├── Create
│   │   │   │   │   ├── CreateOrderCommand.cs
│   │   │   │   │   ├── CreateOrderCommandHandler.cs
│   │   │   │   │   └── CreateOrderDomainEventHandler.cs
│   │   │   │   ├── Expire
│   │   │   │   │   ├── ExpirationOrderDetails.cs
│   │   │   │   │   ├── ExpireOrderCommand.cs
│   │   │   │   │   └── ExpireOrderCommandHandler.cs
│   │   │   │   ├── Pay
│   │   │   │   │   ├── PayOrderCommand.cs
│   │   │   │   │   ├── PayOrderCommandHandler.cs
│   │   │   │   │   └── PayOrderDetails.cs
│   │   │   │   └── Refund
│   │   │   │       ├── CancelletionOrderDetails.cs
│   │   │   │       ├── CancelOrderCommand.cs
│   │   │   │       └── CancelOrderCommandHandler.cs
│   │   │   ├── Common
│   │   │   │   ├── Mappers
│   │   │   │   ├── Projections
│   │   │   │   │   └── OrderProjections.cs
│   │   │   │   ├── OrderAddressDto.cs
│   │   │   │   ├── OrderItemDto.cs
│   │   │   │   └── OrderSummaryDto.cs
│   │   │   └── Queries
│   │   │       ├── ActiveOrders
│   │   │       │   ├── ActiveOrderSpecification.cs
│   │   │       │   ├── GetActiveOrdersQuery.cs
│   │   │       │   └── GetActiveOrdersQueryHandler.cs
│   │   │       ├── OrderDetails
│   │   │       │   ├── ActiveOrderSpecification.cs
│   │   │       │   ├── GetOrderDetailsQuery.cs
│   │   │       │   ├── GetOrderDetailsQueryHandler.cs
│   │   │       │   └── OrderDetailsDto.cs
│   │   │       └── OrdersHistory
│   │   │           ├── GetOrdersHistoryQuery.cs
│   │   │           ├── GetOrdersHistoryQueryHandler.cs
│   │   │           ├── OrderHistoryRequest.cs
│   │   │           ├── OrderHistorySpecification.cs
│   │   │           └── OrdersHistoryResponse.cs
│   │   ├── Payments
│   │   │   ├── Commands
│   │   │   │   ├── PocessPaymentWebhook
│   │   │   │   │   ├── OrderPaymentSucceededDomainEventHandler.cs
│   │   │   │   │   ├── ProcessPaymentDetails.cs
│   │   │   │   │   ├── ProcessPaymentErrors.cs
│   │   │   │   │   ├── ProcessPaymentWebhookCommand.cs
│   │   │   │   │   └── ProcessPaymentWebhookCommandHandler.cs
│   │   │   │   ├── RefundRequested
│   │   │   │   │   └── RefundRequestedOrderPaymentDomainEventHandler.cs
│   │   │   │   └── StartPayment
│   │   │   │       ├── StartOrderPaymentCommand.cs
│   │   │   │       ├── StartOrderPaymentCommandHandler.cs
│   │   │   │       ├── StartOrderPaymentDetails.cs
│   │   │   │       └── StartOrderPaymentErrors.cs
│   │   │   ├── Common
│   │   │   │   ├── PaymentIntegrationEventMapper.cs
│   │   │   │   ├── PaymentSession.cs
│   │   │   │   └── PaymentWebhook.cs
│   │   │   ├── IntegrationEvents
│   │   │   │   └── RefundPaymentRequestedIntegrationEvent.cs
│   │   │   └── Services
│   │   │       └── PaymentLookup.cs
│   │   └── LegacyLego.Application.csproj
│   └── LegacyLego.Domain
│       ├── Abstractions
│       │   ├── IOrderRepository.cs
│       │   └── IPaymentRepository.cs
│       ├── Aggregates
│       │   ├── Order.cs
│       │   └── OrderPayment.cs
│       ├── DomainEvents
│       │   ├── OrderCanceled.cs
│       │   ├── OrderCreated.cs
│       │   ├── OrderExpired.cs
│       │   ├── OrderPaid.cs
│       │   ├── OrderPaymentCreated.cs
│       │   ├── OrderPaymentFailed.cs
│       │   ├── OrderPaymentRefunded.cs
│       │   ├── OrderPaymentRefundedWithoutSuccess.cs
│       │   ├── OrderPaymentRefundRequested.cs
│       │   ├── OrderPaymentSucceeded.cs
│       │   └── OrderRefunded.cs
│       ├── Enums
│       │   ├── OrderAction.cs
│       │   ├── OrderStatus.cs
│       │   ├── PaymentAction.cs
│       │   └── PaymentStatus.cs
│       ├── Errors
│       │   ├── CurrencyErrors.cs
│       │   ├── OrderErrors.cs
│       │   ├── OrderItemErrors.cs
│       │   ├── OrderPaymentErrors.cs
│       │   └── PriceErrors.cs
│       ├── ExceptionalErrors
│       │   ├── CurrencyExceptionalErrors.cs
│       │   ├── OrderExceptionalErrors.cs
│       │   ├── PriceExceptionalErrors.cs
│       │   └── ResultExceptionalErrors.cs
│       ├── Exceptions
│       │   ├── InvalidDomainStateException.cs
│       │   └── InvariantViolationException.cs
│       ├── Shared
│       │   ├── AggregateRoot.cs
│       │   ├── DomainException.cs
│       │   ├── Entity.cs
│       │   ├── Error.cs
│       │   ├── ExceptionalError.cs
│       │   ├── IDomainEvent.cs
│       │   ├── Result.cs
│       │   ├── ResultT.cs
│       │   ├── Specification.cs
│       │   └── ValueObject.cs
│       ├── ValueObjects
│       │   ├── Currency.cs
│       │   ├── ExternalSession.cs
│       │   ├── OrderAddress.cs
│       │   ├── OrderId.cs
│       │   ├── OrderItem.cs
│       │   ├── OrderPaymentId.cs
│       │   └── Price.cs
│       └── LegacyLego.Domain.csproj
├── tests
│   └── LegacyLego.Domain.Tests
│       ├── Common
│       │   ├── Builders
│       │   │   └── OrderBuilder.cs
│       │   └── Factories
│       │       └── OrderDataFactory.cs
│       ├── CurrencyTests
│       │   ├── Equality
│       │   │   └── CurrencyEqualityTests.cs
│       │   └── FromCode
│       │       └── CurrencyFromCodeTests.cs
│       ├── OrderItemTests
│       │   ├── Create
│       │   │   └── OrderItemCreateTests.cs
│       │   ├── Equality
│       │   │   └── OrderItemEqualityTests.cs
│       │   └── GetTotalPriceTests
│       │       └── OrderItemGetTotalPriceTests.cs
│       ├── OrderTests
│       │   ├── Create
│       │   │   └── OrderCreateTests.cs
│       │   ├── StateTransitions
│       │   │   ├── Cancel
│       │   │   │   └── OrderCancelTests.cs
│       │   │   ├── Expire
│       │   │   │   └── OrderExpireTests.cs
│       │   │   ├── Pay
│       │   │   │   └── OrderPayTests.cs
│       │   │   └── Refund
│       │   │       └── OrderRefundTests.cs
│       │   └── TotalPrice
│       │       └── OrderTotalPriceTests.cs
│       ├── PriceTests
│       │   ├── Create
│       │   │   └── PriceCreateTests.cs
│       │   ├── Equality
│       │   │   └── PriceEqualityTests.cs
│       │   ├── MultiplyByQuantity
│       │   │   └── PriceMultiplyByQuantityTests.cs
│       │   └── Plus
│       │       └── PricePlusTests.cs
│       ├── GlobalUsings.cs
│       └── LegacyLego.Domain.Tests.csproj
├── tools
│   └── update-project-listing-docs.ps1
├── LegacyLego.slnx
└── README.md
```

---

# Кодовая база

## LegacyLego.Application

```xml title="LegacyLego.Application.csproj"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\LegacyLego.Domain\LegacyLego.Domain.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="Orders\Commands\Cancel\" />
    <Folder Include="Orders\Commands\Refund\" />
  </ItemGroup>

</Project>
```

---

### Abstractions

#### Data

```cs title="IUnitOfWork.cs"
namespace LegacyLego.Application.Abstractions.Data;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
```

---

#### ExternalServices

```cs title="IBackgroundJobService.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface IBackgroundJobService
{
    public void Schedule(IBaseCommand command, TimeSpan delay);
}
```

---

```cs title="IPaymentProvider.cs"
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.ExternalServices;

public interface IPaymentProvider
{
    public Task<Result<PaymentSession>> CreatePaymentSessionAsync(
        Guid paymentId,
        decimal amount,
        string currency,
        CancellationToken ct);

    public Task<Result<PaymentSession>> GetExistingSessionAsync(
        Guid paymentId,
        CancellationToken ct);
}
```

---

#### Messaging

```cs title="ICommandDispatcher.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging;

public interface ICommandDispatcher
{
    public Task<Result<TResult>> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand<TResult>;

    public Task<Result> DispatchAsync<TCommand>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand;
}
```

---

```cs title="IEventPublisher.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging;

public interface IEventPublisher
{
    public Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken ct = default)
        where TEvent : IDomainEvent;
}
```

---

##### Command

```cs title="IBaseCommand.cs"
namespace LegacyLego.Application.Abstractions.Messaging.Command;

public interface IBaseCommand;
```

---

```cs title="ICommand.cs"
namespace LegacyLego.Application.Abstractions.Messaging.Command;

public interface ICommand : IBaseCommand;

public interface ICommand<TResponse> : IBaseCommand;
```

---

```cs title="ICommandHandler.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging.Command;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    public Task<Result> HandleAsync(TCommand command,CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
```

---

##### Event

###### Domain

```cs title="IDomainEventHandler.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging.Event.Domain;

public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public Task HandleAsync(TDomainEvent notification, CancellationToken cancellationToken);
}
```

---

###### Integration

```cs title="IIntegrationEvent.cs"
namespace LegacyLego.Application.Abstractions.Messaging.Event.Integration;

public interface IIntegrationEvent
{
    public DateTime OccurredOnUtc { get; }
}
```

---

```cs title="IIntegrationEventPublisher.cs"
namespace LegacyLego.Application.Abstractions.Messaging.Event.Integration;

public interface IIntegrationEventPublisher
{
    public Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken ct);
}
```

---

##### Query

```cs title="IQuery.cs"
namespace LegacyLego.Application.Abstractions.Messaging.Query;

public interface IQuery<TResponse>;
```

---

```cs title="IQueryHandler.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Abstractions.Messaging.Query;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    public Task<Result<TResponse>> HandleAsync(TQuery query,CancellationToken cancellationToken);
}
```

---

### Errors

```cs title="PaymentProviderErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Errors;

public static class PaymentProviderErrors
{
    public const string SessionNotFoundCode = "PaymentProvider.SessionNotFoundByPaymentId";

    public static Error GetSessionNotFoundByPaymentIdError(Guid paymentId)
    {
        return new(
            Code: SessionNotFoundCode,
            Message: $"По следующему OrderPayment Id: {paymentId} не было найдено ни одной активной сессии оплаты");
    }

}
```

---

### ExceptionalErrors

```cs title="UnitOfWorkExceptionalErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.ExceptionalErrors;

public static class UnitOfWorkExceptionalErrors
{
    public const string DatabaseSaveErrorCode = "UnitOfWork.SaveError";

    public static ExceptionalError GetDatabaseSaveError(Guid orderId, string internalMessage)
    {
        return new(
            Code: DatabaseSaveErrorCode,
            Message: $"Критическая ошибка при сохранении заказа {orderId}. Внутренняя ошибка: {internalMessage}"
        );
    }
}
```

---

### Exceptions

```cs title="InfrastructureException.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Exceptions;

public abstract class InfrastructureException : Exception
{
    public ExceptionalError Error { get; }

    protected InfrastructureException(ExceptionalError error)
        : base(error.Code + ": " + error.Message)
    {
        Error = error;
    }
}
```

---

```cs title="PersistenceException.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Exceptions;

public class PersistenceException : InfrastructureException
{
    public PersistenceException(ExceptionalError error) : base(error) { }
}
```

---

```cs title="UniqueConstraintViolation.cs"
using LegacyLego.Domain.Shared;
namespace LegacyLego.Application.Exceptions;

public class UniqueConstraintViolation : InfrastructureException
{
    public UniqueConstraintViolation(ExceptionalError error) : base(error) { }
}
```

---

### Orders

#### Commands

##### Cancel

```cs title="CancelletionOrderDetails.cs"
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed record CancelletionOrderDetails
{
    public const string AlreadyCancelledDetailsCode = "Order.Cancelletion.AlreadyCancelled";
    public const string CancelledSuccessfullyCode = "Order.Cancelletion.CancelledSuccessfully";
    public const string WrongStatusTransitionCode = "Order.Cancelletion.WrongStatusTransition";

    public readonly string Code;
    public readonly Guid OrderId;
    public readonly string Message;
    public readonly string CurrentStatus;
    public readonly bool StateChanged;

    private CancelletionOrderDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static CancelletionOrderDetails GetAlreadyCancelledDetails(Guid orderId)
    {
        return new CancelletionOrderDetails(
            Code: AlreadyCancelledDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already cancelled",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            false);
    }

    internal static CancelletionOrderDetails GetCancelledSuccessfullyDetails(Guid orderId)
    {
        return new CancelletionOrderDetails(
            Code: CancelledSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully cancelled",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            true);
    }

    internal static CancelletionOrderDetails GetWrongStatusTransitionDetails(Guid orderId, OrderStatus currentStatus)
    {
        return new CancelletionOrderDetails(
            Code: WrongStatusTransitionCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} Has a status:{currentStatus.ToString()} of not suitable for cancelletion",
            CurrentStatus: currentStatus.ToString(),
            false);
    }
}
```

---

```cs title="CancelOrderCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed record CancelOrderCommand(Guid OrderId) : ICommand<CancelletionOrderDetails>;
```

---

```cs title="CancelOrderCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed class CancelOrderCommandHandler(
IOrderRepository orderRepository,
IUnitOfWork unitOfWork) : ICommandHandler<CancelOrderCommand, CancelletionOrderDetails>
{
    public async Task<Result<CancelletionOrderDetails>> HandleAsync(CancelOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<CancelletionOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Cancel();
        if (result.IsFailure && order.Status is OrderStatus.Cancelled)
            return Result<CancelletionOrderDetails>.Success(CancelletionOrderDetails.GetAlreadyCancelledDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<CancelletionOrderDetails>.Success(CancelletionOrderDetails.GetWrongStatusTransitionDetails(orderIdGuid, order.Status));

        await unitOfWork.SaveChangesAsync(ct);
        return Result<CancelletionOrderDetails>.Success(CancelletionOrderDetails.GetCancelledSuccessfullyDetails(orderIdGuid));
    }
}
```

---

##### Create

```cs title="CreateOrderCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Commands.Create;

public sealed record CreateOrderCommand(
    Guid ClientId,
    string CurrencyCode,
    OrderAddressDto OrderAddress,
    List<OrderItemDto> Items) : ICommand<Guid>;
```

---

```cs title="CreateOrderCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Create;

public sealed class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(CreateOrderCommand command, CancellationToken ct)
    {
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
       
        var orderResult = Order.Create(address, command.ClientId, items);
        if (orderResult.IsFailure) return Result<Guid>.Failure(orderResult.Error);
        var order = orderResult.Value;

        orderRepository.Add(order);
        await unitOfWork.SaveChangesAsync(ct);

        return Result<Guid>.Success(orderResult.Value.Id.Value);
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
```

---

```cs title="CreateOrderDomainEventHandler.cs"
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Orders.Commands.Expire;
using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Orders.Commands.Create;

public class CreateOrderDomainEventHandler(IBackgroundJobService jobService)
: IDomainEventHandler<OrderCreated>
{
    public Task HandleAsync(OrderCreated notification, CancellationToken ct)
    {
        jobService.Schedule(new ExpireOrderCommand(notification.OrderId.Value), TimeSpan.FromMinutes(10));
        return Task.CompletedTask;
    }
}
```

---

##### Expire

```cs title="ExpirationOrderDetails.cs"
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Expire;

public sealed record ExpirationOrderDetails
{
    public const string AlreadyExpiredDetailsCode = "Order.Expiretion.AlreadyExpired";
    public const string ExpiredSuccessfullyCode = "Order.Expiretion.ExpiredSuccessfully";
    public const string WrongStatusTransitionCode = "Order.Expiretion.WrongStatusTransition";

    public readonly string Code;
    public readonly Guid OrderId;
    public readonly string Message;
    public readonly string CurrentStatus;
    public readonly bool StateChanged;

    private ExpirationOrderDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static ExpirationOrderDetails GetAlreadyExpiredDetails(Guid orderId)
    {
        return new ExpirationOrderDetails(
            Code: AlreadyExpiredDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already expired",
            CurrentStatus: OrderStatus.Expired.ToString(),
            false);
    }

    internal static ExpirationOrderDetails GetExpiredSuccessfullyDetails(Guid orderId)
    {
        return new ExpirationOrderDetails(
            Code: ExpiredSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully expired",
            CurrentStatus: OrderStatus.Expired.ToString(),
            true);
    }

    internal static ExpirationOrderDetails GetWrongStatusTransitionDetails(Guid orderId, OrderStatus currentStatus)
    {
        return new ExpirationOrderDetails(
            Code: WrongStatusTransitionCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} Has a status:{currentStatus.ToString()} of not suitable for expiration",
            CurrentStatus: currentStatus.ToString(),
            false);
    }
}
```

---

```cs title="ExpireOrderCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Orders.Commands.Expire;

public sealed record ExpireOrderCommand(Guid OrderId) : ICommand<ExpirationOrderDetails>;
```

---

```cs title="ExpireOrderCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Commands.Pay;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Expire;

public sealed class ExpireOrderCommandHandler(
IOrderRepository orderRepository,
IUnitOfWork unitOfWork) : ICommandHandler<ExpireOrderCommand, ExpirationOrderDetails>
{
    public async Task<Result<ExpirationOrderDetails>> HandleAsync(ExpireOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);
        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<ExpirationOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Expire();
        if (result.IsFailure && order.Status is OrderStatus.Expired) 
            return Result<ExpirationOrderDetails>.Success(ExpirationOrderDetails.GetAlreadyExpiredDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<ExpirationOrderDetails>.Success(ExpirationOrderDetails.GetWrongStatusTransitionDetails(orderIdGuid, order.Status));

        await unitOfWork.SaveChangesAsync(ct);
        return Result<ExpirationOrderDetails>.Success(ExpirationOrderDetails.GetExpiredSuccessfullyDetails(orderIdGuid));
    }
}
```

---

##### Pay

```cs title="PayOrderCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Commands.Cancel;
using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Commands.Pay;

public sealed record PayOrderCommand(Guid OrderId) : ICommand<PayOrderDetails>;
```

---

```cs title="PayOrderCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.ExceptionalErrors;
using LegacyLego.Application.Exceptions;
using LegacyLego.Application.Orders.Commands.Cancel;
using LegacyLego.Application.Orders.Commands.Pay;
using LegacyLego.Application.Orders.Commands.Refund;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Pay;

public sealed class PayOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<PayOrderCommand, PayOrderDetails>
{
    public async Task<Result<PayOrderDetails>> HandleAsync(PayOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<PayOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Pay();
        if (result.IsFailure && order.Status is OrderStatus.Paid)
            return Result<PayOrderDetails>.Success(PayOrderDetails.GetAlreadyPaidDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<PayOrderDetails>.Success(PayOrderDetails.GetWrongStatusTransitionDetails(orderIdGuid, order.Status));

        await unitOfWork.SaveChangesAsync(ct);
        return Result<PayOrderDetails>.Success(PayOrderDetails.GetPaidSuccessfullyDetails(orderIdGuid));
    }

}
```

---

```cs title="PayOrderDetails.cs"
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed record PayOrderDetails
{
    public const string AlreadyPaidDetailsCode = "Order.Payment.AlreadyPaid";
    public const string PaidSuccessfullyCode = "Order.Payment.PaidSuccessfully";
    public const string WrongStatusTransitionCode = "Order.Payment.WrongStatusTransition";

    public readonly string Code;
    public readonly Guid OrderId;
    public readonly string Message;
    public readonly string CurrentStatus;
    public readonly bool StateChanged;

    private PayOrderDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static PayOrderDetails GetAlreadyPaidDetails(Guid orderId)
    {
        return new PayOrderDetails(
            Code: AlreadyPaidDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already paid",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            false);
    }

    internal static PayOrderDetails GetPaidSuccessfullyDetails(Guid orderId)
    {
        return new PayOrderDetails(
            Code: PaidSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully paid",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            true);
    }

    internal static PayOrderDetails GetWrongStatusTransitionDetails(Guid orderId, OrderStatus currentStatus)
    {
        return new PayOrderDetails(
            Code: WrongStatusTransitionCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} Has a status:{currentStatus.ToString()} of not suitable for payment",
            CurrentStatus: currentStatus.ToString(),
            false);
    }
}
```

---

##### Refund

```cs title="CancelletionOrderDetails.cs"
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Commands.Refund;

public sealed record RefundOrderDetails
{
    public const string AlreadyRefundedDetailsCode = "Order.Refund.AlreadyRefunded";
    public const string RefundedSuccessfullyCode = "Order.Refund.RefundedSuccessfully";
    public const string WrongStatusTransitionCode = "Order.Refund.WrongStatusTransition";

    public readonly string Code;
    public readonly Guid OrderId;
    public readonly string Message;
    public readonly string CurrentStatus;
    public readonly bool StateChanged;

    private RefundOrderDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static RefundOrderDetails GetAlreadyRefundedDetails(Guid orderId)
    {
        return new RefundOrderDetails(
            Code: AlreadyRefundedDetailsCode,
            OrderId: orderId,
            Message: $"Order with id: {orderId} is already refunded",
            CurrentStatus: OrderStatus.Refunded.ToString(),
            false);
    }

    internal static RefundOrderDetails GetRefundedSuccessfullyDetails(Guid orderId)
    {
        return new RefundOrderDetails(
            Code: RefundedSuccessfullyCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} is successfully refunded",
            CurrentStatus: OrderStatus.Cancelled.ToString(),
            true);
    }

    internal static RefundOrderDetails GetWrongStatusTransitionDetails(Guid orderId, OrderStatus currentStatus)
    {
        return new RefundOrderDetails(
            Code: WrongStatusTransitionCode,
            OrderId: orderId,
            Message: $"Order with id:{orderId} Has a status:{currentStatus.ToString()} of not suitable for refund",
            CurrentStatus: currentStatus.ToString(),
            false);
    }
}
```

---

```cs title="CancelOrderCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;

namespace LegacyLego.Application.Orders.Commands.Refund;

public sealed record RefundOrderCommand(Guid OrderId) : ICommand<RefundOrderDetails>;
```

---

```cs title="CancelOrderCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Orders.Commands.Refund;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Commands.Cancel;

public sealed class RefundOrderCommandHandler(
IOrderRepository orderRepository,
IUnitOfWork unitOfWork) : ICommandHandler<RefundOrderCommand, RefundOrderDetails>
{
    public async Task<Result<RefundOrderDetails>> HandleAsync(RefundOrderCommand command, CancellationToken ct)
    {
        var orderIdGuid = command.OrderId;
        var orderId = OrderId.From(orderIdGuid);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null) return Result<RefundOrderDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var result = order.Refund();
        if (result.IsFailure && order.Status is OrderStatus.Refunded)
            return Result<RefundOrderDetails>.Success(RefundOrderDetails.GetAlreadyRefundedDetails(orderIdGuid));
        else if (result.IsFailure)
            return Result<RefundOrderDetails>.Success(RefundOrderDetails.GetWrongStatusTransitionDetails(orderIdGuid, order.Status));

        await unitOfWork.SaveChangesAsync(ct);
        return Result<RefundOrderDetails>.Success(RefundOrderDetails.GetRefundedSuccessfullyDetails(orderIdGuid));
    }
}
```

---

#### Common

```cs title="OrderAddressDto.cs"
namespace LegacyLego.Application.Orders.Common;

public sealed record OrderAddressDto(
    string Country,
    string City,
    string Street,
    string PostalCode);
```

---

```cs title="OrderItemDto.cs"
namespace LegacyLego.Application.Orders.Common;

public sealed record OrderItemDto(
    string Title,
    int Quantity,
    Guid ProductId,
    decimal UnitPriceAmount);
```

---

```cs title="OrderSummaryDto.cs"
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Common;

public sealed record OrderSummaryDto(
    Guid OrderId,
    OrderStatus Status,
    decimal TotalAmount,
    string Currency,
    DateTime CreatedAt,
    int ItemsCount
);
```

---

##### Projections

```cs title="OrderProjections.cs"
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
            order.Items.FirstOrDefault()!.UnitPrice.Currency.Code,
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
                    item.UnitPrice.Sum * item.Quantity)).ToList(),
                order.TotalPrice.Sum,
                order.Items.First().UnitPrice.Currency.Code
            );
}
```

---

#### Queries

##### ActiveOrders

```cs title="ActiveOrderSpecification.cs"
using LegacyLego.Application.Orders.Common;
using LegacyLego.Application.Orders.Common.Projections;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public class ActiveOrderSpecification : Specification<Order, OrderId, OrderSummaryDto>
{
    public ActiveOrderSpecification(Guid clientId) : base(OrderProjections.Summary)
    {
        AddFilter(order => order.ClientId == clientId);
        AddFilter(order => order.Status == OrderStatus.PendingPayment);
    }
}
```

---

```cs title="GetActiveOrdersQuery.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Domain.Aggregates;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public sealed record GetActiveOrdersQuery(Guid UserId) : IQuery<IReadOnlyList<OrderSummaryDto>>;
```

---

```cs title="GetActiveOrdersQueryHandler.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Common;
using LegacyLego.Application.Orders.Queries.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public class GetActiveOrdersQueryHandler(IOrderRepository repository) : IQueryHandler<GetActiveOrdersQuery, IReadOnlyList<OrderSummaryDto>>
{
    public async Task<Result<IReadOnlyList<OrderSummaryDto>>> HandleAsync(GetActiveOrdersQuery query, CancellationToken ct)
    {
        var specification = new ActiveOrderSpecification(query.UserId);

        var result = await repository.GetOrders(specification, ct);

        return Result<IReadOnlyList<OrderSummaryDto>>.Success(result);
    }
}
```

---

##### OrderDetails

```cs title="ActiveOrderSpecification.cs"
using LegacyLego.Application.Orders.Common.Projections;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Queries.OrderDetails;

public class OrderDetailsSpecification : Specification<Order, OrderId, OrderDetailsDto>
{
    public OrderDetailsSpecification(Guid clientId, Guid orderId) : base(OrderProjections.Details)
    {
        AddFilter(order => order.ClientId == clientId);
        AddFilter(order => order.Id.Value == orderId);
    }
}
```

---

```cs title="GetOrderDetailsQuery.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.OrderDetails;


namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public sealed record GetOrderDetailsQuery(Guid UserId, Guid OrderId) : IQuery<OrderDetailsDto>;
```

---

```cs title="GetOrderDetailsQueryHandler.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.ActiveOrders;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Orders.Queries.OrderDetails;

public class GetOrderDetailsQueryHandler(IOrderRepository repository) : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    public async Task<Result<OrderDetailsDto>> HandleAsync(GetOrderDetailsQuery query, CancellationToken ct)
    {
        var specification = new OrderDetailsSpecification(query.UserId, query.OrderId);
        var order = await repository.GetOrder(specification, ct);

        if (order is null)
            return Result<OrderDetailsDto>.Failure(OrderErrors.GetNotFoundByOrderIdError(OrderId.From(query.OrderId)));

        return Result<OrderDetailsDto>.Success(order);
    }
}
```

---

```cs title="OrderDetailsDto.cs"
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Orders.Queries.OrderDetails;

public sealed record OrderDetailsDto(
    Guid OrderId,
    OrderStatus Status,
    DateTime CreatedAt,
    AddressDetailsDto DeliveryAddress,
    IReadOnlyList<OrderItemDetailsDto> Items,
    decimal TotalAmount,
    string Currency
);

public sealed record OrderItemDetailsDto(
    Guid ProductId,
    string Title,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);

public sealed record AddressDetailsDto(
    string Country,
    string City,
    string Street,
    string PostalCode
);
```

---

##### OrdersHistory

```cs title="GetOrdersHistoryQuery.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.OrdersHistory;


namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public sealed record GetOrdersHistoryQuery(Guid UserId, OrderHistoryRequest Filter) : IQuery<OrdersHistoryResponse>;
```

---

```cs title="GetOrdersHistoryQueryHandler.cs"
using LegacyLego.Application.Abstractions.Messaging.Query;
using LegacyLego.Application.Orders.Queries.OrdersHistory;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Orders.Queries.ActiveOrders;

public class GetOrdersHistoryQueryHandler(IOrderRepository repository) : IQueryHandler<GetOrdersHistoryQuery, OrdersHistoryResponse>
{
    public async Task<Result<OrdersHistoryResponse>> HandleAsync(GetOrdersHistoryQuery query, CancellationToken ct)
    {
        var specification = new OrderHistorySpecification(query.UserId,query.Filter);
        var orders = await repository.GetOrders(specification, ct);

        specification.DropPagination();
        var count = await repository.GetOrdersCount(specification, ct);

        var result = new OrdersHistoryResponse(orders,count);

        return Result<OrdersHistoryResponse>.Success(result);
    }
}
```

---

```cs title="OrderHistoryRequest.cs"
namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public record OrderHistoryRequest(
    int SkipRecords,
    int TakeRecords,
    decimal? MinPrice = null,
    string? SortBy = null,
    bool SortDescending = true);
```

---

```cs title="OrderHistorySpecification.cs"
using LegacyLego.Application.Orders.Common;
using LegacyLego.Application.Orders.Common.Projections;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using System.Linq.Expressions;

namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public class OrderHistorySpecification : Specification<Order, OrderId, OrderSummaryDto>
{
    public OrderHistorySpecification(Guid clientId, OrderHistoryRequest filter)
        : base(OrderProjections.Summary)
    {
        AddFilter(o => o.ClientId == clientId);

        var historyStatuses = new[] { OrderStatus.Paid, OrderStatus.Cancelled, OrderStatus.Refunded };
        AddFilter(o => historyStatuses.Contains(o.Status));

        if (filter.MinPrice.HasValue)
            AddFilter(o => o.TotalPrice.Sum >= filter.MinPrice.Value);

        ApplySorting(filter.SortBy, filter.SortDescending);

        SetSkipNum(filter.SkipRecords);
        SetLimitNum(filter.TakeRecords);
    }

    private void ApplySorting(string? sortBy, bool isDescending)
    {
        Expression<Func<Order, object>> expression = sortBy?.ToLower() switch
        {
            "price" => o => o.TotalPrice.Sum,
            "date" => o => o.CreationDateUtc,
            _ => o => o.CreationDateUtc 
        };

        if (isDescending) AddOrderByDescending(expression);
        else AddOrderBy(expression);
    }

    public void DropPagination()
    {
        DropLimit();
        DropSkip();
    }
}
```

---

```cs title="OrdersHistoryResponse.cs"
using LegacyLego.Application.Orders.Common;

namespace LegacyLego.Application.Orders.Queries.OrdersHistory;

public sealed record OrdersHistoryResponse(
    IReadOnlyList<OrderSummaryDto> Orders,
    int OrdersCount);
```

---

### Payments

#### Commands

##### PocessPaymentWebhook

```cs title="OrderPaymentSucceededDomainEventHandler.cs"
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging;
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Orders.Commands.Expire;
using LegacyLego.Application.Orders.Commands.Pay;
using LegacyLego.Domain.DomainEvents;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public class OrderPaymentSucceededDomainEventHandler(ICommandDispatcher dispatcher)
: IDomainEventHandler<OrderPaymentSucceeded>
{
    public async Task HandleAsync(OrderPaymentSucceeded notification, CancellationToken ct)
    {
        var command = new PayOrderCommand(notification.OrderId.Value);

        await dispatcher.DispatchAsync(command, ct);
    }
}
```

---

```cs title="ProcessPaymentDetails.cs"
using LegacyLego.Application.Orders.Commands.Expire;
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed record ProcessPaymentDetails
{
    public const string AlreadyProcessedWithTransactionIdCode = "OrderPayment.AlreadyProcessedWithTransactionId";
    public const string AlreadyProcessedCode = "OrderPayment.AlreadyProcessed";

    public const string SetSuccessedCode = "OrderPayment.SuccessfullySuccessed";
    public const string SetFailedCode = "OrderPayment.SuccessfullyFailed";
    public const string SetRefundedCode = "OrderPayment.SuccessfullyRefunded";

    public readonly string Code;
    public readonly string Message;
    public readonly Guid OrderId;
    public readonly string CurrentStatus;
    public readonly bool StateChanged;

    private ProcessPaymentDetails(string Code,
    Guid OrderId,
    string Message,
    string CurrentStatus,
    bool StateChanged)
    {
        this.Code = Code;
        this.OrderId = OrderId;
        this.Message = Message;
        this.CurrentStatus = CurrentStatus;
        this.StateChanged = StateChanged;
    }

    internal static ProcessPaymentDetails GetAlreadyProcessedWithTransactionIdDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: AlreadyProcessedWithTransactionIdCode,
            OrderId: orderId,
            Message: $"Payment with transactionId: {transactionId} is already processed",
            CurrentStatus: PaymentStatus.Succeeded.ToString(),
            false);
    }

    internal static ProcessPaymentDetails GetAlreadyProcessedDetails(string transactionId, PaymentStatus status, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: AlreadyProcessedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId: {transactionId} is already processed with {status.ToString()} state earlier",
            CurrentStatus: status.ToString(),
            false);
    }

    internal static ProcessPaymentDetails GetSetSuccessedDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: SetSuccessedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId:{transactionId} was set Successed",
            CurrentStatus: PaymentStatus.Succeeded.ToString(),
            true);
    }

    internal static ProcessPaymentDetails GetSetFailedDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: SetFailedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId:{transactionId} was set Failed",
            CurrentStatus: PaymentStatus.Failed.ToString(),
            true);
    }

    internal static ProcessPaymentDetails GetSetRefundedDetails(string transactionId, Guid orderId)
    {
        return new ProcessPaymentDetails(
            Code: SetRefundedCode,
            OrderId: orderId,
            Message: $"Payment with transactionId:{transactionId} was set Refunded",
            CurrentStatus: PaymentStatus.Refunded.ToString(),
            true);
    }
}
```

---

```cs title="ProcessPaymentErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public static class ProcessPaymentErrors
{
    public const string InvalidAmountCode = "Payment.InvalidAmount";
    public const string TotalPricesMismatchCode = "ProcessPayment.TotalPricesMismatch";
    public const string UnknownStatusCode = "ProcessPayment.UnknownStatus";
    public const string TransactionConflictCode = "ProcessPayment.TransactionConflict";

    public static Error GetInvalidAmountCodeError(decimal amount)
    {
        return new(
            Code: InvalidAmountCode,
            Message: $"Amount must be greater than zero, but it was {amount}");
    }

    public static Error GetTotalPricesMismatchError(decimal webhookAmount, decimal orderTotal)
    {
        return new(
            Code: TotalPricesMismatchCode,
            Message: $"Webhook amount:{webhookAmount} must be equivalent to order's total price: {orderTotal}");
    }

    public static Error GetUnknownStatusError(PaymentStatus unknownStatus)
    {
        return new(
            Code: UnknownStatusCode,
            Message: $"{unknownStatus} is unknown status");
    }

    public static Error GetTransactionConflictError(string TransactionId, string webhookTransactionId)
    {
        return new(
            Code: TransactionConflictCode,
            Message: $"For successed payment system get more then one different transactions by {TransactionId} and {webhookTransactionId} transactionId's");
    }

}
```

---

```cs title="ProcessPaymentWebhookCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Payments.Common;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed record ProcessPaymentWebhookCommand(PaymentWebhook Webhook) : ICommand<ProcessPaymentDetails>;
```

---

```cs title="ProcessPaymentWebhookCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Application.Payments.Services;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed class ProcessPaymentWebhookCommandHandler(
    PaymentLookup paymentLookup,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ProcessPaymentWebhookCommand, ProcessPaymentDetails>
{
    public async Task<Result<ProcessPaymentDetails>> HandleAsync(ProcessPaymentWebhookCommand command, CancellationToken ct)
    {
        var webhook = command.Webhook;

        var orderId = OrderId.From(webhook.OrderId);

        var order = await orderRepository.GetByIdAsync(orderId, ct);
        if (order is null)
            return Result<ProcessPaymentDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        var payment = await paymentLookup.GetOrCreateAsync(webhook.TransactionId, orderId, ct);

        var result = webhook.Status switch
        {
            PaymentStatus.Refunded => HandleRefunded(payment, webhook.TransactionId),

            PaymentStatus.Failed => HandleFailed(payment),

            PaymentStatus.Succeeded => HandleSucceeded(payment, webhook, order),

            _ => Result<ProcessPaymentDetails>.Failure(ProcessPaymentErrors.GetUnknownStatusError(webhook.Status))
        };

        await unitOfWork.SaveChangesAsync(ct);
        return result;
    }
    
    private static Result<ProcessPaymentDetails> HandleFailed(
        OrderPayment payment)
    {
        if (payment.Status is PaymentStatus.Failed)
        {
            return Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetAlreadyProcessedDetails(payment.TransactionId!, payment.Status, payment.OrderId.Value));
        }

        var paymentResult = payment.MarkAsFailed();
        if (paymentResult.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(paymentResult.Error);

        return Result<ProcessPaymentDetails>.Success(
            ProcessPaymentDetails.GetSetFailedDetails(payment.TransactionId!, payment.OrderId.Value));
    }

    private static Result<ProcessPaymentDetails> HandleRefunded(
        OrderPayment payment, string transactionId)
    {
        if (payment.Status is PaymentStatus.Refunded)
        {
            return Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetAlreadyProcessedDetails(payment.TransactionId!, payment.Status, payment.OrderId.Value));
        }

        var paymentResult = payment.MarkAsRefunded(transactionId);
        if (paymentResult.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(paymentResult.Error);

        return Result<ProcessPaymentDetails>.Success(
            ProcessPaymentDetails.GetSetRefundedDetails(payment.TransactionId!, payment.OrderId.Value));
    }

    private static Result<ProcessPaymentDetails> HandleSucceeded(
        OrderPayment payment, 
        PaymentWebhook webhook, 
        Order order)
    {
        if (payment.Status is PaymentStatus.Succeeded && payment.TransactionId != webhook.TransactionId)
        {
            return Result<ProcessPaymentDetails>.Failure(
                ProcessPaymentErrors.GetTransactionConflictError(payment.TransactionId!, webhook.TransactionId));
        }
        else if (payment.Status is PaymentStatus.Succeeded)
        {
            return Result<ProcessPaymentDetails>.Success(
                ProcessPaymentDetails.GetAlreadyProcessedDetails(payment.TransactionId!, payment.Status, payment.OrderId.Value));
        }

        var amountCheck = ValidateAmount(webhook.Currency, webhook.Amount, order);
        if (amountCheck.IsFailure)
        {
            var refundRequestResult = payment.MarkAsRefundRequested();
            if (refundRequestResult.IsFailure) return Result<ProcessPaymentDetails>.Failure(refundRequestResult.Error);

            return Result<ProcessPaymentDetails>.Failure(amountCheck.Error);
        }

        var paymentResult = payment.MarkAsSucceeded(webhook.TransactionId);
        if (paymentResult.IsFailure)
            return Result<ProcessPaymentDetails>.Failure(paymentResult.Error);

        return Result<ProcessPaymentDetails>.Success(ProcessPaymentDetails.GetSetSuccessedDetails(payment.TransactionId!, payment.OrderId.Value));
    }

    private static Result ValidateAmount(string code, decimal amount, Order order)
    {
        if (amount <= 0)
            return Result.Failure(ProcessPaymentErrors.GetInvalidAmountCodeError(amount));

        var currency = Currency.FromCode(code);

        if (currency.IsFailure)
            return currency;

        var webhookAmountPrice = Price.Create(amount, currency.Value);

        if (webhookAmountPrice.IsFailure)
            return webhookAmountPrice;

        if (order.TotalPrice != webhookAmountPrice.Value)
            return Result.Failure(ProcessPaymentErrors.GetTotalPricesMismatchError(amount, order.TotalPrice.Sum));

        return Result.Success();
    }
}
```

---

##### RefundRequested

```cs title="RefundRequestedOrderPaymentDomainEventHandler.cs"
using LegacyLego.Application.Abstractions.Messaging.Event.Domain;
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.DomainEvents;

namespace LegacyLego.Application.Orders.Commands.Create;

public class RefundRequestedOrderPaymentDomainEventHandler(IIntegrationEventPublisher eventPublisher)
: IDomainEventHandler<OrderPaymentRefundRequested>
{
    public async Task HandleAsync(OrderPaymentRefundRequested notification, CancellationToken ct)
    {
        var ivent = PaymentIntegrationEventMapper.Map(notification);
        await eventPublisher.PublishAsync(ivent, ct);
    }
}
```

---

##### StartPayment

```cs title="StartOrderPaymentCommand.cs"
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

namespace LegacyLego.Application.Payments.Commands.StartPayment;

public sealed record StartOrderPaymentCommand(Guid OrderId) : ICommand<StartOrderPaymentDetails>;
```

---

```cs title="StartOrderPaymentCommandHandler.cs"
using LegacyLego.Application.Abstractions.Data;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Abstractions.Messaging.Command;
using LegacyLego.Application.Exceptions;
using LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;
using LegacyLego.Application.Payments.Common;
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.Commands.StartPayment;

public sealed class StartOrderPaymentCommandHandler(
    IOrderRepository orderRepository,
    IPaymentRepository paymentRepository,
    IPaymentProvider paymentProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<StartOrderPaymentCommand, StartOrderPaymentDetails>
{
    private enum ConstraintCheckTimeline : byte
    {
        AfterConstraintCheck,
        BeforeConstraintCheck
    }

    public async Task<Result<StartOrderPaymentDetails>> HandleAsync(StartOrderPaymentCommand command, CancellationToken ct)
    {
        var orderId = OrderId.From(command.OrderId);

        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
            return Result<StartOrderPaymentDetails>.Failure(OrderErrors.GetNotFoundByOrderIdError(orderId));

        if (order.Status != OrderStatus.PendingPayment)
            return Result<StartOrderPaymentDetails>.Failure(StartOrderPaymentErrors.GetOrderIsNotInPendingPaymentError(command.OrderId, order.Status));

        if (await paymentRepository.ExistsSucceeded(orderId))
            return Result<StartOrderPaymentDetails>.Failure(StartOrderPaymentErrors.GetForOrderIsAlreadyExistsSuccessedPaymentError(command.OrderId));

        var existingBeforeCheckUniqConstraint = await paymentRepository.GetPendingByOrderIdAsync(orderId, ct);

        if (existingBeforeCheckUniqConstraint is not null)
        {
            return await EnsureSession(
                existingBeforeCheckUniqConstraint,
                order,
                paymentProvider,
                unitOfWork,
                ConstraintCheckTimeline.BeforeConstraintCheck,
                ct);
        }

        var paymentResult = OrderPayment.Create(orderId);
        if(paymentResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(paymentResult.Error);

        var payment = paymentResult.Value;

        paymentRepository.Add(payment);

        try
        {
            await unitOfWork.SaveChangesAsync(ct);
        }
        catch (UniqueConstraintViolation)
        {
            var existingAfterCheckUniqConstraint = await paymentRepository.GetPendingByOrderIdAsync(orderId, ct);

            if (existingAfterCheckUniqConstraint is null)
                return Result<StartOrderPaymentDetails>.Failure(StartOrderPaymentErrors.GetCanNotFindPendingPaymentAfterCheckConstraintError(command.OrderId));

            return await EnsureSession(
                existingAfterCheckUniqConstraint,
                order,
                paymentProvider,
                unitOfWork,
                ConstraintCheckTimeline.AfterConstraintCheck,
                ct);
        }

        var sessionResult = await paymentProvider.CreatePaymentSessionAsync(
                    payment.Id.Value,
                    order.TotalPrice.Sum,
                    order.TotalPrice.Currency.Code,
                    ct);

        if (sessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(sessionResult.Error);

        var session = sessionResult.Value;

        var extrernalSessionResult = ExternalSession.Create(
            session.ExternalSessionId,
            session.CheckoutUrl,
            session.ExpiresAtUtc);
        if (extrernalSessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(extrernalSessionResult.Error);

        payment.AttachSession(extrernalSessionResult.Value);

        await unitOfWork.SaveChangesAsync(ct);

        return Result<StartOrderPaymentDetails>.Success(
            StartOrderPaymentDetails.GetNewPaymentWithNewSessionDetails(session, orderId.Value));
    }

    private static async Task<Result<StartOrderPaymentDetails>> EnsureSession(
        OrderPayment payment,
        Order order,
        IPaymentProvider paymentProvider,
        IUnitOfWork unitOfWork,
        ConstraintCheckTimeline timeline,
        CancellationToken ct = default)
    {
        PaymentSession session;

        if (payment.HasSession && !payment.ExternalSession!.IsExpired(DateTime.UtcNow))
        {
            session = new PaymentSession(
                payment.Id.Value,
                payment.ExternalSession.ExternalId,
                payment.ExternalSession.CheckoutUrl,
                payment.ExternalSession.ExpiresAtUtc);


            return timeline switch
            {
                ConstraintCheckTimeline.BeforeConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                    StartOrderPaymentDetails.GetExistingPaymentWithExistingSessionBeforeCheckConstraintDetails(session, order.Id.Value)),

                ConstraintCheckTimeline.AfterConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                    StartOrderPaymentDetails.GetExistingPaymentWithExistingSessionAfterCheckConstraintDetails(session, order.Id.Value)),

                _ => throw new InvalidOperationException($"Unknown CheckConstraint timeline in StartOrderPaymentCommandHandler.EnsureSession. It was {timeline}")
            };
        }

        var newSessionResult = await paymentProvider.CreatePaymentSessionAsync(
                payment.Id.Value,
                order.TotalPrice.Sum,
                order.TotalPrice.Currency.Code,
                ct);

        if (newSessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(newSessionResult.Error);

        session = newSessionResult.Value;

        var extrernalSessionResult = ExternalSession.Create(
            session.ExternalSessionId,
            session.CheckoutUrl,
            session.ExpiresAtUtc);

        if (extrernalSessionResult.IsFailure)
            return Result<StartOrderPaymentDetails>.Failure(extrernalSessionResult.Error);


        payment.AttachSession(extrernalSessionResult.Value);

        await unitOfWork.SaveChangesAsync(ct);

        return timeline switch
        {
            ConstraintCheckTimeline.BeforeConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                StartOrderPaymentDetails.GetExistingPaymentWithNewSessionBeforeCheckConstraintDetails(session, order.Id.Value)),

            ConstraintCheckTimeline.AfterConstraintCheck => Result<StartOrderPaymentDetails>.Success(
                StartOrderPaymentDetails.GetExistingPaymentWithNewSessionAfterCheckConstraintDetails(session, order.Id.Value)),

            _ => throw new InvalidOperationException($"Unknown CheckConstraint timeline in StartOrderPaymentCommandHandler.EnsureSession. It was {timeline}")
        };
    }
}
```

---

```cs title="StartOrderPaymentDetails.cs"
using LegacyLego.Application.Payments.Common;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public sealed record StartOrderPaymentDetails
{
    public const string NewPaymentWithNewSessionCode = "StartOrderPayment.NewPaymentWithNewSession";

    public const string ExistingPaymentWithNewSessionBeforeCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithNewSessionBeforeCheckConstraint";
    public const string ExistingPaymentWithNewSessionAfterCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithNewSessionAfterCheckConstraint";

    public const string ExistingPaymentWithExistingSessionAfterCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithExistingSessionAfterCheckConstraint";
    public const string ExistingPaymentWithExistingSessionBeforeCheckConstraintCode = "StartOrderPayment.ExistingPaymentWithExistingSessionBeforeCheckConstraint";

    public readonly string Code;
    public readonly string Message;
    public readonly Guid OrderId;
    public readonly PaymentSession Session;

    private StartOrderPaymentDetails(string code,
    string message,
    PaymentSession session,
    Guid orderId)
    {
        Code = code;
        OrderId = orderId;
        Message = message;
        Session = session;
    }

    internal static StartOrderPaymentDetails GetNewPaymentWithNewSessionDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: NewPaymentWithNewSessionCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"created new Payment with PaymentId: {session.PaymentId} " +
            $"and session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithNewSessionBeforeCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithNewSessionBeforeCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and created new session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithNewSessionAfterCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithNewSessionAfterCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and created new session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithExistingSessionBeforeCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithExistingSessionBeforeCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and already exists session with ExternalSessionId: {session.ExternalSessionId}");
    }

    internal static StartOrderPaymentDetails GetExistingPaymentWithExistingSessionAfterCheckConstraintDetails(PaymentSession session, Guid orderId)
    {
        return new StartOrderPaymentDetails(
            code: ExistingPaymentWithExistingSessionAfterCheckConstraintCode,
            orderId: orderId,
            session: session,
            message: $"For Order with OrderId: {orderId} " +
            $"already exists Payment with PaymentId: {session.PaymentId} " +
            $"and already exists session with ExternalSessionId: {session.ExternalSessionId}");
    }
}
```

---

```cs title="StartOrderPaymentErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Application.Payments.Commands.PocessPaymentWebhook;

public static class StartOrderPaymentErrors
{
    public const string OrderIsNotInPendingPaymentCode = "StartOrderPatyment.OrderIsNotInPendingPayment";
    public const string ForOrderIsAlreadyExistsSuccessedPaymentCode = "StartOrderPatyment.ForOrderIsAlreadyExistsSuccessedPayment";
    public const string CanNotFindPendingPaymentAfterCheckConstraintCode = "StartOrderPatyment.CanNotFindPendingPaymentAfterCheckConstraint";

    public static Error GetOrderIsNotInPendingPaymentError(Guid orderId, OrderStatus status)
    {
        return new(
            Code: OrderIsNotInPendingPaymentCode,
            Message: $"The order being processed with OrderId: {orderId} is not waiting payment. Its in {status} status now");
    }

    public static Error GetForOrderIsAlreadyExistsSuccessedPaymentError(Guid orderId)
    {
        return new(
            Code: ForOrderIsAlreadyExistsSuccessedPaymentCode,
            Message: $"For order being processed with OrderId: {orderId} is already exists successed payment");
    }

    public static Error GetCanNotFindPendingPaymentAfterCheckConstraintError(Guid orderId)
    {
        return new(
            Code: CanNotFindPendingPaymentAfterCheckConstraintCode,
            Message: $"For order being processed with OrderId: {orderId} can not find existing pending payment after ConstraintCheck");
    }
}
```

---

#### Common

```cs title="PaymentIntegrationEventMapper.cs"
using LegacyLego.Application.Payments.IntegrationEvents;
using LegacyLego.Domain.DomainEvents;

namespace LegacyLego.Application.Payments.Common;

public static class PaymentIntegrationEventMapper
{
    public static RefundPaymentRequestedIntegrationEvent Map(OrderPaymentRefundRequested domainEvent)
    {
        return new RefundPaymentRequestedIntegrationEvent(
            domainEvent.TransactionId
        );
    }
}
```

---

```cs title="PaymentSession.cs"
using System;
using System.Collections.Generic;
using System.Text;

namespace LegacyLego.Application.Payments.Common;

public sealed record PaymentSession(
    Guid PaymentId,
    string ExternalSessionId,
    string CheckoutUrl,
    DateTime? ExpiresAtUtc = null);
```

---

```cs title="PaymentWebhook.cs"
using LegacyLego.Domain.Enums;

namespace LegacyLego.Application.Payments.Common;

public record PaymentWebhook(
    string TransactionId,
    Guid OrderId,
    decimal Amount,
    string Currency,
    PaymentStatus Status);
```

---

#### IntegrationEvents

```cs title="RefundPaymentRequestedIntegrationEvent.cs"
using LegacyLego.Application.Abstractions.Messaging.Event.Integration;

namespace LegacyLego.Application.Payments.IntegrationEvents;

public sealed record RefundPaymentRequestedIntegrationEvent(string TransactionId) : IIntegrationEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}
```

---

#### Services

```cs title="PaymentLookup.cs"
using LegacyLego.Domain.Abstractions;
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Application.Payments.Services;

public sealed class PaymentLookup
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentLookup(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<OrderPayment> GetOrCreateAsync(
        string transactionId,
        OrderId orderId,
        CancellationToken ct)
    {
        var payment = await _paymentRepository
            .GetByTransactionIdAsync(transactionId, ct);

        if (payment is not null)
            return payment;

        payment = await _paymentRepository
            .GetByOrderIdAsync(orderId, ct);

        if (payment is not null)
            return payment;

        var createResult = OrderPayment.Create(orderId);

        if (createResult.IsFailure)
            throw new InvalidOperationException(
                $"Failed to create OrderPayment: {createResult.Error}");

        payment = createResult.Value;

        _paymentRepository.Add(payment);

        return payment;
    }
}
```

---

## LegacyLego.Domain

```xml title="LegacyLego.Domain.csproj"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

---

### Abstractions

```cs title="IOrderRepository.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Abstractions;

public interface IOrderRepository
{
    public Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Order>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<TResult>> GetOrders<TResult>(Specification<Order,OrderId, TResult> specification, CancellationToken cancellationToken = default);

    public Task<TResult?> GetOrder<TResult>(Specification<Order, OrderId, TResult> specification, CancellationToken cancellationToken = default);

    public Task<int> GetOrdersCount(Specification<Order, OrderId> specification, CancellationToken cancellationToken = default);

    public void Add(Order order);
}
```

---

```cs title="IPaymentRepository.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Abstractions;

public interface IPaymentRepository
{
    public Task<OrderPayment?> GetByTransactionIdAsync(string id, CancellationToken cancellationToken = default);

    public Task<OrderPayment?> GetByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default);

    public Task<OrderPayment?> GetPendingByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default);

    public Task<bool> ExistsSucceeded(OrderId orderId);

    public Task<OrderPayment?> GetByIdAsync(OrderPaymentId orderId, CancellationToken cancellationToken = default);

    public void Add(OrderPayment payment);
}
```

---

### Aggregates

```cs title="Order.cs"
using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Domain.Exceptions;

namespace LegacyLego.Domain.Aggregates;

public class Order : AggregateRoot<OrderId>
{
    public Guid ClientId { get; }

    private Price? _frozenTotalPrice;

    public Price TotalPrice
    {
        get
        {
            return Status switch
            {
                OrderStatus.PendingPayment or OrderStatus.Expired
                    => CalculateTotalPrice(Items),
                OrderStatus.Paid or OrderStatus.Cancelled or OrderStatus.Refunded
                    => _frozenTotalPrice ?? throw new InvalidDomainStateException(
                        OrderExceptionalErrors.GetFrozenTotalPriceNotCalculatedError(Status)),
                _ => throw new InvalidDomainStateException(
                        OrderExceptionalErrors.GetWrongOrderStatusToGetTotalPriceError(Status)),
            };
        }
    }

    public OrderStatus Status { get; private set; }


    private readonly List<OrderItem> _items;

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public OrderAddress Address { get; }

    public DateTime CreationDateUtc { get; }

    private Order(
        OrderId id,
        Guid clientId,
        OrderStatus status,
        List<OrderItem> items,
        OrderAddress address,
        DateTime creationDateUtc) 
        : base(id)
    {
        ClientId = clientId;
        Status = status;
        _items = new List<OrderItem>(items);
        Address = address;
        CreationDateUtc = creationDateUtc;
    }

    public static Result<Order> Create(
        OrderAddress address,
        Guid clientId,
        List<OrderItem> items)
    {
        ArgumentNullException.ThrowIfNull(items,nameof(items));
        ArgumentNullException.ThrowIfNull(address, nameof(address));

        if (items.Any(x => x is null))
            throw new ArgumentException("Items collection contains null");

        if (clientId == Guid.Empty)
            return Result<Order>.Failure(OrderErrors.GetClientIdGuidInvalidError(clientId));

        var itemsCount = items.Count;
        // общее количество позиций не меньше одной
        if (itemsCount < 1)
            return Result<Order>.Failure(OrderErrors.GetItemsCountInvalidError(itemsCount));

        var firstCurrency = items.First().UnitPrice.Currency;

        if (items.Any(x => !x.UnitPrice.Currency.Equals(firstCurrency)))
            return Result<Order>.Failure(OrderErrors.GetItemsCurrenciesMismatchError());

        var total = CalculateTotalPrice(items);

        // общая стоимость всех позиций заказа больше нуля
        if (total.Sum <= 0)
            return Result<Order>.Failure(OrderErrors.GetItemsTotalBelowZeroError(total.Sum));

        var createdAt = DateTime.UtcNow;
        var orderId = OrderId.New();

        var order = new Order(
            orderId,
            clientId,
            OrderStatus.PendingPayment,
            items,
            address,
            createdAt);

        order.Raise(new OrderCreated(orderId, clientId, createdAt));

        return Result<Order>.Success(order);

    }

    public Result Pay()
    {
        var orderAction = OrderAction.Pay;
        var nextStatus = OrderStatus.Paid;

        if (Status is not (OrderStatus.PendingPayment or OrderStatus.Expired))
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;
        _frozenTotalPrice = CalculateTotalPrice(Items);

        base.Raise(new OrderPaid(Id,DateTime.UtcNow));

        return Result.Success();
    }

    public Result Cancel()
    {
        var orderAction = OrderAction.Cancel;
        var nextStatus = OrderStatus.Cancelled;

        if (Status is not (OrderStatus.PendingPayment or OrderStatus.Expired))
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;
        _frozenTotalPrice = CalculateTotalPrice(Items);

        base.Raise(new OrderCanceled(Id, DateTime.UtcNow));

        return Result.Success();
    }

    public Result Expire()
    {
        var orderAction = OrderAction.Expire;
        var nextStatus = OrderStatus.Expired;

        if (Status is not OrderStatus.PendingPayment)
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderExpired(Id, DateTime.UtcNow));

        return Result.Success();
    }

    public Result Refund()
    {
        var orderAction = OrderAction.Refund;
        var nextStatus = OrderStatus.Refunded;

        if (Status is not OrderStatus.Paid)
            return Result.Failure(OrderErrors.GetStatusTransitionFailureError(orderAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderRefunded(Id, DateTime.UtcNow));

        return Result.Success();
    }

    private static Price CalculateTotalPrice(IReadOnlyList<OrderItem> items)
    {
        // при текущих инвариантах это невозможно, но станет актуально в случае, если добавятся функции добавления/удаления позиции товара
        if (items.Count == 0)
            throw new InvariantViolationException(
                OrderExceptionalErrors.GetOrderContainsNoItemsError());

        var currency = items.First().UnitPrice.Currency;

        var total = Price.Zero(currency);

        foreach (var item in items)
            total = total.Plus(item.GetTotalPrice());

        return total;
    }
}
```

---

```cs title="OrderPayment.cs"
using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Aggregates;

public class OrderPayment : AggregateRoot<OrderPaymentId>
{
    public string? TransactionId { get; private set; }

    public OrderId OrderId { get; }

    public PaymentStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public ExternalSession? ExternalSession { get; private set; }

    public bool HasSession => ExternalSession is not null;

    public bool IsRefundRequested => Status is PaymentStatus.RefundRequested;

    private OrderPayment(
        OrderPaymentId id,
        OrderId orderId,
        DateTime createdAtUtc,
        PaymentStatus status) : base(id)
    {
        OrderId = orderId;
        Status = status;
        CreatedAtUtc = createdAtUtc;
    }

    public static Result<OrderPayment> Create(OrderId orderId)
    {
        var createdAt = DateTime.UtcNow;
        var status = PaymentStatus.Pending;
        var id = OrderPaymentId.New();

        var payment = new OrderPayment(id, orderId, createdAt, status);

        payment.Raise(new OrderPaymentCreated(id, orderId, createdAt));

        return Result<OrderPayment>.Success(payment);
    }

    public Result AttachSession(ExternalSession externalSession)
    {
        ExternalSession = externalSession;

        return Result.Success();
    }

    public Result MarkAsSucceeded(string transactionId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(transactionId);

        if (Status == PaymentStatus.Succeeded && TransactionId == transactionId)
            return Result.Success();

        if (TransactionId != null && TransactionId != transactionId)
            return Result.Failure(OrderPaymentErrors.GetWrongTransactionIdExchangeError(TransactionId, transactionId));

        var paymentAction = PaymentAction.Success;
        var nextStatus = PaymentStatus.Succeeded;

        if (Status is not PaymentStatus.Pending)
            return Result.Failure(OrderPaymentErrors.GetStatusTransitionFailureError(paymentAction, Status, nextStatus));

        Status = nextStatus;
        TransactionId = transactionId;

        base.Raise(new OrderPaymentSucceeded(Id, OrderId, TransactionId!));

        return Result.Success();
    }

    public Result MarkAsFailed()
    {
        var paymentAction = PaymentAction.Fail;
        var nextStatus = PaymentStatus.Failed;

        if (Status is not PaymentStatus.Pending)
            return Result.Failure(OrderPaymentErrors.GetStatusTransitionFailureError(paymentAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderPaymentFailed(Id));

        return Result.Success();
    }

    public Result MarkAsRefundRequested()
    {
        var paymentAction = PaymentAction.RefundRequest;
        var nextStatus = PaymentStatus.RefundRequested;

        if (Status is not PaymentStatus.Succeeded)
            return Result.Failure(OrderPaymentErrors.GetStatusTransitionFailureError(paymentAction, Status, nextStatus));

        Status = nextStatus;

        base.Raise(new OrderPaymentRefundRequested(Id, TransactionId!));

        return Result.Success();
    }

    public Result MarkAsRefunded(string transactionId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(transactionId);

        if (Status == PaymentStatus.Refunded && TransactionId == transactionId)
            return Result.Success();

        if (TransactionId != null && TransactionId != transactionId)
            return Result.Failure(OrderPaymentErrors.GetWrongTransactionIdExchangeError(TransactionId, transactionId));

        var paymentAction = PaymentAction.Refund;
        var nextStatus = PaymentStatus.Refunded;

        if (Status is not PaymentStatus.RefundRequested
            && Status is not PaymentStatus.Succeeded
            && Status is not PaymentStatus.Pending)
        {
            return Result.Failure(
                OrderPaymentErrors.GetStatusTransitionFailureError(
                    paymentAction, Status, nextStatus));
        }

        TransactionId ??= transactionId;
        Status = nextStatus;

        base.Raise(new OrderPaymentRefunded(Id, TransactionId!));

        return Result.Success();
    }
}
```

---

### DomainEvents

```cs title="OrderCanceled.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderCanceled(
    OrderId OrderId,
    DateTime CanceledAt) : IDomainEvent;
```

---

```cs title="OrderCreated.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderCreated(
    OrderId OrderId,
    Guid ClientId,
    DateTime CreatedAt) : IDomainEvent;
```

---

```cs title="OrderExpired.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderExpired(
    OrderId OrderId,
    DateTime ExpiredAt) : IDomainEvent;
```

---

```cs title="OrderPaid.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaid(
    OrderId OrderId,
    DateTime PaidAt) : IDomainEvent;
```

---

```cs title="OrderPaymentCreated.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentCreated(
    OrderPaymentId Paymentid,
    OrderId OrderId,
    DateTime CreatedAt) : IDomainEvent;
```

---

```cs title="OrderPaymentFailed.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentFailed(
    OrderPaymentId Paymentid) : IDomainEvent;
```

---

```cs title="OrderPaymentRefunded.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentRefunded(
    OrderPaymentId Paymentid,
    string TransactionId) : IDomainEvent;
```

---

```cs title="OrderPaymentRefundedWithoutSuccess.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentRefundedWithoutSuccess(
    OrderPaymentId Paymentid,
    string TransactionId) : IDomainEvent;
```

---

```cs title="OrderPaymentRefundRequested.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentRefundRequested(
    OrderPaymentId Paymentid,
    string TransactionId) : IDomainEvent;
```

---

```cs title="OrderPaymentSucceeded.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderPaymentSucceeded(
    OrderPaymentId Paymentid,
    OrderId OrderId,
    string TransactionId) : IDomainEvent;
```

---

```cs title="OrderRefunded.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.DomainEvents;

public sealed record OrderRefunded(
    OrderId OrderId,
    DateTime RefundedAt) : IDomainEvent;
```

---

### Enums

```cs title="OrderAction.cs"
namespace LegacyLego.Domain.Enums;

public enum OrderAction : byte
{
    Create,
    Pay,
    Expire,
    Cancel,
    Refund
}
```

---

```cs title="OrderStatus.cs"
namespace LegacyLego.Domain.Enums;

public enum OrderStatus : byte
{
    PendingPayment,
    Paid,
    Cancelled,
    Expired,
    Refunded
}
```

---

```cs title="PaymentAction.cs"
namespace LegacyLego.Domain.Enums;

public enum PaymentAction : byte
{
    Success,
    Fail,
    Refund,
    RefundRequest
}
```

---

```cs title="PaymentStatus.cs"
namespace LegacyLego.Domain.Enums;

public enum PaymentStatus : byte
{
    Pending,
    Succeeded,
    Failed,
    Refunded,
    RefundRequested
}
```

---

### Errors

```cs title="CurrencyErrors.cs"
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Errors;

public static class CurrencyErrors
{
    public const string NotSupportedCode = "Currency.NotSupportedCode";
    public const string WrongCodeLengthCode = "Currency.WrongCodeLength";

    public static Error GetWrongCodeError(
        int actualCodeLength,
        string codeString)
    {
        return new(
            Code: WrongCodeLengthCode,
            Message: $"Код валюты должен состоять ровно из 3 символов. Код {codeString} содержит {actualCodeLength}");
    }

    public static Error GetNotSupportedError(string codeString)
    {
        return new(
            Code: NotSupportedCode,
            Message: $"Выбранная вами валюта {codeString} не поддерживается системой");
    }
}
```

---

```cs title="OrderErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Errors;

public static class OrderErrors
{
    public const string StatusTransitionFailureCode = "Order.StatusTransitionFailure";
    public const string ItemsCountInvalidCode = "Order.ItemsCountInvalid";
    public const string ItemsCurrenciesMismatchCode = "Order.ItemsCurrenciesMismatch";
    public const string ItemsTotalBelowZeroCode = "Order.ItemsTotalBelowZero";
    public const string ClientIdGuidInvalidCode = "Order.ClientIdGuidInvalid";
    public const string NotFoundByOrderId = "Order.NotFoundByOrderId";

    public static Error GetStatusTransitionFailureError(
        OrderAction action,
        OrderStatus actualStatus,
        OrderStatus nextStatus)
    {
        return new(
            Code: StatusTransitionFailureCode,
            Message: $"Action {action.ToString()} не позволяет перевести Order из статуса {actualStatus.ToString()} в {nextStatus.ToString()}");
    }

    public static Error GetItemsCountInvalidError(int itemsCount)
    {
        return new(
            Code: ItemsCountInvalidCode,
            Message: $"Невозможно создать заказ с общим количеством позиций {itemsCount}, должна быть хотя бы 1 позиция");
    }

    public static Error GetItemsCurrenciesMismatchError()
    {
        return new(
            Code: ItemsCurrenciesMismatchCode,
            Message: "Стоимости всех позиций заказа не должны быть представлены разными валютами");
    }

    public static Error GetItemsTotalBelowZeroError(decimal total)
    {
        return new(
            Code: ItemsTotalBelowZeroCode,
            Message: $"Общая стоимость всех позиций заказа не должна быть меньше 0, {total} не подходит");
    }

    public static Error GetClientIdGuidInvalidError(Guid guid)
    {
        return new(
            Code: ClientIdGuidInvalidCode,
            Message: $"Полученный ProductId GUID: {guid} Не прошел валидацию"
        );
    }

    public static Error GetNotFoundByOrderIdError(OrderId orderId)
    {
        return new(
            Code: NotFoundByOrderId,
            Message: $"Не удалось найти заказ по заданному первичному ключу: {orderId.Value}"
        );
    }
}
```

---

```cs title="OrderItemErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class OrderItemErrors
{
    public const string TitleInvalidCode = $"OrderItem.TitleInvalid";
    public const string QuantityBelowOneCode = $"OrderItem.QuantityBelowOne";
    public const string ProductIDGuidInvalidCode = $"OrderItem.ProductIDGuidInvalid";

    public static Error GetTitleInvalidError()
    {
        return new(
            Code: TitleInvalidCode,
            Message: "В названии товара не должно быть пустой строки"
        );
    }

    public static Error GetProductIDGuidInvalidError(Guid invalidGuid)
    {
        return new(
            Code: ProductIDGuidInvalidCode,
            Message: $"Полученный ProductId GUID: {invalidGuid} Не прошел валидацию"
        );
    }

    public static Error GetQuantityBelowOneError(int quantity)
    {
        return new(
            Code: QuantityBelowOneCode,
            Message: "Позиция заказа не может быть создана в количестве меньшем единице. " +
                     $"Значение {quantity} не соответствует правилам валидации"
        );
    }
}
```

---

```cs title="OrderPaymentErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class OrderPaymentErrors
{
    public const string StatusTransitionFailureCode = "OrderPayment.StatusTransitionFailure";
    public const string WrongTransactionIdExchangeCode = "OrderPayment.WrongTransactionIdExchange";

    public static Error GetStatusTransitionFailureError(
        PaymentAction action,
        PaymentStatus actualStatus,
        PaymentStatus nextStatus)
    {
        return new(
            Code: StatusTransitionFailureCode,
            Message: $"Action {action.ToString()} не позволяет перевести OrderPayment из статуса {actualStatus.ToString()} в {nextStatus.ToString()}");
    }

    public static Error GetWrongTransactionIdExchangeError(string currentId,string nextId)
    {
        return new(
            Code: WrongTransactionIdExchangeCode,
            Message: $"Недопустимая замена текущего TransactionId:{currentId} на {nextId} в MarkAsSucceeded операции");
    }

}
```

---

```cs title="PriceErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Errors;

public static class PriceErrors
{
    public const string SumBelowZeroCode = $"Price.SumBelowZero";
    public static Error GetSumBelowZeroError(
        decimal sum)
    {
        return new(
            Code: SumBelowZeroCode,
            Message: $"значение цены не должно равняться нулю или опускаться ниже, текущее значение {sum} некорректно"
        );
    }
}
```

---

### ExceptionalErrors

```cs title="CurrencyExceptionalErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class CurrencyExceptionalErrors
{
    public const string InvalidScaleValue = "Currency.InvalidScaleValue";

    public static ExceptionalError GetInvalidScaleValueError(int actualScale)
    {
        return new(
            Code: InvalidScaleValue,
            Message: $"Scale валюты не должен опускаться ниже нуля, значение: {actualScale} нарушает целостность системы");
    }
}
```

---

```cs title="OrderExceptionalErrors.cs"
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class OrderExceptionalErrors
{
    public const string ContainsNoItemsErrorCode = "OrderContainsNoItems";
    public const string FrozenTotalPriceNotCalculatedErrorCode = "Order.FrozenTotalPriceNotCalculated";
    public const string WrongOrderStatusToGetTotalPriceErrorCode = "Order.WrongOrderStatusToGetTotalPrice";
    public static ExceptionalError GetOrderContainsNoItemsError()
    {
        return new(
            Code: ContainsNoItemsErrorCode,
            Message: $"Order не может не содержать ни 1 позиции при расчёте TotalPrice"
        );
    }

    public static ExceptionalError GetFrozenTotalPriceNotCalculatedError(OrderStatus status)
    {
        return new(
            Code: FrozenTotalPriceNotCalculatedErrorCode,
            Message: $"При обращении к полю _frozenTotalPrice произошла ошибка: значение свойства не расчитано." +
            $"в том случае, если текущий Order Status имеет значение {status.ToString()}, занчение _frozenTotalPrice уже должно быть рассчитано"
        );
    }

    public static ExceptionalError GetWrongOrderStatusToGetTotalPriceError(OrderStatus status)
    {
        return new(
            Code: WrongOrderStatusToGetTotalPriceErrorCode,
            Message: $"Для статуса {status.ToString()} рассчитать значение TotalPrice невозможно"
        );
    }
}
```

---

```cs title="PriceExceptionalErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class PriceExceptionalErrors
{

    public const string MultiplyBelowZeroErrorCode = "Price.SumBelowZero";
    public const string CurrencyMismatchErrorCode = "Price.CurrencyMismatch";
    public const string DecimalSumOverflowErrorCode = "Price.DecimalSumOverflow";

    public static ExceptionalError GetMultiplyBelowZeroError(
        int factor)
    {
        return new(
            Code: MultiplyBelowZeroErrorCode,
            Message: $"Множитель стоимости не должен опускаться ниже нуля, текущее значение {factor} нарушает доменную логику"
        );
    }

    public static ExceptionalError GetCurrencyMismatchError(
        string currencyCode,
        string otherCurrencyCode)
    {
        return new(
            Code: CurrencyMismatchErrorCode,
            Message: $"Складывать значения различных валют недопустимо: " +
            $"({currencyCode} + {otherCurrencyCode}) считается недопустимой операцией, нарушающей доменную логику"
        );
    }

    public static ExceptionalError GetAdditionSumOverflowError(
        decimal firstValue,
        decimal otherValue
        )
    {
        return new(
            Code: DecimalSumOverflowErrorCode,
            Message: $"В результате выполнения математической операции сложения" +
            $"со значениями цены: {firstValue} и {otherValue} произошел decimal Overflow");
    }

    public static ExceptionalError GetMultiplySumOverflowError(
        decimal firstValue,
        int factor
        )
    {
        return new(
            Code: DecimalSumOverflowErrorCode,
            Message: $"В результате выполнения математической операции умножения " +
            $"цены: {firstValue} на множитель {factor} произошел decimal Overflow");
    }
}
```

---

```cs title="ResultExceptionalErrors.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ExceptionalErrors;

public static class ResultExceptionalErrors
{
    public const string UnexpectedValueAccessErrorCode = "Result.UnexpectedValueAccess";

    public const string InvalidInitializationErrorCode = "Result.InvalidInitialization";

    public static ExceptionalError GetUnexpectedValueAccessError()
    {
        return new(
            Code: UnexpectedValueAccessErrorCode,
            Message: $"Некорректное обращение к Result.Value в случае, когда IsSuccess = false"
        );
    }

    public static ExceptionalError GetInvalidResultInitializationError(
        bool isSuccess,
        bool isErrorContains)
    {
        var message = InvalidInitializationErrorCode;

        switch (isSuccess)
        {
            case true when isErrorContains:
                message = "Result не может быть успешным (IsSuccess) и одновременно содержать ошибку (Error), это нарушение состояния"; break;

            case false when !isErrorContains:
                message = "Result не может быть инициализирован как Failure и одновременно с тем не содержать ошибки (Error.None)"; break;
        }

        return new(
            Code: InvalidInitializationErrorCode,
            Message: message!
        );
    }
}
```

---

### Exceptions

```cs title="InvalidDomainStateException.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Exceptions;

public class InvalidDomainStateException : DomainException
{
    public InvalidDomainStateException(ExceptionalError error) : base(error) { }
}
```

---

```cs title="InvariantViolationException.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.Exceptions;

public class InvariantViolationException : DomainException
{
    public InvariantViolationException(ExceptionalError error) : base(error) { }
}
```

---

### Shared

```cs title="AggregateRoot.cs"
namespace LegacyLego.Domain.Shared;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : ValueObject
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot(TId id) : base(id)
    {

    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
```

---

```cs title="DomainException.cs"
namespace LegacyLego.Domain.Shared;

public abstract class DomainException : Exception
{
    public ExceptionalError Error { get; }

    protected DomainException(ExceptionalError error)
        : base(error.Code + ": " + error.Message) 
    {
        Error = error;
    }
}
```

---

```cs title="Entity.cs"
using LegacyLego.Domain.Aggregates;

namespace LegacyLego.Domain.Shared;

public abstract class Entity<TId> : IEquatable<Entity<TId>> 
    where TId : ValueObject
{

    public TId Id { get; init; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Equals(entity);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null || other.GetType() != GetType())
            return false;

        return Id.Equals(other.Id);

    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        if (left is null ^ right is null) return false;
        return left is null || left.Equals(right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```

---

```cs title="Error.cs"
namespace LegacyLego.Domain.Shared;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new("", "");
}
```

---

```cs title="ExceptionalError.cs"
namespace LegacyLego.Domain.Shared;

public sealed record ExceptionalError(string Code, string Message);
```

---

```cs title="IDomainEvent.cs"
namespace LegacyLego.Domain.Shared;

public interface IDomainEvent
{

}
```

---

```cs title="Result.cs"
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Exceptions;

namespace LegacyLego.Domain.Shared;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
            case false when error == Error.None:
                throw new InvalidDomainStateException(
                    ResultExceptionalErrors.GetInvalidResultInitializationError(isSuccess, error != Error.None));
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public static Result Success() =>
        new(true, Error.None);

    public static Result Failure(Error error) =>
        new(false, error);
}
```

---

```cs title="ResultT.cs"
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.ExceptionalErrors;

namespace LegacyLego.Domain.Shared;

public class Result<T> : Result
{
    private readonly T _value;

    public T Value => IsSuccess
        ? _value
        : throw new InvalidDomainStateException(
            ResultExceptionalErrors.GetUnexpectedValueAccessError());

    private Result(T value)
        : base(true, Error.None)
    {
        _value = value;
    }

    private Result(Error error)
        : base(false, error)
    {
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    public new static Result<T> Failure(Error error)
    {
        return new Result<T>(error);
    }
}
```

---

```cs title="Specification.cs"
using System.Linq.Expressions;

namespace LegacyLego.Domain.Shared;

public abstract class Specification<TEntity, TId, TResult> : Specification<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : ValueObject
{
    public Expression<Func<TEntity, TResult>> Selector { get; }

    protected Specification(
        Expression<Func<TEntity, TResult>> selector)
    {
        Selector = selector;
    }
}

public abstract class Specification<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : ValueObject
{
    public int? SkipNum { get; private set; }

    public int? LimitNum { get; private set; }

    public List<Expression<Func<TEntity, bool>>> FilterExpressions { get; } = new();

    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

    public List<Expression<Func<TEntity, object>>> OrderByExpressions { get; } = new();

    public List<Expression<Func<TEntity, object>>> OrderByDescendingExpressions { get; } = new();

    protected Specification() { }

    protected void AddFilter(Expression<Func<TEntity, bool>> filterExpression) =>
        FilterExpressions.Add(filterExpression);

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
        IncludeExpressions.Add(includeExpression);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
        OrderByExpressions.Add(orderByExpression);

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) =>
        OrderByDescendingExpressions.Add(orderByDescendingExpression);

    protected void SetSkipNum(int skipNum) =>
        SkipNum = skipNum;

    protected void SetLimitNum(int limitNum) =>
        LimitNum = limitNum;

    protected void DropSkip() =>
        SkipNum = null;

    protected void DropLimit() =>
        LimitNum = null;
}
```

---

```cs title="ValueObject.cs"
using System;
using System.Collections.Generic;
using System.Text;

namespace LegacyLego.Domain.Shared;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null ^ right is null) return false;
        return left is null || left.Equals(right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject other)
            return false;

        return Equals(other);
    }

    public bool Equals(ValueObject? other)
    {
        if (other is null || other.GetType() != GetType())
            return false;

        return ValuesAreEqual(other);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(new HashCode(),
                (hash, value) =>
                {
                    hash.Add(value);
                    return hash;
                }).ToHashCode();
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

}
```

---

### ValueObjects

```cs title="Currency.cs"
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Shared;
using System.IO.IsolatedStorage;

namespace LegacyLego.Domain.ValueObjects;

public class Currency : ValueObject
{
    private static readonly Dictionary<string, Currency> Codes;

    public static readonly Currency Usd = new("USD", "$", 2);

    public static readonly Currency Eur = new("EUR", "€", 2);

    public static readonly Currency Rub = new("RUB", "₽", 2);

    public string Code { get; }

    public string Symbol { get; }

    public int Scale { get; }

    static Currency()
    {
        Codes = new Dictionary<string, Currency>()
        {
            { Usd.Code, Usd},
            { Eur.Code, Eur},
            { Rub.Code, Rub}
        };
    }

    private Currency(string code, string symbol, int scale = 2)
    {
        if (scale < 0)
           throw new InvariantViolationException(
                CurrencyExceptionalErrors.GetInvalidScaleValueError(scale));

        Code = code.ToUpperInvariant();
        Symbol = symbol;
        Scale = scale;

    }

    public static Result<Currency> FromCode(string code)
    {
        if(code is null) 
            throw new ArgumentNullException(nameof(code));

        var codeString = code.Trim().ToUpperInvariant();

        if (codeString.Length != 3)
            return Result<Currency>.Failure(
                CurrencyErrors.GetWrongCodeError(codeString.Length, codeString));

        if (!Codes.TryGetValue(codeString, out var currency))
            return Result<Currency>.Failure(
                CurrencyErrors.GetNotSupportedError(codeString));

        var scale = currency.Scale;


        return Result<Currency>.Success(currency);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
    }
}
```

---

```cs title="ExternalSession.cs"
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using static System.Net.WebRequestMethods;

namespace LegacyLego.Domain.ValueObjects;

public sealed class ExternalSession : ValueObject
{
    public string ExternalId { get; }
    public string CheckoutUrl { get; }
    public DateTime? ExpiresAtUtc { get; }

    private ExternalSession(string externalId, string checkoutUrl, DateTime? expiresAtUtc)
    {
        ExternalId = externalId;
        CheckoutUrl = checkoutUrl;
        ExpiresAtUtc = expiresAtUtc;
    }

    public static Result<ExternalSession> Create(string externalId, string checkoutUrl, DateTime? expiresAtUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(externalId, nameof(externalId));
        ArgumentException.ThrowIfNullOrWhiteSpace(checkoutUrl, nameof(checkoutUrl));

        return Result<ExternalSession>.Success(new ExternalSession(externalId, checkoutUrl, expiresAtUtc));
    }

    public bool IsExpired(DateTime nowUtc)
    {
        if (ExpiresAtUtc is null) return false;

        return ExpiresAtUtc.Value <= nowUtc;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return ExternalId;
        yield return CheckoutUrl;
        if (ExpiresAtUtc.HasValue) yield return ExpiresAtUtc.Value;
    }
}
```

---

```cs title="OrderAddress.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class OrderAddress : ValueObject
{
    public string Country { get; }

    public string City { get; }

    public string Street { get; }

    public string PostalCode { get; }

    private OrderAddress(
        string country,
        string city,
        string street,
        string postalCode)
    {
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    public static Result<OrderAddress> Create(
        string country,
        string city,
        string street,
        string postalCode)
    {
        return Result<OrderAddress>.Success(new OrderAddress(
            country,
            city,
            street,
            postalCode
        ));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Country;
        yield return City;
        yield return Street;
        yield return PostalCode;
    }
}
```

---

```cs title="OrderId.cs"
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public sealed class OrderId : ValueObject
{
    public Guid Value { get; }

    public OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId New() => new(Guid.NewGuid());

    public static OrderId From(Guid value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
```

---

```cs title="OrderItem.cs"
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class OrderItem : ValueObject
{
    public string Title { get; }

    public int Quantity { get; }

    public Guid ProductId { get; }

    public Price UnitPrice { get; }

    private OrderItem(
        string title,
        int quantity,
        Guid productId,
        Price unitPrice)
    {
        Title = title;
        Quantity = quantity;
        ProductId = productId;
        UnitPrice = unitPrice;
    }

    public static Result<OrderItem> Create(
        string title,
        int quantity,
        Guid productId,
        Price unitPrice)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
        ArgumentNullException.ThrowIfNull(unitPrice, nameof(unitPrice));

        title = title.Trim();

        if (productId == Guid.Empty)
            return Result<OrderItem>.Failure(OrderItemErrors.GetProductIDGuidInvalidError(productId));

        if (quantity < 1)
            return Result<OrderItem>.Failure(OrderItemErrors.GetQuantityBelowOneError(quantity));

        return Result<OrderItem>.Success(new OrderItem(title, quantity, productId, unitPrice));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return ProductId;
        yield return Title;
        yield return Quantity;
        yield return UnitPrice;
    }

    public Price GetTotalPrice()
    {
        return UnitPrice.MultiplyByQuantity(Quantity);
    }
}
```

---

```cs title="OrderPaymentId.cs"
using LegacyLego.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegacyLego.Domain.ValueObjects;

public sealed class OrderPaymentId : ValueObject
{
    public Guid Value { get; }

    public OrderPaymentId(Guid value)
    {
        Value = value;
    }

    public static OrderPaymentId New() => new(Guid.NewGuid());

    public static OrderPaymentId From(Guid value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
```

---

```cs title="Price.cs"
using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.ExceptionalErrors;
using LegacyLego.Domain.Exceptions;
using LegacyLego.Domain.Shared;

namespace LegacyLego.Domain.ValueObjects;

public class Price : ValueObject
{
    public decimal Sum { get; }

    public Currency Currency { get; }

    public bool IsPositive => Sum > 0;

    public bool IsZero => Sum == 0m;

    private Price(decimal sum,Currency currency)
    {
        Sum = Normalize(sum, currency.Scale);
        Currency = currency;   
    }

    private static decimal Normalize(decimal value, int scale)
    {
        return Math.Round(value, scale, MidpointRounding.ToEven);
    }

    internal static Price Zero(Currency currency)
    {
        return new Price(0m, currency);
    }

    public static Result<Price> Create(decimal sum, Currency currency)
    {
        ArgumentNullException.ThrowIfNull(currency);

        var normalized = Normalize(sum, currency.Scale);

        if (normalized <= 0)
        {
            return Result<Price>.Failure(PriceErrors.GetSumBelowZeroError(sum));
        }

        var price = new Price(normalized, currency);

        return Result<Price>.Success(price);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Currency;
        yield return Sum;
    }

    public Price Plus(Price other)
    {
        if (!this.Currency.Equals(other.Currency))
        {
            throw new InvariantViolationException(
                PriceExceptionalErrors.GetCurrencyMismatchError(
                    this.Currency.Code,other.Currency.Code));
        }

        decimal sum;

        try
        {
            sum = checked(this.Sum + other.Sum);
        }
        catch (OverflowException)
        {
            throw new InvariantViolationException(
                PriceExceptionalErrors.GetAdditionSumOverflowError(this.Sum,other.Sum));
        }

        var sumPrice = new Price(sum, this.Currency);

        return sumPrice;
    }

    public Price MultiplyByQuantity(int factor)
    {
        if (factor < 0)
        {
            throw new InvariantViolationException(
                PriceExceptionalErrors.GetMultiplyBelowZeroError(factor));
        }

        decimal sum;

        try
        {
            sum = checked(this.Sum * factor);
        }
        catch (OverflowException)
        {
            throw new InvariantViolationException(PriceExceptionalErrors
                .GetMultiplySumOverflowError(this.Sum, factor));
        }

        var sumPrice = new Price(sum, this.Currency);

        return sumPrice;
    }
}
```

---

## LegacyLego.Domain.Tests

```xml title="LegacyLego.Domain.Tests.csproj"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <OutputType>Exe</OutputType>
    <TUnit_Parallel>true</TUnit_Parallel>
    <TUnit_DefaultTimeout>00:05:00</TUnit_DefaultTimeout>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="TUnit" Version="1.22.3" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\LegacyLego.Domain\LegacyLego.Domain.csproj" />
  </ItemGroup>

</Project>
```

---

```cs title="GlobalUsings.cs"
global using TUnit.Core;
global using TUnit.Assertions;
global using TUnit.Assertions.Extensions;

global using LegacyLego.Domain.Aggregates;
global using LegacyLego.Domain.DomainEvents;
global using LegacyLego.Domain.Enums;
global using LegacyLego.Domain.Errors;
global using LegacyLego.Domain.ExceptionalErrors;
global using LegacyLego.Domain.Exceptions;
global using LegacyLego.Domain.Shared;
global using LegacyLego.Domain.ValueObjects;
global using LegacyLego.Domain.Tests.Common.Builders;

global using static LegacyLego.Domain.Tests.Common.Factories.OrderDataFactory;
```

---

### Common

#### Builders

```cs title="OrderBuilder.cs"
namespace LegacyLego.Domain.Tests.Common.Builders;

public class OrderBuilder
{
    private OrderAddress? _address = null!;
    private Guid? _clientId = null!;
    private List<OrderItem>? _items = null!;

    public OrderBuilder WithAddress(OrderAddress address)
    {
        _address = address;
        return this;
    }

    public OrderBuilder WithNullAddress()
    {
        _address = null!;
        return this;
    }

    public OrderBuilder WithClientId(Guid clientId)
    {
        _clientId = clientId;
        return this;
    }

    public OrderBuilder WithEmptyClientId()
    {
        _clientId = Guid.Empty;
        return this;
    }

    public OrderBuilder WithItems(List<OrderItem> items)
    {
        _items = items;
        return this;
    }

    public OrderBuilder WithNullItems()
    {
        _items = null!;
        return this;
    }

    public OrderBuilder WithNoItems()
    {
        _items = new List<OrderItem>();
        return this;
    }

    public OrderBuilder AddItem(OrderItem item)
    {
        _items?.Add(item);
        return this;
    }

    public OrderBuilder AddNullItem()
    {
        _items?.Add(null!);
        return this;
    }

    public Result<Order> BuildResult()
    {
        return Order.Create(_address!, _clientId ?? Guid.Empty, _items!);
    }

    public Order BuildValue()
    {
        return Order.Create(_address!, _clientId ?? Guid.Empty, _items!).Value;
    }
}
```

---

#### Factories

```cs title="OrderDataFactory.cs"
namespace LegacyLego.Domain.Tests.Common.Factories;

internal static class OrderDataFactory
{
    public static Order CreateDefaultOrder()
    {
        var address = OrderAddress.Create("US", "Berlin", "New York", "90210").Value;
        var items = new List<OrderItem>
        {
            OrderItem.Create("Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value
        };
        return Order.Create(address, Guid.NewGuid(), items).Value;
    }
}
```

---

### CurrencyTests

#### Equality

```cs title="CurrencyEqualityTests.cs"
namespace LegacyLego.Domain.Tests.CurrencyTests;

public class CurrencyEqualityTests
{
    [Test]
    public async Task Equals_WithSameCode_ShouldBeTrue()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("usd").Value;

        await Assert.That(c1.Equals(c2)).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentCode_ShouldBeFalse()
    {
        var usd = Currency.FromCode("USD").Value;
        var rub = Currency.FromCode("RUB").Value;

        await Assert.That(usd.Equals(rub)).IsFalse();
    }

    [Test]
    public async Task Equals_ShouldBeConsistentWithEqualsOperator()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("USD").Value;

        await Assert.That(c1 == c2).IsTrue();
    }

    [Test]
    public async Task Equals_ShouldBeConsistentWithNotEqualsOperator()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("RUB").Value;

        await Assert.That(c1 != c2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualObjects_ShouldBeSame()
    {
        var c1 = Currency.FromCode("USD").Value;
        var c2 = Currency.FromCode("usd").Value;

        await Assert.That(c1.GetHashCode()).IsEqualTo(c2.GetHashCode());
    }

    [Test]
    public async Task Equals_ShouldDependOnlyOnCode()
    {
        var usd = Currency.FromCode("USD").Value;

        await Assert.That(usd.Code).IsEqualTo("USD");
    }
}
```

---

#### FromCode

```cs title="CurrencyFromCodeTests.cs"
namespace LegacyLego.Domain.Tests.CurrencyTests;

public class CurrencyFromCodeTests
{
    [Test]
    public async Task FromCode_WithValidCodeUSD_ShouldReturnSuccess()
    {
        var result =  Currency.FromCode("USD");

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Currency.Usd);
        await Assert.That(result.Value).IsSameReferenceAs(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithUnknownValidCode_ShouldReturnNotSupported()
    {
        var result = Currency.FromCode("ABC");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error).Member(error => error.Code,name => name.EqualTo(CurrencyErrors.NotSupportedCode));
    }

    [Test]
    public async Task FromCode_WithEmptyString_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    public async Task FromCode_WithInvalidCodeLength_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("USDDD");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error)
            .Member(error => error.Code, code => code.EqualTo(CurrencyErrors.WrongCodeLengthCode));
    }

    [Test]
    public async Task FromCode_WithNullCode_ShouldThrowArgumentNullException()
    {
        var exception = await Assert.That(() => Currency.FromCode(null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments("usd")]
    [Arguments("Usd")]
    [Arguments("uSd")]
    [Arguments("usD")]
    [Arguments("UsD")]
    public async Task FromCode_WithLowerCaseValidCode_ShouldReturnSuccess(string inputLowerCode)
    {
        var result = Currency.FromCode(inputLowerCode);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Currency.Usd);
        await Assert.That(result.Value).IsSameReferenceAs(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithValidUntrimmedCode_ShouldReturnSuccess()
    {
        var result = Currency.FromCode("    USD  ");

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Currency.Usd);
        await Assert.That(result.Value).IsSameReferenceAs(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithInvalidUntrimmedCode_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("    USDDDDD  ");

        await Assert.That(result.IsSuccess).IsFalse();
        await Assert.That(result.Error.Code).IsEqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    [Arguments("U")]
    [Arguments("US")]
    public async Task FromCode_WithShortCode_ShouldReturnWrongCodeLengthError(string code)
    {
        var result = Currency.FromCode(code);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    [Arguments("USD", "$", 2)]
    [Arguments("RUB", "₽", 2)]
    [Arguments("EUR", "€", 2)]
    public async Task FromCode_ShouldReturnCurrencyWithCorrectProperties(string code,string symbol,int scale)
    {
        var result = Currency.FromCode(code);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Symbol).IsEqualTo(symbol);
        await Assert.That(result.Value.Scale).IsEqualTo(scale);
    }
}
```

---

### OrderItemTests

#### Create

```cs title="OrderItemCreateTests.cs"
namespace LegacyLego.Domain.Tests.OrderItemTests;

public class OrderItemCreateTests
{
    [Test]
    public async Task Create_WithValidValues_ShouldPreserveCurrency()
    {
        var guid = Guid.NewGuid();
        var title = "New Item";
        var quantity = 3;
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(title, quantity, guid, p);

        await Assert.That(r.IsSuccess).IsTrue();
        await Assert.That(r.Value)
            .Member(x => x.ProductId, m => m.IsEqualTo(guid))
            .And.Member(x => x.Title, m => m.IsEqualTo(title))
            .And.Member(x => x.Quantity, m => m.IsEqualTo(quantity))
            .And.Member(x => x.UnitPrice, m => m.IsEqualTo(p));
    }

    [Test]
    public async Task Create_WithUntrimmedTitle_ShouldReturnSuccess()
    {
        var untrimmed = "    untrimmed     ";
        var trimmed = untrimmed.Trim();
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create(untrimmed, 3, Guid.NewGuid(), p);

        await Assert.That(r.IsSuccess).IsTrue();
        await Assert.That(r.Value.Title).IsEqualTo(trimmed);
    }

    [Test]
    public async Task Create_WithNullTitle_ShouldReturnFailureWithTitleInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var action = () => OrderItem.Create(null!, 3, Guid.NewGuid(), p);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithWhiteTitle_ShouldReturnFailureWithTitleInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var action = () => OrderItem.Create("", 3, Guid.NewGuid(), p);

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Create_WithEmptyGuid_ShouldReturnFailureWithProductIDGuidInvalid()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", 3, Guid.Empty, p);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(OrderItemErrors.ProductIDGuidInvalidCode);
    }

    [Test]
    [Arguments(0)]
    [Arguments(-10)]
    public async Task Create_InvalidQuantity_ShouldReturnFailureWithQuantityBelowOne(int quantity)
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", quantity, Guid.NewGuid(), p);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(OrderItemErrors.QuantityBelowOneCode);
    }

    [Test]
    public async Task Create_WithMinimalValidQuantity_ShouldReturnSuccess()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", 1, Guid.NewGuid(), p);

        await Assert.That(r.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Create_WithNullUnitPrice_ShouldThrowArgumentNullException()
    {
        var action = () => OrderItem.Create("New Item", 3, Guid.NewGuid(), null!);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithZeroPrice_ShouldReturnSuccess()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var r = OrderItem.Create("New Item", 1, Guid.NewGuid(), p.MultiplyByQuantity(0));

        await Assert.That(r.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item", 1, guid, p).Value;
        var item2 = OrderItem.Create("New Item", 1, guid, p).Value;

        await Assert.That(item1).IsEqualTo(item2);
        await Assert.That(ReferenceEquals(item1, item2)).IsFalse();
    }

    [Test]
    public async Task Create_EqualObjects_ShouldHaveSameHashCode()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item", 1, guid, p).Value;
        var item2 = OrderItem.Create("New Item", 1, guid, p).Value;

        await Assert.That(item1.GetHashCode()).IsEqualTo(item2.GetHashCode());
    }
}
```

---

#### Equality

```cs title="OrderItemEqualityTests.cs"
namespace LegacyLego.Domain.Tests.OrderItemTests;

public class OrderItemEqualityTests
{
    [Test]
    public async Task Equals_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(tytle, quantity, guid, p).Value;
        var item2 = OrderItem.Create(tytle, quantity, guid, p).Value;


        await Assert.That(item1).IsEqualTo(item2);
        await Assert.That(item1).IsNotSameReferenceAs(item2);
    }

    [Test]
    public async Task EqualsOperator_WitSameValues_ShouldBeTrue()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(tytle, quantity, guid, p).Value;
        var item2 = OrderItem.Create(tytle, quantity, guid, p).Value;

        await Assert.That(item1 == item2).IsTrue();
    }

    [Test]
    public async Task EqualsOperator_WithDifferentValues_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Rub).Value;

        var item1 = OrderItem.Create("New Item", 1, Guid.NewGuid(), p1).Value;
        var item2 = OrderItem.Create("New Item2", 2, Guid.NewGuid(), p2).Value;

        await Assert.That(item1 == item2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(tytle, quantity, guid, p).Value;
        var item2 = OrderItem.Create(tytle, quantity, guid, p).Value;

        await Assert.That(item1 != item2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithDifferentValues_ShouldBeTrue()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Rub).Value;

        var item1 = OrderItem.Create("New Item", 1, Guid.NewGuid(), p1).Value;
        var item2 = OrderItem.Create("New Item2", 2, Guid.NewGuid(), p2).Value;

        await Assert.That(item1 != item2).IsTrue();
    }

    [Test]
    public async Task Equals_WithSameParameters_ShouldNotBeSameReference()
    {
        var guid = Guid.NewGuid();
        var tytle = "New Item";
        var quantity = 3;

        var p = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create(tytle, quantity, guid, p).Value;
        var item2 = OrderItem.Create(tytle, quantity, guid, p).Value;

        await Assert.That(item1).IsNotSameReferenceAs(item2);
    }

    [Test]
    public async Task Equals_WithNull_ShouldBeFalse()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("New Item", 1, Guid.NewGuid(), p).Value;

        await Assert.That(item.Equals(null)).IsFalse();
    }

    [Test]
    public async Task EqualsOperator_WithNull_ShouldBeFalse()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("New Item", 1, Guid.NewGuid(), p).Value;

        await Assert.That(item == null).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithNull_ShouldBeTrue()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("New Item", 1, Guid.NewGuid(), p).Value;

        await Assert.That(item != null).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentType_ShouldBeFalse()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create( "New Item", 1, Guid.NewGuid(), p).Value;

        await Assert.That(item.Equals("not a order item")).IsFalse();
    }

    [Test]
    public async Task EqualsOperator_WithBothNull_ShouldBeTrue()
    {
        OrderItem? a = null;
        OrderItem? b = null;

        await Assert.That(a == b).IsTrue();
    }

    [Test]
    public async Task NotEqualsOperator_WithBothNull_ShouldBeFalse()
    {
        OrderItem? a = null;
        OrderItem? b = null;

        await Assert.That(a != b).IsFalse();
    }

    [Test]
    public async Task Equals_WithSameReference_ShouldBeTrue()
    {
        var p = Price.Create(10m, Currency.Usd).Value;
        var item = OrderItem.Create("New Item", 1, Guid.NewGuid(), p).Value;

        await Assert.That(item.Equals(item)).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentTitle_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(10m, Currency.Usd).Value;

        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item", 1, guid, p1).Value;

        var item2 = OrderItem.Create("New Item2", 1, guid, p2).Value;

        await Assert.That(item1).IsNotEqualTo(item2);
        await Assert.That(item1 == item2).IsFalse();
        await Assert.That(item1 != item2).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentQuantity_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(10m, Currency.Usd).Value;

        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item", 1, guid, p1).Value;
        var item2 = OrderItem.Create("New Item", 2, guid, p2).Value;

        await Assert.That(item1).IsNotEqualTo(item2);
        await Assert.That(item1 == item2).IsFalse();
        await Assert.That(item1 != item2).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentProductId_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(10m, Currency.Usd).Value;

        var item1 = OrderItem.Create("New Item",1,Guid.NewGuid(),p1).Value;
        var item2 = OrderItem.Create("New Item", 1,Guid.NewGuid(),p2).Value;

        await Assert.That(item1).IsNotEqualTo(item2);
        await Assert.That(item1 == item2).IsFalse();
        await Assert.That(item1 != item2).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentUnitPrice_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Rub).Value;

        Guid guid = Guid.NewGuid();

        var item1 = OrderItem.Create("New Item",1,guid,p1).Value;
        var item2 = OrderItem.Create("New Item",1,guid,p2).Value;

        await Assert.That(item1).IsNotEqualTo(item2);
        await Assert.That(item1 == item2).IsFalse();
        await Assert.That(item1 != item2).IsTrue();
    }
}
```

---

#### GetTotalPriceTests

```cs title="OrderItemGetTotalPriceTests.cs"
namespace LegacyLego.Domain.Tests.OrderItemTests;

public class OrderItemGetTotalPriceTests
{
    [Test]
    public async Task GetTotalPrice_WithNormalizedPrice_ShouldEqualsExpected()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("Item", 3, Guid.NewGuid(), p).Value;

        var expectedPrice = Price.Create(30m, Currency.Usd).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total).IsEqualTo(expectedPrice);
    }

    [Test]
    public async Task GetTotalPrice_CurrencyShouldStayConsistent()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("Item", 3, Guid.NewGuid(), p).Value;

        var expectedPrice = Price.Create(30m, Currency.Usd).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total.Currency).IsEqualTo(item.UnitPrice.Currency);
    }

    [Test]
    public async Task GetTotalPrice_WithUnnormalizedPrice_ShouldEqualsExpected()
    {
        // gonna be normalized here at first to 10.56
        var p = Price.Create(10.5552m, Currency.Usd).Value;

        var item = OrderItem.Create("Item", 3, Guid.NewGuid(), p).Value;

        var expectedPrice = Price.Create(31.68m, Currency.Usd).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total).IsEqualTo(expectedPrice);
    }

    [Test]
    public async Task GetTotalPrice_UnitPriceSholdStayConsistent()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("Item", 3, Guid.NewGuid(), p).Value;

        item.GetTotalPrice();

        await Assert.That(item.UnitPrice).IsEqualTo(p);
        await Assert.That(item.UnitPrice).IsSameReferenceAs(p);
    }

    [Test]
    public async Task GetTotalPrice_ItemImmutability()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var originalItem = OrderItem.Create("Item", 3, Guid.NewGuid(), p).Value;

        var item = originalItem;

        item.GetTotalPrice();

        await Assert.That(item).IsEqualTo(originalItem);
    }

    [Test]
    public async Task GetTotalPrice_WithQuantityOne_ShouldReturnUnitPrice()
    {
        var p = Price.Create(10m, Currency.Usd).Value;

        var item = OrderItem.Create("Item", 1, Guid.NewGuid(), p).Value;

        var total = item.GetTotalPrice();

        await Assert.That(total).IsEqualTo(p);
    }
}
```

---

### OrderTests

#### Create

```cs title="OrderCreateTests.cs"
namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderCreateTests
{
    private static OrderAddress DefaultAddress =>
    OrderAddress.Create("US", "Berlin", "New York", "90210").Value;

    private static List<OrderItem> DefaultItems => new List<OrderItem>()
    {
            OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m,Currency.Usd).Value).Value,
            OrderItem.Create("New Item2", 2, Guid.NewGuid(), Price.Create(200m, Currency.Usd).Value).Value,
            OrderItem.Create("New Item3", 3, Guid.NewGuid(), Price.Create(300m, Currency.Usd).Value).Value
    };

    [Test]
    public async Task Create_WithValidValues_ShouldResultSuccess()
    {
        var clientId = Guid.NewGuid();
        var items = DefaultItems;

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(items)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value)
            .Member(o => o.Address, m => m.IsEqualTo(DefaultAddress))
            .And.Member(o => o.ClientId, m => m.EqualTo(clientId))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(items));
    }

    [Test]
    public async Task Create_OrderItemsListShouldNotBeSameReference()
    {
        var clientId = Guid.NewGuid();
        var items = DefaultItems;

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(items)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.Items).IsNotSameReferenceAs(items);
    }

    [Test]
    public async Task Create_OrderItemsListShouldNotMutate()
    {
        var items = DefaultItems;

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        items.Clear();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.Items).IsNotEquivalentTo(items);
    }

    #region Guard Clauses

    [Test]
    public async Task Create_WithAddressNull_ShouldThrowArgumentNullException()
    {
        var action = () =>
        {
            new OrderBuilder()
            .WithNullAddress()
            .WithItems(DefaultItems)
            .WithClientId(Guid.NewGuid())
            .BuildResult();
        };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithOrderItemsNull_ShouldThrowArgumentNullException()
    {
        var action = () =>
        {
            new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNullItems()
            .WithClientId(Guid.NewGuid())
            .BuildResult();
        };

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithOrderItemsListContainsNull_ShouldThrowArgumentNullException()
    {
        var action = () =>
        {
            new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .AddNullItem()
            .WithClientId(Guid.NewGuid())
            .BuildResult();
        };

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    #endregion

    #region Validation Invariants

    [Test]
    public async Task Create_WithClientIdEmpty_ShouldThrowArgumentException()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithEmptyClientId()
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ClientIdGuidInvalidCode);
    }

    [Test]
    public async Task Create_WithOrderItemsEmptyList_ShouldResultFailure()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ItemsCountInvalidCode);
    }

    [Test]
    public async Task Create_WithOrderItemsCurrenciesMismatch_ShouldResultFailure()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value)
            .AddItem(OrderItem.Create("New Item2", 2, Guid.NewGuid(), Price.Create(200m, Currency.Rub).Value).Value)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ItemsCurrenciesMismatchCode);
    }

    [Test]
    public async Task Create_WithOrderItemsTotalPriceZero_ShouldResultFailureWithItemsTotalBelowZeroError()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value.MultiplyByQuantity(0)).Value)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsFailure).IsTrue();
        await Assert.That(order.Error.Code).IsEqualTo(OrderErrors.ItemsTotalBelowZeroCode);
    }

    [Test]
    public async Task Create_WithOrderItemsZeroPrices_ShouldResultSuccess()
    {
        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value.MultiplyByQuantity(0)).Value)
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value.MultiplyByQuantity(0)).Value)
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value)
            .WithClientId(Guid.NewGuid())
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
    }

    #endregion

    [Test]
    public async Task Create_WithValidValues_ShouldReturnOrderWithPendingPaymentStatus()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.Status).IsEqualTo(Enums.OrderStatus.PendingPayment);
    }

    [Test]
    public async Task Create_CreationDateUtcShouldNotBeDefoult()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.CreationDateUtc).IsNotDefault();
    }

    [Test]
    public async Task Create_WithValidValues_ShouldRiseOrderCreatedDomainEvent()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithItems(DefaultItems)
            .WithClientId(clientId)
            .BuildResult();

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderCreated));
    }

    [Test]
    public async Task Create_TotalPriceShouldBeEquivalentAsExpected()
    {
        var clientId = Guid.NewGuid();

        var order = new OrderBuilder()
            .WithAddress(DefaultAddress)
            .WithNoItems()
            .AddItem(OrderItem.Create("New Item1", 1, Guid.NewGuid(), Price.Create(100m, Currency.Usd).Value).Value)
            .AddItem(OrderItem.Create("New Item2", 2, Guid.NewGuid(), Price.Create(200m, Currency.Usd).Value).Value)
            .AddItem(OrderItem.Create("New Item3", 3, Guid.NewGuid(), Price.Create(300m, Currency.Usd).Value).Value)
            .WithClientId(clientId)
            .BuildResult();

        var expected = 1400m;

        await Assert.That(order.IsSuccess).IsTrue();
        await Assert.That(order.Value.TotalPrice.Sum).IsEqualTo(expected);
    }
}
```

---

#### StateTransitions

##### Cancel

```cs title="OrderCancelTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderCancelTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_FromPending_ShouldSucceedAndChangeStatus(Order order)
    {
        var statusBefore = order.Status;

        var cancel = order.Cancel();
        var statusAfter = order.Status;

        await Assert.That(cancel.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(OrderStatus.PendingPayment);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_ShouldRiseOrderCancelledDomainEvent(Order order)
    {
        var cancel = order.Cancel();

        await Assert.That(cancel.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderCanceled));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterExpire_ShouldResultSuccess(Order order)
    {
        var expire = order.Expire();

        var cancel = order.Cancel();

        await Assert.That(cancel.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).Count().IsEqualTo(3)
            .And.Contains(e => e.GetType() == typeof(OrderCreated))
            .And.Contains(e => e.GetType() == typeof(OrderExpired))
            .And.Contains(e => e.GetType() == typeof(OrderCanceled));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterPay_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var payment = order.Pay();
        order.ClearDomainEvents();

        var cancel = order.Cancel();

        await Assert.That(cancel.IsFailure).IsTrue();
        await Assert.That(cancel.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderCanceled));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var paiment = order.Pay();

        var rafund = order.Refund();
        order.ClearDomainEvents();

        var cancel = order.Cancel();

        await Assert.That(cancel.IsFailure).IsTrue();
        await Assert.That(cancel.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderCanceled));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_CancelAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstCancel = order.Cancel();
        order.ClearDomainEvents();
        var secondCancel = order.Cancel();

        await Assert.That(firstCancel.IsSuccess).IsTrue();
        await Assert.That(secondCancel.IsFailure).IsTrue();
        await Assert.That(secondCancel.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderCanceled));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Cancel_ValuesAreSameAfterCancel(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var cancel = order.Cancel();

        await Assert.That(cancel.IsSuccess).IsTrue();

        await Assert.That(order)
            .Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.Id, m => m.IsEqualTo(orderID))
            .And.Member(o => o.ClientId, m => m.IsEqualTo(clientID))
            .And.Member(o => o.CreationDateUtc, m => m.IsEqualTo(orderCreatedAt))
            .And.Member(o => o.Address, m => m.IsEqualTo(orderAddress))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(afterCreatingItems));
    }
}
```

---

##### Expire

```cs title="OrderExpireTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderExpireTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_FromPending_ShouldSucceedAndChangeStatus(Order order)
    {
        var statusBefore = order.Status;

        var expire = order.Expire();
        var statusAfter = order.Status;

        await Assert.That(expire.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(OrderStatus.PendingPayment);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Expired);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ShouldRiseOrderExpiredDomainEvent(Order order)
    {
        var expire = order.Expire();

        await Assert.That(expire.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderExpired));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ExpireAfterExpire_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstExpire = order.Expire();
        order.ClearDomainEvents();
        var secondExpire = order.Expire();

        await Assert.That(firstExpire.IsSuccess).IsTrue();
        await Assert.That(secondExpire.IsFailure).IsTrue();
        await Assert.That(secondExpire.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderExpired));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Expired);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ExpireAfterPay_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var payment = order.Pay();
        order.ClearDomainEvents();

        var expire = order.Expire();

        await Assert.That(expire.IsFailure).IsTrue();
        await Assert.That(expire.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderExpired));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ExpireAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var payment = order.Pay();

        var refund = order.Refund();
        order.ClearDomainEvents();

        var expire = order.Expire();

        await Assert.That(expire.IsFailure).IsTrue();
        await Assert.That(expire.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderExpired));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ExpireAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var cancell = order.Cancel();
        order.ClearDomainEvents();

        var expire = order.Expire();

        await Assert.That(expire.IsFailure).IsTrue();
        await Assert.That(expire.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Expire_ValuesAreSameAfterExpire(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var expire = order.Expire();

        await Assert.That(expire.IsSuccess).IsTrue();

        await Assert.That(order)
            .Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.Id, m => m.IsEqualTo(orderID))
            .And.Member(o => o.ClientId, m => m.IsEqualTo(clientID))
            .And.Member(o => o.CreationDateUtc, m => m.IsEqualTo(orderCreatedAt))
            .And.Member(o => o.Address, m => m.IsEqualTo(orderAddress))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(afterCreatingItems));
    }
}
```

---

##### Pay

```cs title="OrderPayTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderPayTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_FromPending_ShouldSucceedAndChangeStatus(Order order)
    {
        var statusBefore = order.Status;

        var payment = order.Pay();
        var statusAfter = order.Status;

        await Assert.That(payment.IsSuccess).IsTrue();
        await Assert.That(statusBefore).IsEqualTo(OrderStatus.PendingPayment);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Paid);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_ShouldRiseOrderPaidDomainEvent(Order order)
    {
        var payment = order.Pay();

        await Assert.That(payment.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).HasSingleItem(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterPay_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstPay = order.Pay();
        order.ClearDomainEvents();
        var secondPay = order.Pay();

        await Assert.That(firstPay.IsSuccess).IsTrue();
        await Assert.That(secondPay.IsFailure).IsTrue();
        await Assert.That(secondPay.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Paid);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterExpire_ShouldResultSuccess(Order order)
    {
        var expire = order.Expire();

        var paiment = order.Pay();

        await Assert.That(expire.IsSuccess).IsTrue();
        await Assert.That(paiment.IsSuccess).IsTrue();
        await Assert.That(order.DomainEvents).Count().IsEqualTo(3)
            .And.Contains(e => e.GetType() == typeof(OrderCreated))
            .And.Contains(e => e.GetType() == typeof(OrderExpired))
            .And.Contains(e => e.GetType() == typeof(OrderPaid));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Paid);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var firstPayment = order.Pay();

        var refund = order.Refund();
        order.ClearDomainEvents();

        var secondPayment = order.Pay();

        await Assert.That(firstPayment.IsSuccess).IsTrue();
        await Assert.That(refund.IsSuccess).IsTrue();
        await Assert.That(secondPayment.IsFailure).IsTrue();
        await Assert.That(secondPayment.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_PayAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var cancell = order.Cancel();
        order.ClearDomainEvents();

        var payment = order.Pay();

        await Assert.That(cancell.IsSuccess).IsTrue();
        await Assert.That(payment.IsFailure).IsTrue();
        await Assert.That(payment.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderPaid));
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_ValuesAreSameAfterPayment(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var payment = order.Pay();

        await Assert.That(payment.IsSuccess).IsTrue();

        await Assert.That(order)
            .Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.Id, m => m.IsEqualTo(orderID))
            .And.Member(o => o.ClientId, m => m.IsEqualTo(clientID))
            .And.Member(o => o.CreationDateUtc, m => m.IsEqualTo(orderCreatedAt))
            .And.Member(o => o.Address, m => m.IsEqualTo(orderAddress))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(afterCreatingItems));
    }
}
```

---

##### Refund

```cs title="OrderRefundTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderRefundTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_FromPending_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var refund = order.Refund();
        var statusAfter = order.Status;

        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.PendingPayment);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterExpire_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var expire = order.Expire();
        order.ClearDomainEvents();
        var refund = order.Refund();

        await Assert.That(refund.IsFailure).IsTrue();
        await Assert.That(refund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));
        await Assert.That(order.Status).IsEqualTo(OrderStatus.Expired);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterPay_ShouldResultSuccess(Order order)
    {
        var payment = order.Pay();

        var statusBefore = order.Status;

        var refund = order.Refund();

        var statusAfter = order.Status;

        await Assert.That(refund.IsSuccess).IsTrue();

        await Assert.That(order.DomainEvents).Count().IsEqualTo(3)
            .And.Contains(e => e.GetType() == typeof(OrderCreated))
            .And.Contains(e => e.GetType() == typeof(OrderPaid))
            .And.Contains(e => e.GetType() == typeof(OrderRefunded));

        await Assert.That(statusBefore).IsEqualTo(OrderStatus.Paid);
        await Assert.That(statusAfter).IsEqualTo(OrderStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterRefund_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var payment = order.Pay();

        var firstRefund = order.Refund();
        order.ClearDomainEvents();

        var secondRefund = order.Refund();

        await Assert.That(firstRefund.IsSuccess).IsTrue();
        await Assert.That(secondRefund.IsFailure).IsTrue();

        await Assert.That(secondRefund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));

        await Assert.That(order.Status).IsEqualTo(OrderStatus.Refunded);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_RefundAfterCancel_ShouldResultFailureWithStatusTransitionFailureError(Order order)
    {
        var cancell = order.Cancel();
        order.ClearDomainEvents();

        var refund = order.Refund();

        await Assert.That(refund.IsFailure).IsTrue();

        await Assert.That(refund.Error.Code).IsEqualTo(OrderErrors.StatusTransitionFailureCode);
        await Assert.That(order.DomainEvents).DoesNotContain(e => e.GetType() == typeof(OrderRefunded));

        await Assert.That(order.Status).IsEqualTo(OrderStatus.Cancelled);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Refund_ValuesAreSameAfterRefund(Order order)
    {
        var totalSum = order.TotalPrice.Sum;
        var orderID = order.Id;
        var clientID = order.ClientId;
        var orderCreatedAt = order.CreationDateUtc;
        var orderAddress = order.Address;
        var afterCreatingItems = order.Items;

        var payment = order.Pay();
        var refund = order.Refund();

        await Assert.That(refund.IsSuccess).IsTrue();

        await Assert.That(order)
            .Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.TotalPrice.Sum, m => m.IsEqualTo(totalSum))
            .And.Member(o => o.Id, m => m.IsEqualTo(orderID))
            .And.Member(o => o.ClientId, m => m.IsEqualTo(clientID))
            .And.Member(o => o.CreationDateUtc, m => m.IsEqualTo(orderCreatedAt))
            .And.Member(o => o.Address, m => m.IsEqualTo(orderAddress))
            .And.Member(o => o.Items, m => m.IsEquivalentTo(afterCreatingItems));
    }
}
```

---

#### TotalPrice

```cs title="OrderTotalPriceTests.cs"
using LegacyLego.Domain.Tests.Common.Factories;

namespace LegacyLego.Domain.Tests.OrderTests;

public class OrderTotalPriceTests
{
    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task TotalPrice_WhenPending_ShouldEqualSumOfItems(Order order)
    {
        var expected = order.Items
            .Select(i => i.GetTotalPrice())
            .Aggregate((a, b) => a.Plus(b));

        await Assert.That(order.TotalPrice).IsEqualTo(expected);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task TotalPrice_AfterPay_ShouldRemainSame(Order order)
    {
        var before = order.TotalPrice;

        order.Pay();

        var after = order.TotalPrice;

        await Assert.That(after).IsEqualTo(before);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task TotalPrice_WhenExpired_ShouldStillBeCalculated(Order order)
    {
        var before = order.TotalPrice;

        order.Expire();

        var after = order.TotalPrice;

        await Assert.That(after).IsEqualTo(before);
    }

    [Test]
    [MethodDataSource(typeof(OrderDataFactory), nameof(CreateDefaultOrder))]
    public async Task Pay_ShouldSetFrozenTotalPrice_Implicitly(Order order)
    {
        order.Pay();

        // если _frozenTotalPrice не установлен,
        // этот вызов бросит InvalidDomainStateException
        var total = order.TotalPrice;

        await Assert.That(total).IsNotNull();
    }
}
```

---

### PriceTests

#### Create

```cs title="PriceCreateTests.cs"
namespace LegacyLego.Domain.Tests.PriceTests;

public class PriceCreateTests
{
    [Test]
    public async Task Create_ShouldPreserveCurrency()
    {
        var result = Price.Create(10m, Currency.Eur);

        await Assert.That(result.Value.Currency).IsEqualTo(Currency.Eur);
    }

    [Test]
    public async Task Create_WithNullCurrency_ShouldThrowArgumentNullException()
    {
        await Assert.That(() => Price.Create(100m, null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithValidParameters_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(100m, currency);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Sum).IsEqualTo(100m);
        await Assert.That(result.Value.Currency).IsEqualTo(currency);
    }

    [Test]
    public async Task Create_WithNegativeSum_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(-100m, currency);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(PriceErrors.SumBelowZeroCode);
    }

    [Test]
    public async Task Create_WithZeroSum_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(0m, currency);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(PriceErrors.SumBelowZeroCode);
    }

    [Test]
    public async Task Create_WithZeroNormalizedSum_ShouldReturnSumBelowZeroError()
    {
        var currency = Currency.Usd;
        var result = Price.Create(0.0004m, currency);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(PriceErrors.SumBelowZeroCode);
    }

    [Test]
    public async Task Create_WithMaximalDecimalSum_ShoulReturnSuccess()
    {
        var currency = Currency.Usd;
        var result = Price.Create(decimal.MaxValue, currency);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Sum).IsEqualTo(decimal.MaxValue);
        await Assert.That(result.Value.Currency).IsEqualTo(currency);
    }

    [Test]
    public async Task Create_ShouldNormalizeSumAccordingToCurrencyScale()
    {
        var currency = Currency.Usd;

        // 10.555 → 10.56 (banker's rounding)
        var result = Price.Create(10.555m, currency);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Sum).IsEqualTo(10.56m);
    }

    [Test]
    public async Task Create_WithMinimalPositiveValue_ShouldReturnSuccess()
    {
        var currency = Currency.Usd;

        var result = Price.Create(0.01m, currency);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Sum).IsEqualTo(0.01m);
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var currency = Currency.Usd;

        var p1 = Price.Create(10m, currency).Value;
        var p2 = Price.Create(10m, currency).Value;

        await Assert.That(p1).IsEqualTo(p2);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task EqualObjects_ShouldHaveSameHashCode()
    {
        var currency = Currency.Usd;

        var p1 = Price.Create(10m, currency).Value;
        var p2 = Price.Create(10m, currency).Value;

        await Assert.That(p1.GetHashCode()).IsEqualTo(p2.GetHashCode());
    }
}
```

---

#### Equality

```cs title="PriceEqualityTests.cs"
namespace LegacyLego.Domain.Tests.PriceTests;

public class PriceEqualityTests
{
    [Test]
    public async Task Equals_WithSamePrice_ShouldBeTrue()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1).IsEqualTo(p2);
    }

    [Test]
    public async Task Equals_WithDifferentValues_ShouldBeFalse()
    {
        var p1 = Price.Create(10m, Currency.Rub).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1).IsNotEqualTo(p2);
    }

    [Test]
    public async Task EqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1 == p2).IsTrue();
    }

    [Test]
    public async Task EqualsOperator_WithDifferentValues_ShouldBeFalse()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        await Assert.That(p1 == p2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1 != p2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithDifferentValues_ShouldBeTrue()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        await Assert.That(p1 != p2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualObjects_ShouldBeSame()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1.GetHashCode()).IsEqualTo(p2.GetHashCode());
    }

    [Test]
    public async Task GetHashCode_ForDifferentObjects_ShouldBeDifferent()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        await Assert.That(p1.GetHashCode()).IsNotEqualTo(p2.GetHashCode());
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnDifferentInstances()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task Equals_WithNull_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price.Equals(null)).IsFalse();
    }

    [Test]
    public async Task EqualsOperator_WithNull_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price == null).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithNull_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price != null).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentType_ShouldBeFalse()
    {
        var price = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(price.Equals("not a price")).IsFalse();
    }

    [Test]
    public async Task Equals_WithNormalizedValues_ShouldBeTrue()
    {
        var p1 = Price.Create(100.000m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        await Assert.That(p1).IsEqualTo(p2);
    }
}
```

---

#### MultiplyByQuantity

```cs title="PriceMultiplyByQuantityTests.cs"
namespace LegacyLego.Domain.Tests.PriceTests;

public class PriceMultiplyByQuantityTests
{
    [Test]
    public async Task MultiplyByQuantity_WithValidFactor_ShouldReturnExpectedPrice()
    {
        int factor = 3;
        decimal initSum = 100m;
        decimal expectedSum = initSum * factor;

        var p1 = Price.Create(initSum,Currency.Usd).Value;

        var p2 = p1.MultiplyByQuantity(3);

        await Assert.That(p2.Sum).IsEqualTo(expectedSum);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task MultiplyByQuantity_WithBelowZeroFactor_ShouldThrowInvariantViolationException()
    {
        int factor = -2;
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var exception = await Assert.That(() => p1.MultiplyByQuantity(factor))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(ex => ex.Code,code => code.EqualTo(PriceExceptionalErrors.MultiplyBelowZeroErrorCode));
    }

    [Test]
    public async Task MultiplyByQuantity_WithZeroFactor_ShouldReturnValidPrice()
    {
        int factor = 0;
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var p2 = p1.MultiplyByQuantity(factor);

        await Assert.That(p2.Sum).IsEqualTo(0);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task MultiplyByQuantity_WithMinimalValidFactor_ShouldReturnValidPrice()
    {
        int factor = 1;
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var p2 = p1.MultiplyByQuantity(factor);

        await Assert.That(p2.Sum).IsEqualTo(initSum);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(ReferenceEquals(p1, p2)).IsFalse();
    }

    [Test]
    public async Task MultiplyByQuantity_DecimalOverflow_ShouldThrowInvariantViolationException()
    {
        int factor = 2;
        decimal initSum = decimal.MaxValue;

        var p1 = Price.Create(initSum, Currency.Usd).Value;

        var exception = await Assert.That(() => p1.MultiplyByQuantity(factor))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(ex => ex.Code, code => code.EqualTo(PriceExceptionalErrors.DecimalSumOverflowErrorCode));
    }

    [Test]
    public async Task MultiplyByQuantity_ShouldNormalizeResult()
    {
        int factor = 3;
        decimal initSum = 10.555m;

        var p = Price.Create(initSum, Currency.Usd).Value;

        var result = p.MultiplyByQuantity(factor);

        await Assert.That(result.Sum).IsEqualTo(31.68m);
    }

    [Test]
    public async Task MultiplyByQuantity_ShouldNotModifyOriginalPrice()
    {
        int factor = 3;
        decimal initSum = 100m;

        var p = Price.Create(initSum, Currency.Usd).Value;

        var result = p.MultiplyByQuantity(factor);

        await Assert.That(p).IsEqualTo(Price.Create(initSum, Currency.Usd).Value);
    }
}
```

---

#### Plus

```cs title="PricePlusTests.cs"
namespace LegacyLego.Domain.Tests.PriceTests;

public class PricePlusTests
{
    [Test]
    public async Task Plus_WithValidPrices_ShouldReturnExpectedPrice()
    {
        decimal initSum = 100m;
        decimal expectedSum = 200m;

        Price expectedPrice = Price.Create(expectedSum, Currency.Usd).Value;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.Plus(p2);

        await Assert.That(pSum).IsEqualTo(expectedPrice);

        await Assert.That(ReferenceEquals(pSum, p1)).IsFalse();
        await Assert.That(ReferenceEquals(pSum, p2)).IsFalse();
    }

    [Test]
    public async Task Plus_WithUnnormalizedPrices_ShouldReturnExpectedPrice()
    {
        decimal initSum = 10.5551m;
        decimal expectedSum = 21.12m;

        Price expectedPrice = Price.Create(expectedSum, Currency.Usd).Value;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.Plus(p2);

        await Assert.That(pSum).IsEqualTo(expectedPrice);

        await Assert.That(ReferenceEquals(pSum, p1)).IsFalse();
        await Assert.That(ReferenceEquals(pSum, p2)).IsFalse();
    }

    [Test]
    public async Task Plus_WithDifferentCurrencies_ShouldThrowInvariantViolationExceptionWithCurrencyMismatchCode()
    {
        decimal initSum = 100m;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Rub).Value;

        var exception = await Assert.That(() => p1.Plus(p2))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(ex => ex.Code, code => code.EqualTo(PriceExceptionalErrors.CurrencyMismatchErrorCode));
    }

    [Test]
    public async Task Plus_WithSumDecimalOverflow_ShouldThrowInvariantViolationExceptionWithDecimalSumOverflowCode()
    {
        decimal initSum = decimal.MaxValue;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var exception = await Assert.That(() => p1.Plus(p2))
            .ThrowsExactly<InvariantViolationException>();

        await Assert.That(exception?.Error).IsNotNull()
            .And.Member(err => err.Code, code => code.EqualTo(PriceExceptionalErrors.DecimalSumOverflowErrorCode));
    }

    [Test]
    public async Task Plus_WithBoundaryValues_ShouldNotOverflow()
    {
        var p1 = Price.Create(decimal.MaxValue - 1, Currency.Usd).Value;
        var p2 = Price.Create(1m, Currency.Usd).Value;

        var result = p1.Plus(p2);

        await Assert.That(result.Sum).IsEqualTo(decimal.MaxValue);
    }

    [Test]
    public async Task Plus_ShouldNotModifyOriginalPrices()
    {
        decimal initSum = 100m;
        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.Plus(p2);

        await Assert.That(p1.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(p2.Currency).IsEqualTo(Currency.Usd);

        await Assert.That(p1.Sum).IsEqualTo(initSum);
        await Assert.That(p2.Sum).IsEqualTo(initSum);
    }

    [Test]
    public async Task Plus_WithMultiplyByQuantityAsParametersNormalized_ShouldReturnExpectedPrice()
    {
        decimal initSum = 100m;
        decimal expectedSum = 600m;

        Price expectedPrice = Price.Create(expectedSum, Currency.Usd).Value;

        var p1 = Price.Create(initSum, Currency.Usd).Value;
        var p2 = Price.Create(initSum, Currency.Usd).Value;

        var pSum = p1.MultiplyByQuantity(2).Plus(p2.MultiplyByQuantity(4));

        await Assert.That(pSum).IsEqualTo(expectedPrice);
    }

    [Test]
    public async Task Plus_WithMultiplyByQuantityAsParametersZero_ShouldReturnZeroPrice()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(100m, Currency.Usd).Value;

        var pSum = p1.MultiplyByQuantity(0).Plus(p2.MultiplyByQuantity(0));

        await Assert.That(pSum.Currency).IsEqualTo(Currency.Usd);
        await Assert.That(pSum.Sum).IsEqualTo(0);
    }

    [Test]
    public async Task Plus_WithZeroPrice_ShouldReturnSamePrice()
    {
        var p = Price.Create(100m, Currency.Usd).Value;
        var zero = p.MultiplyByQuantity(0);

        var result = p.Plus(zero);

        await Assert.That(result).IsEqualTo(p);
    }

    [Test]
    public async Task Plus_ShouldBeCommutative()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var p2 = Price.Create(200m, Currency.Usd).Value;

        var r1 = p1.Plus(p2);
        var r2 = p2.Plus(p1);

        await Assert.That(r1).IsEqualTo(r2);
    }

    [Test]
    public async Task Plus_ShouldBeAssociative()
    {
        var p1 = Price.Create(10.555m, Currency.Usd).Value;
        var p2 = Price.Create(20.111m, Currency.Usd).Value;
        var p3 = Price.Create(30.333m, Currency.Usd).Value;

        var r1 = p1.Plus(p2).Plus(p3);
        var r2 = p1.Plus(p2.Plus(p3));

        await Assert.That(r1).IsEqualTo(r2);
    }

    [Test]
    public async Task Plus_ShouldBeCommutativeWithZero()
    {
        var p1 = Price.Create(100m, Currency.Usd).Value;
        var zero = p1.MultiplyByQuantity(0);

        var r1 = p1.Plus(zero);

        await Assert.That(r1).IsEqualTo(p1);
    }
}
```

---


