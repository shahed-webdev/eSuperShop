using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ErrorOfProductQuantitySetOrderCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductQuantitySet_OrderCart",
                table: "OrderCart");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCart_ProductQuantitySetId",
                table: "OrderCart",
                column: "ProductQuantitySetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductQuantitySet_OrderCart",
                table: "OrderCart",
                column: "ProductQuantitySetId",
                principalTable: "ProductQuantitySet",
                principalColumn: "ProductQuantitySetId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductQuantitySet_OrderCart",
                table: "OrderCart");

            migrationBuilder.DropIndex(
                name: "IX_OrderCart_ProductQuantitySetId",
                table: "OrderCart");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductQuantitySet_OrderCart",
                table: "OrderCart",
                column: "ProductId",
                principalTable: "ProductQuantitySet",
                principalColumn: "ProductQuantitySetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
