using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderPaymentActualAndExpectedPriceAmounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "actual_amount",
                table: "Order_payment",
                type: "numeric(15,2)",
                precision: 15,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "actual_currency",
                table: "Order_payment",
                type: "varchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "expected_amount",
                table: "Order_payment",
                type: "numeric(15,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "expected_currency",
                table: "Order_payment",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "actual_amount",
                table: "Order_payment");

            migrationBuilder.DropColumn(
                name: "actual_currency",
                table: "Order_payment");

            migrationBuilder.DropColumn(
                name: "expected_amount",
                table: "Order_payment");

            migrationBuilder.DropColumn(
                name: "expected_currency",
                table: "Order_payment");
        }
    }
}
