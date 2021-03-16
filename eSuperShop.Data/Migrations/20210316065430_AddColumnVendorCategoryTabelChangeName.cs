using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddColumnVendorCategoryTabelChangeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangedImageFileName",
                table: "VendorProductCategory",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangedName",
                table: "VendorProductCategory",
                maxLength: 400,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedImageFileName",
                table: "VendorProductCategory");

            migrationBuilder.DropColumn(
                name: "ChangedName",
                table: "VendorProductCategory");
        }
    }
}
