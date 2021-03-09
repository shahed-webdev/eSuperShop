using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ChangeUrlToFileNameInVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedStoreBannerUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ChangedStoreLogoUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ChequeImageUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "NIdImageBackUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "NIdImageFrontUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "StoreBannerUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "StoreLogoUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "TradeLicenseImageUrl",
                table: "Vendor");

            migrationBuilder.AddColumn<string>(
                name: "ChangedStoreBannerFileName",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangedStoreLogoFileName",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChequeImageFileName",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIdImageBackFileName",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIdImageFrontFileName",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreBannerFileName",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreLogoFileName",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeLicenseImageFileName",
                table: "Vendor",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedStoreBannerFileName",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ChangedStoreLogoFileName",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ChequeImageFileName",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "NIdImageBackFileName",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "NIdImageFrontFileName",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "StoreBannerFileName",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "StoreLogoFileName",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "TradeLicenseImageFileName",
                table: "Vendor");

            migrationBuilder.AddColumn<string>(
                name: "ChangedStoreBannerUrl",
                table: "Vendor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangedStoreLogoUrl",
                table: "Vendor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChequeImageUrl",
                table: "Vendor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIdImageBackUrl",
                table: "Vendor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIdImageFrontUrl",
                table: "Vendor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreBannerUrl",
                table: "Vendor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreLogoUrl",
                table: "Vendor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeLicenseImageUrl",
                table: "Vendor",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
