using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LegacyLego.Infrastructure.Configuration.Common;

public static class EntityTypeBuilderExtensions
{
    public static CheckConstraintBuilder HasEnumCheckConstraint<TEnum>(
        this TableBuilder tableBuilder,
        string constraintName,
        string columnName) where TEnum : struct, Enum
    {
        var sb = new StringBuilder($"\"{columnName}\" IN (");

        foreach (var status in Enum.GetValues<TEnum>())
        {
            sb.Append($"'{status}', ");
        }

        sb.Remove(sb.Length - 2, 2);
        sb.Append(")");

        return tableBuilder.HasCheckConstraint(constraintName, sb.ToString());
    }
}