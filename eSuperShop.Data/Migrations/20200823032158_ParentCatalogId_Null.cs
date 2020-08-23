using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ParentCatalogId_Null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentCatalogId",
                table: "Catalog",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentCatalogId",
                table: "Catalog",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
