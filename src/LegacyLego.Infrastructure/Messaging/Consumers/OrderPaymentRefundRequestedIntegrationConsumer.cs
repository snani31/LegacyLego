using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Payments.IntegrationEvents;
using LegacyLego.Infrastructure.Messaging.Abstractions;

namespace LegacyLego.Infrastructure.Messaging.Consumers;

public class OrderPaymentRefundRequestedIntegrationConsumer : IIntegrationEventConsumer<OrderPaymentRefundRequestedIntegrationEvent>
{
    private readonly IPaymentProvider _paymentProvider;

    public OrderPaymentRefundRequestedIntegrationConsumer(IPaymentProvider paymentProvider)
    {
        _paymentProvider = paymentProvider;
    }

    public async Task HandleAsync(OrderPaymentRefundRequestedIntegrationEvent notification, CancellationToken ct)
    {
        var result = await _paymentProvider.RequestRefundAsync(
             notification.OrderId,
             notification.Amount,
             notification.Currency,
             notification.TransactionId,
             ct);

        if (result.IsFailure)
        {
            throw new InvalidOperationException(result.Error.Message);
        }
    }
}
