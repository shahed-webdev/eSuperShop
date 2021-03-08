using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddVendorChangedApprovedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangedStoreBannerUrl",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangedStoreLogoUrl",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangedStoreTagLine",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChangedApproved",
                table: "Vendor",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedStoreBannerUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ChangedStoreLogoUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ChangedStoreTagLine",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "IsChangedApproved",
                table: "Vendor");
        }
    }
}
