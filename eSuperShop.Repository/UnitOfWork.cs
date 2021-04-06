using AutoMapper;
using eSuperShop.Data;
using eSuperShop.Repository.Repositories;

namespace eSuperShop.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public UnitOfWork(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

            Area = new AreaRepository(_db, _mapper);
            Attribute = new AttributeRepository(_db, _mapper);
            Brand = new BrandRepository(_db, _mapper);
            Customer = new CustomerRepository(_db, _mapper);
            GeneralSetting = new GeneralSettingRepository(_db, _mapper);
            Order = new OrderRepository(_db, _mapper);
            OrderShippingAddress = new OrderShippingAddressRepository(_db, _mapper);
            Product = new ProductRepository(_db, _mapper);
            ProductReview = new ProductReviewRepository(_db, _mapper);
            ProductFaq = new ProductFaqRepository(_db, _mapper);
            Registration = new RegistrationRepository(_db, _mapper);
            Region = new RegionRepository(_db, _mapper);
            Slider = new SliderRepository(_db, _mapper);
            Specification = new SpecificationRepository(_db, _mapper);
            Catalog = new CatalogRepository(_db, _mapper);
            Vendor = new VendorRepository(_db, _mapper);
            VendorStoreSlider = new VendorStoreSliderRepository(_db, _mapper);
            VendorProductCategory = new VendorProductCategoryRepository(_db, _mapper);
            Warehouse = new WarehouseRepository(_db, _mapper);
        }

        public IAreaRepository Area { get; }
        public IAttributeRepository Attribute { get; }
        public IBrandRepository Brand { get; }
        public ICustomerRepository Customer { get; }
        public IGeneralSettingRepository GeneralSetting { get; }
        public IOrderRepository Order { get; }
        public IOrderShippingAddressRepository OrderShippingAddress { get; }
        public IProductRepository Product { get; }
        public IProductReviewRepository ProductReview { get; }
        public IProductFaqRepository ProductFaq { get; }
        public IRegistrationRepository Registration { get; }
        public IRegionRepository Region { get; }
        public ISliderRepository Slider { get; }
        public ISpecificationRepository Specification { get; }
        public ICatalogRepository Catalog { get; }
        public IVendorRepository Vendor { get; }
        public IVendorStoreSliderRepository VendorStoreSlider { get; }
        public IVendorProductCategoryRepository VendorProductCategory { get; }
        public IWarehouseRepository Warehouse { get; }

        public void Dispose()
        {
            _db.Dispose();
        }


        public int SaveChanges()
        {
            return _db.SaveChanges();
        }
    }
}