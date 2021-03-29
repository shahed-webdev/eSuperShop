using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddShippingChargeInCatalogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AdditionalFeePercentageInDhaka",
                table: "Catalog",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AdditionalFeePercentageOutDhaka",
                table: "Catalog",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BasicChargeInDhaka",
                table: "Catalog",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BasicChargeOutDhaka",
                table: "Catalog",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "BasicMaxQuantityInDhaka",
                table: "Catalog",
                nullable: false,
                defaultValueSql: "3");

            migrationBuilder.AddColumn<int>(
                name: "BasicMaxQuantityOutDhaka",
                table: "Catalog",
                nullable: false,
                defaultValueSql: "3");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryWithin",
                table: "Catalog",
                nullable: false,
                defaultValueSql: "5");

            migrationBuilder.AddColumn<int>(
                name: "ReturnWithin",
                table: "Catalog",
                nullable: false,
                defaultValueSql: "5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalFeePercentageInDhaka",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "AdditionalFeePercentageOutDhaka",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "BasicChargeInDhaka",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "BasicChargeOutDhaka",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "BasicMaxQuantityInDhaka",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "BasicMaxQuantityOutDhaka",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "DeliveryWithin",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "ReturnWithin",
                table: "Catalog");
        }
    }
}
