using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ProductAttributeDesginChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttributeSetList");

            migrationBuilder.DropTable(
                name: "ProductAttributeSet");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ProductAttribute");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ProductAttribute");

            migrationBuilder.CreateTable(
                name: "ProductAttributeValue",
                columns: table => new
                {
                    ProductAttributeValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAttributeId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: true),
                    InsertedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValue", x => x.ProductAttributeValueId);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValue_ProductAttribute",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "ProductAttributeId");
                });

            migrationBuilder.CreateTable(
                name: "ProductQuantitySet",
                columns: table => new
                {
                    ProductQuantitySetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    PriceAdjustment = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    InsertedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuantitySet", x => x.ProductQuantitySetId);
                    table.ForeignKey(
                        name: "FK_ProductQuantitySet_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductQuantitySetAttribute",
                columns: table => new
                {
                    ProductQuantitySetAttributeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductQuantitySetId = table.Column<int>(nullable: false),
                    ProductAttributeValueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuantitySetAttribute", x => x.ProductQuantitySetAttributeId);
                    table.ForeignKey(
                        name: "FK_ProductQuantitySetAttribute_ProductAttributeValue",
                        column: x => x.ProductAttributeValueId,
                        principalTable: "ProductAttributeValue",
                        principalColumn: "ProductAttributeValueId");
                    table.ForeignKey(
                        name: "FK_ProductQuantitySetAttribute_ProductQuantitySet",
                        column: x => x.ProductQuantitySetId,
                        principalTable: "ProductQuantitySet",
                        principalColumn: "ProductQuantitySetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_ProductAttributeId",
                table: "ProductAttributeValue",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantitySet_ProductId",
                table: "ProductQuantitySet",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantitySetAttribute_ProductAttributeValueId",
                table: "ProductQuantitySetAttribute",
                column: "ProductAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantitySetAttribute_ProductQuantitySetId",
                table: "ProductQuantitySetAttribute",
                column: "ProductQuantitySetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductQuantitySetAttribute");

            migrationBuilder.DropTable(
                name: "ProductAttributeValue");

            migrationBuilder.DropTable(
                name: "ProductQuantitySet");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ProductAttribute",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "ProductAttribute",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProductAttributeSet",
                columns: table => new
                {
                    ProductAttributeSetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    PriceAdjustment = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeSet", x => x.ProductAttributeSetId);
                    table.ForeignKey(
                        name: "FK_ProductAttributeSet_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeSetList",
                columns: table => new
                {
                    ProductAttributeSetListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: false),
                    ProductAttributeSetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeSetList", x => x.ProductAttributeSetListId);
                    table.ForeignKey(
                        name: "FK_ProductAttributeSetList_ProductAttribute",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "ProductAttributeId");
                    table.ForeignKey(
                        name: "FK_ProductAttributeSetList_ProductAttributeSet",
                        column: x => x.ProductAttributeSetId,
                        principalTable: "ProductAttributeSet",
                        principalColumn: "ProductAttributeSetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeSet_ProductId",
                table: "ProductAttributeSet",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeSetList_ProductAttributeId",
                table: "ProductAttributeSetList",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeSetList_ProductAttributeSetId",
                table: "ProductAttributeSetList",
                column: "ProductAttributeSetId");
        }
    }
}
