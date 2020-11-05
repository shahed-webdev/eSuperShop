using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace eSuperShop.Web
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //for google storage
            services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
            //for Core Classes
            services.AddTransient<IAttributeCore, AttributeCore>();
            services.AddTransient<IBrandCore, BrandCore>();
            services.AddTransient<ICatalogCore, CatalogCore>();
            services.AddTransient<ICustomerCore, CustomerCore>();
            services.AddTransient<IOrderCore, OrderCore>();
            services.AddTransient<IProductCore, ProductCore>();
            services.AddTransient<ISpecificationCore, SpecificationCore>();
            services.AddTransient<ISliderCore, SliderCore>();
            services.AddTransient<IStoreCore, StoreCore>();
            services.AddTransient<IVendorCore, VendorCore>();
            services.AddTransient<IVendorDashboardCore, VendorDashboardCore>();
            services.AddTransient<IVendorSliderCore, VendorSliderCore>();
            services.AddTransient<IVendorProductCategoryCore, VendorProductCategoryCore>();
            return services;
        }

    }
}