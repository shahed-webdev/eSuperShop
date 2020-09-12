using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSuperShop.Data.Migrations
{
    public partial class InitialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Validation = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    DateofBirth = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.RegistrationId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllAttribute",
                columns: table => new
                {
                    AttributeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyName = table.Column<string>(maxLength: 255, nullable: false),
                    AllowFiltering = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllAttribute", x => x.AttributeId);
                    table.ForeignKey(
                        name: "FK_AllAttribute_Registration",
                        column: x => x.CreatedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllBrand",
                columns: table => new
                {
                    BrandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    LogoUrl = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllBrand", x => x.BrandId);
                    table.ForeignKey(
                        name: "FK_AllBrand_Registration",
                        column: x => x.CreatedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllSpecification",
                columns: table => new
                {
                    SpecificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyName = table.Column<string>(maxLength: 100, nullable: false),
                    AllowFiltering = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllSpecification", x => x.SpecificationId);
                    table.ForeignKey(
                        name: "FK_AllSpecification_Registration",
                        column: x => x.CreatedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customer_Registration",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SEO",
                columns: table => new
                {
                    SeoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetaKeywords = table.Column<string>(maxLength: 400, nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 4000, nullable: true),
                    MetaTitle = table.Column<string>(maxLength: 400, nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEO", x => x.SeoId);
                    table.ForeignKey(
                        name: "FK_SEO_Registration",
                        column: x => x.CreatedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    SliderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: false),
                    RedirectUrl = table.Column<string>(maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: true),
                    DisplayPlace = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.SliderId);
                    table.ForeignKey(
                        name: "FK_Slider_Registration",
                        column: x => x.CreatedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(nullable: true),
                    AuthorizedPerson = table.Column<string>(maxLength: 128, nullable: false),
                    VerifiedPhone = table.Column<string>(maxLength: 50, nullable: false),
                    StoreName = table.Column<string>(fixedLength: true, maxLength: 128, nullable: false),
                    StoreAddress = table.Column<string>(fixedLength: true, maxLength: 255, nullable: true),
                    StoreTheme = table.Column<string>(maxLength: 50, nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    ApprovedByRegistrationId = table.Column<int>(nullable: true),
                    ApprovedOnUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    GrossSale = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Refund = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    NetSale = table.Column<decimal>(type: "decimal(20, 2)", nullable: false, computedColumnSql: "([GrossSale]-([Discount]+[Refund]))"),
                    Commission = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Withdraw = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(22, 2)", nullable: false, computedColumnSql: "([GrossSale]-((([Discount]+[Refund])+[Commission])+[Withdraw]))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_Vendor_Registration",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Location = table.Column<string>(maxLength: 255, nullable: true),
                    Details = table.Column<string>(maxLength: 500, nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WarehouseId);
                    table.ForeignKey(
                        name: "FK_Warehouse_Registration",
                        column: x => x.CreatedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    CatalogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogName = table.Column<string>(maxLength: 400, nullable: false),
                    SlugUrl = table.Column<string>(maxLength: 128, nullable: false),
                    ParentCatalogId = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    DisplayOrder = table.Column<int>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    SeoId = table.Column<int>(nullable: true),
                    CreatedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.CatalogId);
                    table.ForeignKey(
                        name: "FK_Catalog_Registration",
                        column: x => x.CreatedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Catalog_Catalog",
                        column: x => x.ParentCatalogId,
                        principalTable: "Catalog",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Catalog_Seo",
                        column: x => x.SeoId,
                        principalTable: "SEO",
                        principalColumn: "SeoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorFollower",
                columns: table => new
                {
                    VendorFollowerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    FollowedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorFollower", x => x.VendorFollowerId);
                    table.ForeignKey(
                        name: "FK_VendorFollower_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorFollower_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorReview",
                columns: table => new
                {
                    VendorReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Review = table.Column<string>(maxLength: 500, nullable: true),
                    ReviewedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorReview", x => x.VendorReviewId);
                    table.ForeignKey(
                        name: "FK_VendorReview_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorReview_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorWarehouse",
                columns: table => new
                {
                    VendorWarehouseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    AssignedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    AssignedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorWarehouse", x => x.VendorWarehouseId);
                    table.ForeignKey(
                        name: "FK_VendorWarehouse_Registration",
                        column: x => x.AssignedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorWarehouse_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorWarehouse_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CatalogAttribute",
                columns: table => new
                {
                    CatalogAttributeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogId = table.Column<int>(nullable: false),
                    AttributeId = table.Column<int>(nullable: false),
                    AssignedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    AssignedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogAttribute", x => x.CatalogAttributeId);
                    table.ForeignKey(
                        name: "FK_CatalogAttribute_Registration",
                        column: x => x.AssignedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogAttribute_AllAttribute",
                        column: x => x.AttributeId,
                        principalTable: "AllAttribute",
                        principalColumn: "AttributeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogAttribute_Catalog",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CatalogBrand",
                columns: table => new
                {
                    CatalogBrandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    AssignedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    AssignedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrand", x => x.CatalogBrandId);
                    table.ForeignKey(
                        name: "FK_CatalogBrand_Registration",
                        column: x => x.AssignedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogBrand_AllBrand",
                        column: x => x.BrandId,
                        principalTable: "AllBrand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogBrand_Catalog",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CatalogShownPlace",
                columns: table => new
                {
                    CatalogShownPlaceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogId = table.Column<int>(nullable: false),
                    ShownPlace = table.Column<string>(maxLength: 128, nullable: false),
                    DisplayOrder = table.Column<int>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogShownPlace", x => x.CatalogShownPlaceId);
                    table.ForeignKey(
                        name: "FK_CatalogShownPlace_Catalog",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogShownPlace_Registration",
                        column: x => x.CreatedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CatalogSpecification",
                columns: table => new
                {
                    CatalogSpecificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogId = table.Column<int>(nullable: false),
                    SpecificationId = table.Column<int>(nullable: false),
                    AssignedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    AssignedByRegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogSpecification", x => x.CatalogSpecificationId);
                    table.ForeignKey(
                        name: "FK_CatalogSpecification_Registration",
                        column: x => x.AssignedByRegistrationId,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogSpecification_Catalog",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogSpecification_AllSpecification",
                        column: x => x.SpecificationId,
                        principalTable: "AllSpecification",
                        principalColumn: "SpecificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    CatalogId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: true),
                    SeoId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 4000, nullable: true),
                    FullDescription = table.Column<string>(nullable: true),
                    AdminComment = table.Column<string>(maxLength: 4000, nullable: true),
                    StockQuantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DisplayOrder = table.Column<int>(nullable: true),
                    OldPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SpecialPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    SpecialPriceStartDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    SpecialPriceEndDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    Published = table.Column<bool>(nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_AllBrand",
                        column: x => x.BrandId,
                        principalTable: "AllBrand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Catalog",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_SEO",
                        column: x => x.SeoId,
                        principalTable: "SEO",
                        principalColumn: "SeoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    ProductAttributeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    AttributeId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: true),
                    DisplayType = table.Column<string>(maxLength: 50, nullable: false),
                    InsertedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.ProductAttributeId);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_AllAttribute",
                        column: x => x.AttributeId,
                        principalTable: "AllAttribute",
                        principalColumn: "AttributeId");
                    table.ForeignKey(
                        name: "FK_ProductAttribute_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeSet",
                columns: table => new
                {
                    ProductAttributeSetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    PriceAdjustment = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    InsertedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
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
                name: "ProductBlob",
                columns: table => new
                {
                    ProductBlobId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    BlobUrl = table.Column<string>(maxLength: 255, nullable: false),
                    DisplayOrder = table.Column<int>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBlob", x => x.ProductBlobId);
                    table.ForeignKey(
                        name: "FK_ProductBlob_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductReview",
                columns: table => new
                {
                    ProductReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Review = table.Column<string>(maxLength: 500, nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    ReviewedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReview", x => x.ProductReviewId);
                    table.ForeignKey(
                        name: "FK_ProductReview_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReview_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecification",
                columns: table => new
                {
                    ProductSpecificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    SpecificationId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 255, nullable: false),
                    InsertedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecification", x => x.ProductSpecificationId);
                    table.ForeignKey(
                        name: "FK_ProductSpecification_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSpecification_AllSpecification",
                        column: x => x.SpecificationId,
                        principalTable: "AllSpecification",
                        principalColumn: "SpecificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeSetList",
                columns: table => new
                {
                    ProductAttributeSetListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAttributeSetId = table.Column<int>(nullable: false),
                    ProductAttributeId = table.Column<int>(nullable: false)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5A71C6C4-9488-4BCC-A680-445A34C6E721", "5A71C6C4-9488-4BCC-A680-445A34C6E721", "admin", "ADMIN" },
                    { "F73A5277-2535-48A4-A371-300508ADDD2F", "F73A5277-2535-48A4-A371-300508ADDD2F", "sub-admin", "SUB-ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "A0456563-F978-4135-B563-97F23EA02FDA", 0, "A0456563-F978-4135-B563-97F23EA02FDA", "admin@gmail.com", true, true, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEDch3arYEB9dCAudNdsYEpVX7ryywa8f3ZIJSVUmEThAI50pLh9RyEu7NjGJccpOog==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Registration",
                columns: new[] { "RegistrationId", "DateofBirth", "Email", "ImageUrl", "Name", "Phone", "Type", "UserName" },
                values: new object[] { 1, null, null, null, "Admin", null, "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "A0456563-F978-4135-B563-97F23EA02FDA", "5A71C6C4-9488-4BCC-A680-445A34C6E721" });

            migrationBuilder.CreateIndex(
                name: "IX_AllAttribute_CreatedByRegistrationId",
                table: "AllAttribute",
                column: "CreatedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_AllBrand_CreatedByRegistrationId",
                table: "AllBrand",
                column: "CreatedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_AllSpecification_CreatedByRegistrationId",
                table: "AllSpecification",
                column: "CreatedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CreatedByRegistrationId",
                table: "Catalog",
                column: "CreatedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_ParentCatalogId",
                table: "Catalog",
                column: "ParentCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_SeoId",
                table: "Catalog",
                column: "SeoId",
                unique: true,
                filter: "[SeoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogAttribute_AssignedByRegistrationId",
                table: "CatalogAttribute",
                column: "AssignedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogAttribute_AttributeId",
                table: "CatalogAttribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogAttribute_CatalogId",
                table: "CatalogAttribute",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogBrand_AssignedByRegistrationId",
                table: "CatalogBrand",
                column: "AssignedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogBrand_BrandId",
                table: "CatalogBrand",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogBrand_CatalogId",
                table: "CatalogBrand",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogShownPlace_CatalogId",
                table: "CatalogShownPlace",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogShownPlace_CreatedByRegistrationId",
                table: "CatalogShownPlace",
                column: "CreatedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogSpecification_AssignedByRegistrationId",
                table: "CatalogSpecification",
                column: "AssignedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogSpecification_CatalogId",
                table: "CatalogSpecification",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogSpecification_SpecificationId",
                table: "CatalogSpecification",
                column: "SpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_RegistrationId",
                table: "Customer",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CatalogId",
                table: "Product",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SeoId",
                table: "Product",
                column: "SeoId",
                unique: true,
                filter: "[SeoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Product_VendorId",
                table: "Product",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_AttributeId",
                table: "ProductAttribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_ProductId",
                table: "ProductAttribute",
                column: "ProductId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductBlob_ProductId",
                table: "ProductBlob",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_CustomerId",
                table: "ProductReview",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecification_ProductId",
                table: "ProductSpecification",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecification_SpecificationId",
                table: "ProductSpecification",
                column: "SpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_SEO_CreatedByRegistrationId",
                table: "SEO",
                column: "CreatedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Slider_CreatedByRegistrationId",
                table: "Slider",
                column: "CreatedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_RegistrationId",
                table: "Vendor",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorFollower_CustomerId",
                table: "VendorFollower",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorFollower_VendorId",
                table: "VendorFollower",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorReview_CustomerId",
                table: "VendorReview",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorReview_VendorId",
                table: "VendorReview",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorWarehouse_AssignedByRegistrationId",
                table: "VendorWarehouse",
                column: "AssignedByRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorWarehouse_VendorId",
                table: "VendorWarehouse",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorWarehouse_WarehouseId",
                table: "VendorWarehouse",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_CreatedByRegistrationId",
                table: "Warehouse",
                column: "CreatedByRegistrationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CatalogAttribute");

            migrationBuilder.DropTable(
                name: "CatalogBrand");

            migrationBuilder.DropTable(
                name: "CatalogShownPlace");

            migrationBuilder.DropTable(
                name: "CatalogSpecification");

            migrationBuilder.DropTable(
                name: "ProductAttributeSetList");

            migrationBuilder.DropTable(
                name: "ProductBlob");

            migrationBuilder.DropTable(
                name: "ProductReview");

            migrationBuilder.DropTable(
                name: "ProductSpecification");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropTable(
                name: "VendorFollower");

            migrationBuilder.DropTable(
                name: "VendorReview");

            migrationBuilder.DropTable(
                name: "VendorWarehouse");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.DropTable(
                name: "ProductAttributeSet");

            migrationBuilder.DropTable(
                name: "AllSpecification");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "AllAttribute");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "AllBrand");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "SEO");

            migrationBuilder.DropTable(
                name: "Registration");
        }
    }
}
