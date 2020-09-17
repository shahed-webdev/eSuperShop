using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddEmailVendorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StoreName",
                table: "Vendor",
                fixedLength: true,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(128)",
                oldFixedLength: true,
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "StoreAddress",
                table: "Vendor",
                fixedLength: true,
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(255)",
                oldFixedLength: true,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vendor",
                fixedLength: true,
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vendor");

            migrationBuilder.AlterColumn<string>(
                name: "StoreName",
                table: "Vendor",
                type: "nchar(128)",
                fixedLength: true,
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "StoreAddress",
                table: "Vendor",
                type: "nchar(255)",
                fixedLength: true,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
