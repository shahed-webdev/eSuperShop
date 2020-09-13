using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class VendorCatalogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorCatalog",
                columns: table => new
                {
                    VendorCatalogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    CatalogId = table.Column<int>(nullable: false),
                    CommissionPercentage = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    AssignedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    AssignedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCatalog", x => x.VendorCatalogId);
                    table.ForeignKey(
                        name: "FK_VendorCatalog_Registration",
                        column: x => x.AssignedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorCatalog_Catalog",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorCatalog_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorCatalog_AssignedByRegistrationId",
                table: "VendorCatalog",
                column: "AssignedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCatalog_CatalogId",
                table: "VendorCatalog",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCatalog_VendorId",
                table: "VendorCatalog",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorCatalog");
        }
    }
}
