using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class AddColumnVendorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountNumber",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountTitle",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChequeImageUrl",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileBankingNumber",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileBankingType",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NId",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIdImageBackUrl",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIdImageFrontUrl",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnAddress",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReturnAreaId",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnPhone",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnPostcode",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoutingNumber",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreAreaId",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorePostcode",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeLicenseImageUrl",
                table: "Vendor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseAddress",
                table: "Vendor",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseAreaId",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehousePhone",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehousePostcode",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VendorCertificate",
                columns: table => new
                {
                    VendorCertificateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    CertificateImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCertificate", x => x.VendorCertificateId);
                    table.ForeignKey(
                        name: "FK_VendorCertificate_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_ReturnAreaId",
                table: "Vendor",
                column: "ReturnAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_StoreAreaId",
                table: "Vendor",
                column: "StoreAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_WarehouseAreaId",
                table: "Vendor",
                column: "WarehouseAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCertificate_VendorId",
                table: "VendorCertificate",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Area_VendorReturnArea",
                table: "Vendor",
                column: "ReturnAreaId",
                principalTable: "Area",
                principalColumn: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Area_VendorStore",
                table: "Vendor",
                column: "StoreAreaId",
                principalTable: "Area",
                principalColumn: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Area_VendorWarehouse",
                table: "Vendor",
                column: "WarehouseAreaId",
                principalTable: "Area",
                principalColumn: "AreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Area_VendorReturnArea",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_Area_VendorStore",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_Area_VendorWarehouse",
                table: "Vendor");

            migrationBuilder.DropTable(
                name: "VendorCertificate");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_ReturnAreaId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_StoreAreaId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_WarehouseAreaId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "BankAccountTitle",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ChequeImageUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "MobileBankingNumber",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "MobileBankingType",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "NId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "NIdImageBackUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "NIdImageFrontUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ReturnAddress",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ReturnAreaId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ReturnPhone",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ReturnPostcode",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "RoutingNumber",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "StoreAreaId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "StorePostcode",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "TradeLicenseImageUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "WarehouseAddress",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "WarehouseAreaId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "WarehousePhone",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "WarehousePostcode",
                table: "Vendor");
        }
    }
}
