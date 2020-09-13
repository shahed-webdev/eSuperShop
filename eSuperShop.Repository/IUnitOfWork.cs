using System;

namespace eSuperShop.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAttributeRepository Attribute { get; }
        IBrandRepository Brand { get; }
        IRegistrationRepository Registration { get; }
        ISliderRepository Slider { get; }
        ISpecificationRepository Specification { get; }
        ICatalogRepository Catalog { get; }
        IWarehouseRepository Warehouse { get; }
        int SaveChanges();
    }
}