using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class ChangeCertificateUrlToCertificateFileNameInVendorCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateImageUrl",
                table: "VendorCertificate");

            migrationBuilder.AddColumn<string>(
                name: "CertificateImageFileName",
                table: "VendorCertificate",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateImageFileName",
                table: "VendorCertificate");

            migrationBuilder.AddColumn<string>(
                name: "CertificateImageUrl",
                table: "VendorCertificate",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
