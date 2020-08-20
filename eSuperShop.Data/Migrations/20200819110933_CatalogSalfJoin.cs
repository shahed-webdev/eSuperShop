using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class CatalogSalfJoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Catalog_ParentCatalogId",
                table: "Catalog",
                column: "ParentCatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_Catalog",
                table: "Catalog",
                column: "ParentCatalogId",
                principalTable: "Catalog",
                principalColumn: "CatalogId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_Catalog",
                table: "Catalog");

            migrationBuilder.DropIndex(
                name: "IX_Catalog_ParentCatalogId",
                table: "Catalog");
        }
    }
}
