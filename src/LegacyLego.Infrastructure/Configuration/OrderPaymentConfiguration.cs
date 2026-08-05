using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LegacyLego.Infrastructure.Configuration.Common;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public class OrderPaymentConfiguration : IEntityTypeConfiguration<OrderPayment>
{
    #region CostraintNames
    private const string CHECK_ORDER_PAYMENT_CONSTRAINT_NAME = "check_order_payment_status";
    private const string PK_CONSTRAINT_NAME = "pk_order_payment";
    private const string FK_ORDER_PAYMENT_ORDERS_CONSTRAINT_NAME = "fk_order_payment_orders"; 
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "Order_payment";
    private const string ID_COLUMN_NAME = "id";
    private const string STATUS_COLUMN_NAME = "status";
    private const string TRANSACTION_ID_COLUMN_NAME = "transaction_id";
    private const string CREATED_AT_UTC_COLUMN_NAME = "created_at_utc";
    private const string ORDER_ID_COLUMN_NAME = "order_id";

    private const string EXPECTED_AMOUNT_COLUMN_NAME = "expected_amount";
    private const string EXPECTED_CURRENCY_COLUMN_NAME = "expected_currency";

    private const string ACTUAL_AMOUNT_COLUMN_NAME = "actual_amount";
    private const string ACTUAL_CURRENCY_COLUMN_NAME = "actual_currency";
    #endregion

    public void Configure(EntityTypeBuilder<OrderPayment> builder)
    {
        builder.ToTable(TABLE_NAME,
               t => t.HasEnumCheckConstraint<PaymentStatus>(CHECK_ORDER_PAYMENT_CONSTRAINT_NAME, STATUS_COLUMN_NAME));

        #region id
        builder.HasKey(x => x.Id).HasName(PK_CONSTRAINT_NAME);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasColumnType(Uuid)
            .HasConversion(id => id.Value, value => OrderPaymentId.From(value))
            .HasColumnName(ID_COLUMN_NAME);
        #endregion

        #region transaction_id
        builder.HasIndex(x => x.TransactionId)
            .IsUnique()
            .HasFilter("transaction_id IS NOT NULL");
        builder.Property(x => x.TransactionId)
            .HasPostgresVarchar(255)
            .IsRequired(false)
            .HasColumnName(TRANSACTION_ID_COLUMN_NAME);
        #endregion

        #region created_at_utc
        builder.Property(p => p.CreatedAtUtc)
            .HasColumnType(TimeStampTz)
            .IsRequired()
            .HasColumnName(CREATED_AT_UTC_COLUMN_NAME);
        #endregion

        #region status
        builder.Property(p => p.Status)
            .HasPostgresVarchar(50, allowStringConversion:true)
            .IsRequired()
            .HasColumnName(STATUS_COLUMN_NAME);
        #endregion

        #region order_id
        builder.Property(p => p.OrderId)
            .HasConversion(id => id.Value, value => OrderId.From(value))
            .HasColumnName(ORDER_ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .IsRequired();

        #region Price ExpectedAmount (NOT NULL)
        builder.ComplexProperty(x => x.ExpectedAmount, priceBuilder =>
        {
            priceBuilder.Property(p => p.Sum)
                .HasColumnType(Numeric(15, 2))
                .HasColumnName(EXPECTED_AMOUNT_COLUMN_NAME)
                .IsRequired();

            priceBuilder.Property(p => p.Currency)
                .HasConversion(c => c.Code, code => Currency.FromCode(code).Value)
                .HasPostgresVarchar(3)
                .HasColumnName(EXPECTED_CURRENCY_COLUMN_NAME)
                .IsRequired();
        });
        #endregion

        #region Price ActualAmount (NULLABLE)
        builder.ComplexProperty(x => x.ActualAmount, priceBuilder =>
        {
            priceBuilder.Property(p => p.Sum)
                .HasPrecision(15, 2)
                .HasColumnName(ACTUAL_AMOUNT_COLUMN_NAME);

            priceBuilder.Property(p => p.Currency)
                .HasConversion(c => c.Code, code => Currency.FromCode(code).Value)
                .HasPostgresVarchar(3)
                .HasColumnName(ACTUAL_CURRENCY_COLUMN_NAME);
        });
        #endregion

        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(p => p.OrderId)
            .HasConstraintName(FK_ORDER_PAYMENT_ORDERS_CONSTRAINT_NAME);
        #endregion

        #region ExternalSession
        builder.HasOne(p => p.ExternalSession)
            .WithOne()
            .HasForeignKey<ExternalSession>("OrderPaymentId")
            .IsRequired(false); 
        #endregion
    }
}