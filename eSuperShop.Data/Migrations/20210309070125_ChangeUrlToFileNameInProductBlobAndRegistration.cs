using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ChangeUrlToFileNameInProductBlobAndRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "BlobUrl",
                table: "ProductBlob");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Registration",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlobFileName",
                table: "ProductBlob",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "BlobFileName",
                table: "ProductBlob");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Registration",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlobUrl",
                table: "ProductBlob",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
