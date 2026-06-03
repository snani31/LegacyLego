using LegacyLego.Infrastructure.Configuration.Common;
using LegacyLego.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    #region CostraintNames
    private const string PK_CONSTRAINT_NAME = "pk_outbox_messages";
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "Outbox_messages";
    private const string ID_COLUMN_NAME = "id";
    private const string TYPE_COLUMN_NAME = "type";
    private const string CONTENT_COLUMN_NAME = "content";
    private const string OCCURRED_ON_UTC_COLUMN_NAME = "occurred_on_utc";

    private const string PROCESSED_ON_UTC_COLUMN_NAME = "processed_on_utc";
    private const string ERROR_COLUMN_NAME = "error";
    #endregion

    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TABLE_NAME);

        #region id
        builder.HasKey(x => x.Id)
            .HasName(PK_CONSTRAINT_NAME);

        builder.Property(x => x.Id)
            .HasColumnName(ID_COLUMN_NAME)
            .HasColumnType(Uuid)
            .IsRequired(); 
        #endregion

        #region type
        builder.Property(x => x.Type)
            .HasColumnName(TYPE_COLUMN_NAME)
            .HasPostgresVarchar(255)
            .IsRequired(); 
        #endregion

        #region content
        builder.Property(x => x.Content)
            .HasColumnName(CONTENT_COLUMN_NAME)
            .HasColumnType(Text)
            .IsRequired(); 
        #endregion

        #region occurred_on_utc
        builder.Property(x => x.OccurredOnUtc)
            .HasColumnName(OCCURRED_ON_UTC_COLUMN_NAME)
            .HasColumnType(TimeStampTz)
            .IsRequired(); 
        #endregion

        #region processed_on_utc
        builder.Property(x => x.ProcessedOnUtc)
            .HasColumnName(PROCESSED_ON_UTC_COLUMN_NAME)
            .HasColumnType(TimeStampTz)
            .IsRequired(false); 
        #endregion

        #region error
        builder.Property(x => x.Error)
            .HasColumnName(ERROR_COLUMN_NAME)
            .HasColumnType(Text)
            .IsRequired(false); 
        #endregion
    }
}