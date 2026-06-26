using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegacyLego.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency_code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    frozen_total_sum = table.Column<decimal>(type: "numeric(15,2)", nullable: true),
                    address_city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    address_country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    address_postal_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    address_street = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.id);
                    table.CheckConstraint("check_order_frozen_total_sun", "\"frozen_total_sum\" >= 0");
                    table.CheckConstraint("check_order_status", "\"status\" IN ('PendingPayment', 'Paid', 'Cancelled', 'Expired', 'Refunded')");
                });

            migrationBuilder.CreateTable(
                name: "Outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Order_item",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    quantity = table.Column<short>(type: "smallint", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency_code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item", x => x.order_id);
                    table.CheckConstraint("check_quantity", "\"quantity\" >= 1");
                    table.CheckConstraint("check_unit_price_status", "\"unit_price\" > 0");
                    table.ForeignKey(
                        name: "fk_order_order_items",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order_payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    transaction_id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_payment", x => x.id);
                    table.CheckConstraint("check_order_payment_status", "\"status\" IN ('Pending', 'Succeeded', 'Failed', 'Refunded', 'RefundRequested')");
                    table.ForeignKey(
                        name: "fk_order_payment_orders",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "External_session",
                columns: table => new
                {
                    order_payment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    checkout_url = table.Column<string>(type: "text", nullable: false),
                    expires_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_external_session", x => x.order_payment_id);
                    table.ForeignKey(
                        name: "FK_External_session_Order_payment_order_payment_id",
                        column: x => x.order_payment_id,
                        principalTable: "Order_payment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_payment_order_id",
                table: "Order_payment",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_payment_transaction_id",
                table: "Order_payment",
                column: "transaction_id",
                unique: true,
                filter: "transaction_id IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "External_session");

            migrationBuilder.DropTable(
                name: "Order_item");

            migrationBuilder.DropTable(
                name: "Outbox_messages");

            migrationBuilder.DropTable(
                name: "Order_payment");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
