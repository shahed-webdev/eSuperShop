using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddProductFaq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductFaq",
                columns: table => new
                {
                    ProductFaqId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Question = table.Column<string>(maxLength: 1024, nullable: false),
                    Answer = table.Column<string>(maxLength: 2048, nullable: true),
                    IsVisible = table.Column<bool>(nullable: false),
                    QuestionOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    AnswerOnUtc = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFaq", x => x.ProductFaqId);
                    table.ForeignKey(
                        name: "FK_ProductFaq_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFaq_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFaq_CustomerId",
                table: "ProductFaq",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFaq_ProductId",
                table: "ProductFaq",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFaq");
        }
    }
}
