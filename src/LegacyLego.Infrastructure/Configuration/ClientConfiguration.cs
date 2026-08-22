using LegacyLego.Domain.Aggregates;
using LegacyLego.Domain.ValueObjects;
using LegacyLego.Infrastructure.Configuration.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using static LegacyLego.Infrastructure.Configuration.Common.PostgresTypes;

namespace LegacyLego.Infrastructure.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    #region CostraintNames
    private const string PK_CONSTRAINT_NAME = "pk_client";
    #endregion

    #region ColumnNames
    private const string TABLE_NAME = "client";
    private const string ID_COLUMN_NAME = "id";
    private const string USERNAME_COLUMN_NAME = "username";
    private const string FIRST_NAME_COLUMN_NAME = "first_name";
    private const string LAST_NAME_COLUMN_NAME = "last_name";
    private const string PHONE_NUMBER_COLUMN_NAME = "phone_number";
    private const string PREFERENCES_COLUMN_NAME = "preferences";
    private const string EMAIL_COLUMN_NAME = "email";
    private const string CREATED_AT_UTC_COLUMN_NAME = "created_at_utc";

    #endregion

    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable(TABLE_NAME);

        #region id
        builder.HasKey(o => o.Id).HasName(PK_CONSTRAINT_NAME);

        builder.Property(o => o.Id)
            .ValueGeneratedNever()
            .HasColumnType(Uuid)
            .HasConversion(id => id.Value, value => ClientId.From(value).Value)
            .HasColumnName(ID_COLUMN_NAME);
        #endregion

        #region created_at_utc
        builder.Property(x => x.CreatedAtUtc)
            .HasColumnType(TimeStampTz)
            .IsRequired()
            .HasColumnName(CREATED_AT_UTC_COLUMN_NAME);
        #endregion

        #region username
        builder.HasIndex(x => x.Username)
            .IsUnique();
        builder.Property(x => x.Username)
            .HasPostgresVarchar(100)
            .IsRequired()
            .HasColumnName(USERNAME_COLUMN_NAME);
        #endregion

        #region first_name
        builder.Property(x => x.FirstName)
            .HasPostgresVarchar(100)
            .IsRequired(false)
            .HasColumnName(FIRST_NAME_COLUMN_NAME);
        #endregion

        #region last_name
        builder.Property(x => x.LastName)
            .HasPostgresVarchar(100)
            .IsRequired(false)
            .HasColumnName(LAST_NAME_COLUMN_NAME);
        #endregion

        #region Email VO
        builder.HasIndex(x => x.Email)
           .IsUnique();
        builder.Property(o => o.Email)
            .HasPostgresVarchar(256)
            .HasConversion(e => e.Value, value => Email.Create(value).Value)
            .IsRequired()
            .HasColumnName(EMAIL_COLUMN_NAME);
        #endregion

        #region PhoneNumber VO
        builder.Property(o => o.PhoneNumber)
            .HasPostgresVarchar(20)
            .IsRequired(false)
            .HasColumnName(PHONE_NUMBER_COLUMN_NAME)
            .HasConversion(
                phone => phone != null ? phone.Value : null,
                value => !string.IsNullOrWhiteSpace(value) ? PhoneNumber.Create(value).Value : null);
        #endregion

        #region Preferences VO

        builder.OwnsOne(c => c.Preferences, prefBuilder =>
        {
            // Весь Owned тип в одну JSON-колонку
            prefBuilder.ToJson(PREFERENCES_COLUMN_NAME);

            // Задаётся camelCase для ключей внутри JSON-документа
            prefBuilder.Property(p => p.LanguageCode)
                .HasJsonPropertyName("languageCode")
                .IsRequired();

            prefBuilder.Property(p => p.CurrencyCode)
                .HasJsonPropertyName("currencyCode")
                .IsRequired();
        });

        #endregion

    }
}