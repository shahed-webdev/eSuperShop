using eSuperShop.Repository;
using Paging.Infrastructure;
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
        DbResponse<ProductDetailsModel> DetailsBySlugUrl(string slugUrl);
        DbResponse<List<ProductQuantitySetViewModel>> QuantitySetList(int productId, string vendorUserName);
        DbResponse<ProductQuantitySetAddReturnModel> QuantityAdd(ProductQuantityAddModel model, string vendorUserName);
        DbResponse<ProductQuantitySetUpdateReturnModel> QuantityUpdate(ProductQuantityViewModel model);
        DbResponse PublishedUpdate(int productId, bool published, string vendorUserName);
        DbResponse<ProductQuantityViewModel> GetQuantitySet(ProductQuantityCheckModel model);
        DbResponse<ProductQuantityViewModel> GetQuantitySet(ProductQuantityCheckModel model, string vendorUserName);
        DbResponse<PagedResult<ProductListViewModel>> GetTopRated(ProductFilterRequest request);
        DbResponse<PagedResult<ProductListViewModel>> GetFlashDeals(ProductFilterRequest request);
        DbResponse<PagedResult<ProductListViewModel>> GetMoreToLove(ProductFilterRequest request);


        DbResponse AddReview(ProductReviewAddModel model);
        DbResponse<PagedResult<ProductReviewViewModel>> ReviewList(ProductReviewFilerRequest request);
        DbResponse<ProductReviewAverageModel> AverageReview(int productId);


        DbResponse FaqAdd(ProductFaqAddModel model);
        DbResponse FaqAnswerAdd(ProductFaqAnswerModel model);
        DbResponse<PagedResult<FaqProductWiseViewModel>> FaqProductWiseList(ProductReviewFilerRequest request);
        DbResponse<PagedResult<FaqCustomerWiseViewModel>> FaqCustomerWiseList(ProductReviewFilerRequest request);
        DbResponse<PagedResult<FaqVendorWiseViewModel>> FaqVendorWiseList(ProductReviewFilerRequest request);
    }
}