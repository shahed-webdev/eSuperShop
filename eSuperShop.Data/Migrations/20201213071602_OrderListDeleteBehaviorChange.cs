using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class OrderListDeleteBehaviorChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderList_Order",
                table: "OrderList");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderList_Order",
                table: "OrderList",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderList_Order",
                table: "OrderList");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderList_Order",
                table: "OrderList",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
