using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    #region CostraintNames
    private const string PK_CONSTRAINT_NAME = "pk_order_item";
    private const string CHECK_UNIT_PRICE_CONSTRAINT_NAME = "check_unit_price_status";
    private const string CHECK_QUANTITY_CONSTRAINT_NAME = "check_quantity";
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "Order_item";
    private const string ID_COLUMN_NAME = "id";
    private const string TITLE_COLUMN_NAME = "title";
    private const string QUANTITY_COLUMN_NAME = "quantity";
    private const string PRODUCT_ID_COLUMN_NAME = "product_id";
    private const string ORDER_ID_COLUMN_NAME = "order_id";

    private const string UNIT_PRICE_COLUMN_NAME = "unit_price";
    private const string CURRENCY_CODE_COLUMN_NAME = "currency_code"; 
    #endregion

    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(TABLE_NAME,
               t =>
               {
                   t.HasCheckConstraint(CHECK_QUANTITY_CONSTRAINT_NAME, $"\"{QUANTITY_COLUMN_NAME}\" >= 1");
                   t.HasCheckConstraint(CHECK_UNIT_PRICE_CONSTRAINT_NAME, $"\"{UNIT_PRICE_COLUMN_NAME}\" > 0");
               });

        #region id
        // Shadow property
        builder.Property<Guid>("Id")
            .HasColumnName(ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasKey("Id").HasName(PK_CONSTRAINT_NAME);
        #endregion

        #region title
        builder.Property(x => x.Title)
            .HasColumnName(TITLE_COLUMN_NAME)
            .HasPostgresVarchar(255)
            .IsRequired();
        #endregion

        #region quantity
        builder.Property(x => x.Quantity)
            .HasColumnName(QUANTITY_COLUMN_NAME)
            .HasColumnType(SmallInt)
            .IsRequired();
        #endregion

        #region order_id
        // Shadow property
        builder.Property<OrderId>("OrderId")
             .HasColumnName(ORDER_ID_COLUMN_NAME)
             .HasConversion(id => id.Value, value => OrderId.From(value))
             .IsRequired();
        #endregion

        #region UnitPrice VO
        builder.ComplexProperty(x => x.UnitPrice, price =>
        {
            price.Property(p => p.Sum)
                .HasColumnName(UNIT_PRICE_COLUMN_NAME)
                .HasColumnType(Numeric(15,2))
                .IsRequired();

            price.Property(p => p.Currency)
                .HasColumnName(CURRENCY_CODE_COLUMN_NAME)
                .HasPostgresVarchar(3)
                .HasConversion(c => c.Code, code => Currency.FromCode(code).Value)
                .IsRequired();
        });
        #endregion

        #region product_id
        //TODO FK when Product implemented !!!
        builder.Property(x => x.ProductId)
            .HasColumnName(PRODUCT_ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .IsRequired(); 
        #endregion
    }
}