using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ChangeImageUrlToImageFileNameInCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Catalog");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Catalog",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Catalog");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Catalog",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
