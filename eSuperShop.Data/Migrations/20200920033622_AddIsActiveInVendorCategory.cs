using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddIsActiveInVendorCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "VendorProductCategory",
                nullable: false,
                defaultValueSql: "((1))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "VendorProductCategory");
        }
    }
}
