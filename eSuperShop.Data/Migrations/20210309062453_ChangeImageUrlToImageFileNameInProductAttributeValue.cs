using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ChangeImageUrlToImageFileNameInProductAttributeValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ProductAttributeValue");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "ProductAttributeValue",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "ProductAttributeValue");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ProductAttributeValue",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
