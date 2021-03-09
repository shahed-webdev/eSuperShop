using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ChangeLogoUrlToLogoFileNameInBrandTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "AllBrand");

            migrationBuilder.AddColumn<string>(
                name: "LogoFileName",
                table: "AllBrand",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoFileName",
                table: "AllBrand");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "AllBrand",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);
        }
    }
}
