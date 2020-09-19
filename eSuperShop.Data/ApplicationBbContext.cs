using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eSuperShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<AllAttribute> AllAttribute { get; set; }
        public virtual DbSet<AllBrand> AllBrand { get; set; }
        public virtual DbSet<AllSpecification> AllSpecification { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<CatalogAttribute> CatalogAttribute { get; set; }
        public virtual DbSet<CatalogBrand> CatalogBrand { get; set; }
        public virtual DbSet<CatalogShownPlace> CatalogShownPlace { get; set; }
        public virtual DbSet<CatalogSpecification> CatalogSpecification { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttribute { get; set; }
        public virtual DbSet<ProductAttributeSet> ProductAttributeSet { get; set; }
        public virtual DbSet<ProductAttributeSetList> ProductAttributeSetList { get; set; }
        public virtual DbSet<ProductBlob> ProductBlob { get; set; }
        public virtual DbSet<ProductReview> ProductReview { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecification { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<Seo> Seo { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<VendorCatalog> VendorCatalog { get; set; }
        public virtual DbSet<VendorFollower> VendorFollower { get; set; }
        public virtual DbSet<VendorReview> VendorReview { get; set; }
        public virtual DbSet<VendorStoreSlider> VendorStoreSlider { get; set; }
        public virtual DbSet<VendorWarehouse> VendorWarehouse { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AllAttributeConfiguration());
            builder.ApplyConfiguration(new AllBrandConfiguration());
            builder.ApplyConfiguration(new AllSpecificationConfiguration());
            builder.ApplyConfiguration(new CatalogAttributeConfiguration());
            builder.ApplyConfiguration(new CatalogBrandConfiguration());
            builder.ApplyConfiguration(new CatalogConfiguration());
            builder.ApplyConfiguration(new CatalogShownPlaceConfiguration());
            builder.ApplyConfiguration(new CatalogSpecificationConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new ProductAttributeConfiguration());
            builder.ApplyConfiguration(new ProductAttributeSetConfiguration());
            builder.ApplyConfiguration(new ProductAttributeSetListConfiguration());
            builder.ApplyConfiguration(new ProductBlobConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductReviewConfiguration());
            builder.ApplyConfiguration(new ProductSpecificationConfiguration());
            builder.ApplyConfiguration(new RegistrationConfiguration());
            builder.ApplyConfiguration(new SeoConfiguration());
            builder.ApplyConfiguration(new SliderConfiguration());
            builder.ApplyConfiguration(new VendorConfiguration());
            builder.ApplyConfiguration(new VendorCatalogConfiguration());
            builder.ApplyConfiguration(new VendorFollowerConfiguration());
            builder.ApplyConfiguration(new VendorReviewConfiguration());
            builder.ApplyConfiguration(new VendorStoreSliderConfiguration());
            builder.ApplyConfiguration(new VendorWarehouseConfiguration());
            builder.ApplyConfiguration(new WarehouseConfiguration());



            base.OnModelCreating(builder);
            builder.SeedAdminData();
        }
    }
}
