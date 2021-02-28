using eSuperShop.Repository.Repositories;
using System;

namespace eSuperShop.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAreaRepository Area { get; }
        IAttributeRepository Attribute { get; }
        IBrandRepository Brand { get; }
        ICustomerRepository Customer { get; }
        IOrderRepository Order { get; }
        IOrderShippingAddressRepository OrderShippingAddress { get; }
        IProductRepository Product { get; }
        IProductReviewRepository ProductReview { get; }
        IProductFaqRepository ProductFaq { get; }
        IRegistrationRepository Registration { get; }
        IRegionRepository Region { get; }
        ISliderRepository Slider { get; }
        ISpecificationRepository Specification { get; }
        ICatalogRepository Catalog { get; }
        IVendorRepository Vendor { get; }
        IVendorStoreSliderRepository VendorStoreSlider { get; }
        IVendorProductCategoryRepository VendorProductCategory { get; }
        IWarehouseRepository Warehouse { get; }
        int SaveChanges();
    }
}