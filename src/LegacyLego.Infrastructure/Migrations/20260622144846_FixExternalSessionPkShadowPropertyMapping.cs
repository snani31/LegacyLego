using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixExternalSessionPkShadowPropertyMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item",
                table: "Order_item");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "Order_item",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item",
                table: "Order_item",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_item_order_id",
                table: "Order_item",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item",
                table: "Order_item");

            migrationBuilder.DropIndex(
                name: "IX_Order_item_order_id",
                table: "Order_item");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Order_item");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item",
                table: "Order_item",
                column: "order_id");
        }
    }
}
