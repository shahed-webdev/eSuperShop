using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddStoreSlug_Banar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoreBanarUrl",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreSlugUrl",
                table: "Vendor",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreBanarUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "StoreSlugUrl",
                table: "Vendor");
        }
    }
}
