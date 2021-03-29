using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddAreaInOrderShippingAddressTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "OrderShippingAddress",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderShippingAddress_AreaId",
                table: "OrderShippingAddress",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderShippingAddress_Area",
                table: "OrderShippingAddress",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderShippingAddress_Area",
                table: "OrderShippingAddress");

            migrationBuilder.DropIndex(
                name: "IX_OrderShippingAddress_AreaId",
                table: "OrderShippingAddress");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "OrderShippingAddress");
        }
    }
}
