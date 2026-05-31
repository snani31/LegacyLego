using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegacyLego.Infrastructure.Configuration.Common;

internal static class PropertyBuilderExtensions
{
    internal static PropertyBuilder<T> HasPostgresVarchar<T>(this PropertyBuilder<T> builder, int length, bool allowStringConversion = false)
    {
        if (allowStringConversion)
            builder.HasConversion<string>();

        return builder
            .HasColumnType($"varchar({length})")
            .HasMaxLength(length);
    }

    internal static ComplexTypePropertyBuilder<T> HasPostgresVarchar<T>(this ComplexTypePropertyBuilder<T> builder, int length, bool allowStringConversion = false)
    {
        if (allowStringConversion)
            builder.HasConversion<string>();

        return builder
            .HasColumnType($"varchar({length})")
            .HasMaxLength(length);
    }
}