namespace LegacyLego.Domain.Tests.Common.Factories;

internal static class OrderPaymentDataFactory
{
    public static OrderPayment CreateDefaultOrderPayment()
    {
        return OrderPayment.Create(OrderId.New(), DateTime.UtcNow).Value;
    }
}
