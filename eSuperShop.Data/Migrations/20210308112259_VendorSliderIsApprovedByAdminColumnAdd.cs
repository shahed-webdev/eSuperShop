using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class VendorSliderIsApprovedByAdminColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedByAdmin",
                table: "VendorStoreSlider",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedByAdmin",
                table: "VendorProductCategoryList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedByAdmin",
                table: "VendorProductCategory",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprovedByAdmin",
                table: "VendorStoreSlider");

            migrationBuilder.DropColumn(
                name: "IsApprovedByAdmin",
                table: "VendorProductCategoryList");

            migrationBuilder.DropColumn(
                name: "IsApprovedByAdmin",
                table: "VendorProductCategory");
        }
    }
}
