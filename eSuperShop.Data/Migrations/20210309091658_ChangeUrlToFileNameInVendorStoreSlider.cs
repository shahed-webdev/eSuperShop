using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ChangeUrlToFileNameInVendorStoreSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "VendorStoreSlider");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "VendorStoreSlider",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "VendorStoreSlider");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "VendorStoreSlider",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
