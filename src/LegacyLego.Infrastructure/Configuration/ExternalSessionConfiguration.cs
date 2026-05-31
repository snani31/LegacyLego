using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public class ExternalSessionConfiguration : IEntityTypeConfiguration<ExternalSession>
{
    #region CostraintNames
    private const string PK_CONSTRAINT_NAME = "pk_external_session";
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "External_session";
    private const string ID_COLUMN_NAME = "order_payment_id";
    private const string EXTERNAL_ID_COLUMN_NAME = "external_id";
    private const string CHECKOUT_URL_COLUMN_NAME = "checkout_url";
    private const string EXPIRES_AT_UTC_COLUMN_NAME = "expires_at_utc";
    #endregion

    public void Configure(EntityTypeBuilder<ExternalSession> builder)
    {
        builder.ToTable(TABLE_NAME);

        #region order_payment_id
        builder.Property<Guid>(ID_COLUMN_NAME)
            .HasColumnName(ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .IsRequired();

        builder.HasKey(ID_COLUMN_NAME)
            .HasName(PK_CONSTRAINT_NAME); 
        #endregion

        #region external_id
        builder.Property(x => x.ExternalId)
            .HasColumnName(EXTERNAL_ID_COLUMN_NAME)
            .HasPostgresVarchar(255)
            .IsRequired(); 
        #endregion

        #region checkout_url
        builder.Property(x => x.CheckoutUrl)
            .HasColumnName(CHECKOUT_URL_COLUMN_NAME)
            .HasColumnType(Text)
            .IsRequired(); 
        #endregion

        #region expires_at_utc
        builder.Property(x => x.ExpiresAtUtc)
            .HasColumnName(EXPIRES_AT_UTC_COLUMN_NAME)
            .HasColumnType(TimeStampTz)
            .IsRequired();
        #endregion
    }
}