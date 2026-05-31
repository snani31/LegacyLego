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
    private const string ORDER_PAYMENT_ID_COLUMN_NAME = "order_payment_id"; 
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

        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(p => p.OrderId)
            .HasConstraintName(FK_ORDER_PAYMENT_ORDERS_CONSTRAINT_NAME);
        #endregion

        #region ExternalSession
        builder.HasOne(p => p.ExternalSession)
            .WithOne()
            .HasForeignKey<ExternalSession>(ORDER_PAYMENT_ID_COLUMN_NAME)
            .IsRequired(false); 
        #endregion
    }
}