using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddAreaInCustomerAddressBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "CustomerAddressBook",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddressBook_AreaId",
                table: "CustomerAddressBook",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddressBook_Area",
                table: "CustomerAddressBook",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddressBook_Area",
                table: "CustomerAddressBook");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddressBook_AreaId",
                table: "CustomerAddressBook");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "CustomerAddressBook");
        }
    }
}
