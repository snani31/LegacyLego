using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClientAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    created_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    preferences = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_client_id",
                table: "Order",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_email",
                table: "client",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_username",
                table: "client",
                column: "username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_order_client",
                table: "Order",
                column: "client_id",
                principalTable: "client",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_client",
                table: "Order");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropIndex(
                name: "IX_Order_client_id",
                table: "Order");
        }
    }
}
