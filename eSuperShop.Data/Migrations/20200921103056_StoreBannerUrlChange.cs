using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class StoreBannerUrlChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreBanarUrl",
                table: "Vendor");

            migrationBuilder.AddColumn<string>(
                name: "StoreBannerUrl",
                table: "Vendor",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreBannerUrl",
                table: "Vendor");

            migrationBuilder.AddColumn<string>(
                name: "StoreBanarUrl",
                table: "Vendor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
