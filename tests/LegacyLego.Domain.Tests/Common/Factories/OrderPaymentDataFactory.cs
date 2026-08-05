namespace LegacyLego.Domain.Tests.Common.Factories;

internal static class OrderPaymentDataFactory
{
    public static OrderPayment CreateDefaultOrderPayment()
        => CreateDefaultOrderPayment(100m);

    public static OrderPayment CreateDefaultOrderPayment(decimal amount)
    {
        var price = PriceDataFactory.CreatePrice(amount);
        return OrderPayment.Create(OrderId.New(), price, DateTime.UtcNow).Value;
    }

    public static OrderPayment CreateSucceededOrderPayment(decimal amount = 100m, string txId = "tx-123")
    {
        var payment = CreateDefaultOrderPayment(amount);
        var price = PriceDataFactory.CreatePrice(amount);
        payment.RegisterPaymentReceipt(txId, price);
        payment.ClearDomainEvents();
        return payment;
    }

    public static OrderPayment CreateRefundRequestedOrderPayment(decimal expectedAmount = 100m, decimal actualAmount = 50m, string txId = "tx-123")
    {
        var payment = CreateDefaultOrderPayment(expectedAmount);
        var mismatchedPrice = PriceDataFactory.CreatePrice(actualAmount);
        payment.RegisterPaymentReceipt(txId, mismatchedPrice);
        payment.ClearDomainEvents();
        return payment;
    }

    public static OrderPayment CreateRefundedOrderPayment(string txId = "tx-123")
    {
        var payment = CreateRefundRequestedOrderPayment(txId: txId);
        payment.MarkAsRefunded(txId);
        payment.ClearDomainEvents();
        return payment;
    }

    public static OrderPayment CreateFailedOrderPayment()
    {
        var payment = CreateDefaultOrderPayment();
        payment.MarkAsFailed();
        payment.ClearDomainEvents();
        return payment;
    }
}
