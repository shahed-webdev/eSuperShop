using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ChangeUrlToFileNameInVendorProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "VendorProductCategory");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "VendorProductCategory",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "VendorProductCategory");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "VendorProductCategory",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
