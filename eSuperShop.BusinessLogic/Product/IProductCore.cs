using eSuperShop.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface IProductCore
    {
        DbResponse<CatalogDisplayModel> CatalogDetails(int catalogId);
        DbResponse AddProduct(ProductAddModel model, string vendorUserName);
        DbResponse<List<ICatalogVendorModel>> VendorWiseCatalogList(string vendorUserName);
        Task<ICollection<BrandModel>> SearchBrandAsync(int catalogId, string key);
        Task<ICollection<AttributeModel>> SearchAttributeAsync(int catalogId, string key);
        Task<ICollection<SpecificationModel>> SearchSpecificationAsync(int catalogId, string key);

    }
}