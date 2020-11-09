using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddCustomerAddressTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderShippingAddress",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderShippingAddress_Customer",
                table: "OrderShippingAddress");

            migrationBuilder.DropIndex(
                name: "IX_OrderShippingAddress_CustomerId",
                table: "OrderShippingAddress");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderShippingAddressId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "OrderShippingAddress");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "OrderShippingAddress");

            migrationBuilder.DropColumn(
                name: "OrderShippingAddressId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "AlternativePhone",
                table: "OrderShippingAddress",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderShippingAddress",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CustomerAddressBook",
                columns: table => new
                {
                    CustomerAddressBookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    AlternativePhone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 2000, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddressBook", x => x.CustomerAddressBookId);
                    table.ForeignKey(
                        name: "FK_CustomerAddressBook_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderShippingAddress_OrderId",
                table: "OrderShippingAddress",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddressBook_CustomerId",
                table: "CustomerAddressBook",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderShippingAddress_Order",
                table: "OrderShippingAddress",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderShippingAddress_Order",
                table: "OrderShippingAddress");

            migrationBuilder.DropTable(
                name: "CustomerAddressBook");

            migrationBuilder.DropIndex(
                name: "IX_OrderShippingAddress_OrderId",
                table: "OrderShippingAddress");

            migrationBuilder.DropColumn(
                name: "AlternativePhone",
                table: "OrderShippingAddress");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderShippingAddress");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "OrderShippingAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "OrderShippingAddress",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrderShippingAddressId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderShippingAddress_CustomerId",
                table: "OrderShippingAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderShippingAddressId",
                table: "Order",
                column: "OrderShippingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderShippingAddress",
                table: "Order",
                column: "OrderShippingAddressId",
                principalTable: "OrderShippingAddress",
                principalColumn: "OrderShippingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderShippingAddress_Customer",
                table: "OrderShippingAddress",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
