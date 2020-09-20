using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddVendorProductCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoreLogoUrl",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreTagLine",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VendorProductCategory",
                columns: table => new
                {
                    VendorProductCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorProductCategory", x => x.VendorProductCategoryId);
                    table.ForeignKey(
                        name: "FK_VendorProductCategory_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorProductCategoryList",
                columns: table => new
                {
                    VendorProductCategoryListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorProductCategoryId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorProductCategoryList", x => x.VendorProductCategoryListId);
                    table.ForeignKey(
                        name: "FK_VendorProductCategoryList_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_VendorProductCategoryList_VendorProductCategory",
                        column: x => x.VendorProductCategoryId,
                        principalTable: "VendorProductCategory",
                        principalColumn: "VendorProductCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorProductCategory_VendorId",
                table: "VendorProductCategory",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorProductCategoryList_ProductId",
                table: "VendorProductCategoryList",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorProductCategoryList_VendorProductCategoryId",
                table: "VendorProductCategoryList",
                column: "VendorProductCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorProductCategoryList");

            migrationBuilder.DropTable(
                name: "VendorProductCategory");

            migrationBuilder.DropColumn(
                name: "StoreLogoUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "StoreTagLine",
                table: "Vendor");
        }
    }
}
