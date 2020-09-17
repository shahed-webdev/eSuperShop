using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class VendorTableErrorSloved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StoreName",
                table: "Vendor",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(255)",
                oldFixedLength: true,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "StoreAddress",
                table: "Vendor",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(500)",
                oldFixedLength: true,
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Vendor",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(128)",
                oldFixedLength: true,
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StoreName",
                table: "Vendor",
                type: "nchar(255)",
                fixedLength: true,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "StoreAddress",
                table: "Vendor",
                type: "nchar(500)",
                fixedLength: true,
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Vendor",
                type: "nchar(128)",
                fixedLength: true,
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }
    }
}
