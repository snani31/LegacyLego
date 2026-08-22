using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    #region CostraintNames
    private const string PK_CONSTRAINT_NAME = "pk_order";
    private const string CHECK_ORDER_STATUS_CONSTRAINT_NAME = "check_order_status";
    private const string CHECK_FROZEN_TOTAL_SUM_CONSTRAINT_NAME = "check_order_frozen_total_sun";
    private const string FK_ORDER_ORDER_ITEMS_CONSTRAINT_NAME = "fk_order_order_items";
    private const string FK_ORDER_CLIENT_CONSTRAINT_NAME = "fk_order_client";
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "Order";
    private const string ID_COLUMN_NAME = "id";
    private const string STATUS_COLUMN_NAME = "status";
    private const string CREATED_AT_UTC_COLUMN_NAME = "created_at_utc";
    private const string CLIENT_ID_COLUMN_NAME = "client_id";
    private const string FROZEN_TOTAL_SUM_COLUMN_NAME = "frozen_total_sum";
    private const string CURRENCY_CODE_COLUMN_NAME = "currency_code";

    private const string ADDRESS_COUNTRY_COLUMT_NAME = "address_country";
    private const string ADDRESS_CITY_COLUMT_NAME = "address_city";
    private const string ADDRESS_STREET_COLUMT_NAME = "address_street";
    private const string ADDRESS_POSTAL_CODE_COLUMT_NAME = "address_postal_code";
    #endregion

    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TABLE_NAME,
               t =>
               {
                   t.HasEnumCheckConstraint<OrderStatus>(CHECK_ORDER_STATUS_CONSTRAINT_NAME, STATUS_COLUMN_NAME);
                   t.HasCheckConstraint(CHECK_FROZEN_TOTAL_SUM_CONSTRAINT_NAME, $"\"{FROZEN_TOTAL_SUM_COLUMN_NAME}\" >= 0");
               });

        #region id
        builder.HasKey(o => o.Id).HasName(PK_CONSTRAINT_NAME);

        builder.Property(o => o.Id)
            .ValueGeneratedNever()
            .HasColumnType(Uuid)
            .HasConversion(id => id.Value, value => OrderId.From(value))
            .HasColumnName(ID_COLUMN_NAME);
        #endregion

        #region status
        builder.Property(o => o.Status)
            .HasPostgresVarchar(50, allowStringConversion: true)
            .HasColumnName(STATUS_COLUMN_NAME)
            .IsRequired();
        #endregion

        #region created_at_utc
        builder.Property(o => o.CreationDateUtc)
            .HasColumnName(CREATED_AT_UTC_COLUMN_NAME)
            .HasColumnType(TimeStampTz)
            .IsRequired();
        #endregion

        #region currency_code
        builder.Property(o => o.Currency)
            .HasColumnName(CURRENCY_CODE_COLUMN_NAME)
            .HasPostgresVarchar(3)
            .HasConversion(c => c.Code, code => Currency.FromCode(code).Value)
            .IsRequired();
        #endregion

        #region Address VO
        builder.ComplexProperty(o => o.Address, address =>
        {
            address.Property(a => a.Country)
                .HasColumnName(ADDRESS_COUNTRY_COLUMT_NAME)
                .HasPostgresVarchar(100)
                .IsRequired();

            address.Property(a => a.City)
                .HasColumnName(ADDRESS_CITY_COLUMT_NAME)
                .HasPostgresVarchar(100)
                .IsRequired();

            address.Property(a => a.Street)
                .HasColumnName(ADDRESS_STREET_COLUMT_NAME)
                .HasPostgresVarchar(255)
                .IsRequired();

            address.Property(a => a.PostalCode)
                .HasColumnName(ADDRESS_POSTAL_CODE_COLUMT_NAME)
                .HasPostgresVarchar(20)
                .IsRequired();
        });
        #endregion

        #region Items FK
        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .HasConstraintName(FK_ORDER_ORDER_ITEMS_CONSTRAINT_NAME); 
        #endregion

        #region frozen_total_sum
        builder.Property<decimal?>("FrozenTotalSum")
            .HasColumnName(FROZEN_TOTAL_SUM_COLUMN_NAME)
            .HasColumnType(Numeric(15,2))
            .IsRequired(false);
        #endregion

        #region client_id FK
        builder.Property(o => o.ClientId)
            .HasColumnName(CLIENT_ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .IsRequired()
            .HasConversion(id => id.Value, value => ClientId.From(value).Value);

        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(o => o.ClientId)
            .HasConstraintName(FK_ORDER_CLIENT_CONSTRAINT_NAME)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
    }
}