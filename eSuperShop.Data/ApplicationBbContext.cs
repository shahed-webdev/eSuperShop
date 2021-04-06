using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eSuperShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<AllAttribute> AllAttribute { get; set; }
        public virtual DbSet<AllBrand> AllBrand { get; set; }
        public virtual DbSet<AllSpecification> AllSpecification { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<CatalogAttribute> CatalogAttribute { get; set; }
        public virtual DbSet<CatalogBrand> CatalogBrand { get; set; }
        public virtual DbSet<CatalogShownPlace> CatalogShownPlace { get; set; }
        public virtual DbSet<CatalogSpecification> CatalogSpecification { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerAddressBook> CustomerAddressBook { get; set; }
        public virtual DbSet<GeneralSetting> GeneralSetting { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderList> OrderList { get; set; }
        public virtual DbSet<OrderShippingAddress> OrderShippingAddress { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttribute { get; set; }
        public virtual DbSet<ProductAttributeValue> ProductAttributeValue { get; set; }
        public virtual DbSet<ProductQuantitySet> ProductQuantitySet { get; set; }
        public virtual DbSet<ProductQuantitySetAttribute> ProductQuantitySetAttribute { get; set; }
        public virtual DbSet<ProductBlob> ProductBlob { get; set; }
        public virtual DbSet<ProductFaq> ProductFaq { get; set; }
        public virtual DbSet<ProductReview> ProductReview { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecification { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Seo> Seo { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<VendorCertificate> VendorCertificate { get; set; }
        public virtual DbSet<VendorCatalog> VendorCatalog { get; set; }
        public virtual DbSet<VendorFollower> VendorFollower { get; set; }
        public virtual DbSet<VendorProductCategory> VendorProductCategory { get; set; }
        public virtual DbSet<VendorProductCategoryList> VendorProductCategoryList { get; set; }
        public virtual DbSet<VendorReview> VendorReview { get; set; }
        public virtual DbSet<VendorStoreSlider> VendorStoreSlider { get; set; }
        public virtual DbSet<VendorWarehouse> VendorWarehouse { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AreaConfiguration());
            builder.ApplyConfiguration(new AllAttributeConfiguration());
            builder.ApplyConfiguration(new AllBrandConfiguration());
            builder.ApplyConfiguration(new AllSpecificationConfiguration());
            builder.ApplyConfiguration(new CatalogAttributeConfiguration());
            builder.ApplyConfiguration(new CatalogBrandConfiguration());
            builder.ApplyConfiguration(new CatalogConfiguration());
            builder.ApplyConfiguration(new CatalogShownPlaceConfiguration());
            builder.ApplyConfiguration(new CatalogSpecificationConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new CustomerAddressBookConfiguration());
            builder.ApplyConfiguration(new GeneralSettingConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderListConfiguration());
            builder.ApplyConfiguration(new OrderShippingAddressConfiguration());
            builder.ApplyConfiguration(new ProductAttributeConfiguration());
            builder.ApplyConfiguration(new ProductAttributeValueConfiguration());
            builder.ApplyConfiguration(new ProductQuantitySetConfiguration());
            builder.ApplyConfiguration(new ProductQuantitySetAttributeConfiguration());
            builder.ApplyConfiguration(new ProductBlobConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductFaqConfiguration());
            builder.ApplyConfiguration(new ProductReviewConfiguration());
            builder.ApplyConfiguration(new ProductSpecificationConfiguration());
            builder.ApplyConfiguration(new RegistrationConfiguration());
            builder.ApplyConfiguration(new RegionConfiguration());
            builder.ApplyConfiguration(new SeoConfiguration());
            builder.ApplyConfiguration(new SliderConfiguration());
            builder.ApplyConfiguration(new VendorConfiguration());
            builder.ApplyConfiguration(new VendorCertificateConfiguration());
            builder.ApplyConfiguration(new VendorCatalogConfiguration());
            builder.ApplyConfiguration(new VendorFollowerConfiguration());
            builder.ApplyConfiguration(new VendorProductCategoryConfiguration());
            builder.ApplyConfiguration(new VendorProductCategoryListConfiguration());
            builder.ApplyConfiguration(new VendorReviewConfiguration());
            builder.ApplyConfiguration(new VendorStoreSliderConfiguration());
            builder.ApplyConfiguration(new VendorWarehouseConfiguration());
            builder.ApplyConfiguration(new WarehouseConfiguration());



            base.OnModelCreating(builder);
            builder.SeedAdminData();
        }
    }
}
