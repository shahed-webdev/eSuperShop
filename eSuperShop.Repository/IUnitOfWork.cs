using System;

namespace eSuperShop.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAttributeRepository Attribute { get; }
        IBrandRepository Brand { get; }
        ICustomerRepository Customer { get; }
        IProductRepository Product { get; }
        IProductReviewRepository ProductReview { get; }
        IProductFaqRepository ProductFaq { get; }
        IRegistrationRepository Registration { get; }
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