using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddOrderTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderShippingAddress",
                columns: table => new
                {
                    OrderShippingAddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 2000, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShippingAddress", x => x.OrderShippingAddressId);
                    table.ForeignKey(
                        name: "FK_OrderShippingAddress_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    OrderSN = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<string>(maxLength: 50, nullable: true),
                    OrderShippingAddressId = table.Column<int>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    ConfirmedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeliveredOnUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    IsDelivered = table.Column<bool>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    NetAmount = table.Column<decimal>(nullable: false, computedColumnSql: "(([TotalAmount]-[Discount])+[ShippingCost])")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_OrderShippingAddress",
                        column: x => x.OrderShippingAddressId,
                        principalTable: "OrderShippingAddress",
                        principalColumn: "OrderShippingAddressId");
                });

            migrationBuilder.CreateTable(
                name: "OrderList",
                columns: table => new
                {
                    OrderListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductQuantitySetId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false, computedColumnSql: "([Quantity] * [UnitPrice])"),
                    CommissionPercentage = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CommissionAmount = table.Column<decimal>(nullable: false, computedColumnSql: " ((([Quantity] * [UnitPrice])*[CommissionPercentage])/100)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderList", x => x.OrderListId);
                    table.ForeignKey(
                        name: "FK_OrderList_Order",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderList_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_OrderList_ProductQuantitySet",
                        column: x => x.ProductQuantitySetId,
                        principalTable: "ProductQuantitySet",
                        principalColumn: "ProductQuantitySetId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderShippingAddressId",
                table: "Order",
                column: "OrderShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderList_OrderId",
                table: "OrderList",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderList_ProductId",
                table: "OrderList",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderList_ProductQuantitySetId",
                table: "OrderList",
                column: "ProductQuantitySetId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShippingAddress_CustomerId",
                table: "OrderShippingAddress",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderList");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderShippingAddress");
        }
    }
}
