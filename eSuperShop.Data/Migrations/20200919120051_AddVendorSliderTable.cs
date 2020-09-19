using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddVendorSliderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorStoreSlider",
                columns: table => new
                {
                    VendorStoreSliderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: false),
                    RedirectUrl = table.Column<string>(maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorStoreSlider", x => x.VendorStoreSliderId);
                    table.ForeignKey(
                        name: "FK_Vendor_VendorStoreSlider",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorStoreSlider_VendorId",
                table: "VendorStoreSlider",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorStoreSlider");
        }
    }
}
