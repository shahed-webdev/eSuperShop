using eSuperShop.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface IProductCore
    {
        ISeoCore Seo { get; set; }
        DbResponse<CatalogDisplayModel> CatalogDetails(int catalogId);
        Task<DbResponse<int>> AddProductAsync(ProductAddModel model, string vendorUserName);
        DbResponse<List<ICatalogVendorModel>> VendorWiseCatalogList(string vendorUserName);
        DbResponse<List<ProductUnpublishedModel>> UnpublishedList(string vendorUserName);
        DbResponse<List<ProductUnpublishedModel>> PublishedList(string vendorUserName);
        Task<ICollection<BrandModel>> SearchBrandAsync(int catalogId, string key);
        Task<ICollection<AttributeModel>> SearchAttributeAsync(int catalogId, string key);
        Task<ICollection<SpecificationModel>> SearchSpecificationAsync(int catalogId, string key);
        DbResponse<ProductDetailsModel> Details(string vendorUserName, int productId);
        DbResponse<List<ProductQuantitySetViewModel>> QuantitySetList(int productId, string vendorUserName);
        DbResponse<ProductQuantitySetAddReturnModel> QuantityAdd(ProductQuantityAddModel model, string vendorUserName);
        DbResponse<ProductQuantitySetUpdateReturnModel> QuantityUpdate(ProductQuantityViewModel model);
        DbResponse PublishedUpdate(int productId, bool published, string vendorUserName);
        DbResponse<ProductQuantityViewModel> GetQuantitySet(ProductQuantityCheckModel model, string vendorUserName);
        DbResponse<List<ProductListViewModel>> GetTopRated(ProductFilterRequest request);
        DbResponse<List<ProductListViewModel>> GetFlashDeals(ProductFilterRequest request);
        DbResponse<List<ProductListViewModel>> GetMoreToLove(ProductFilterRequest request);
    }
}