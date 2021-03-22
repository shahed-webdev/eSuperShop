using eSuperShop.Data;
using Paging.Infrastructure;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface IProductRepository : ISeoRepository
    {
        Product Product { get; set; }
        ProductQuantitySet ProductQuantitySet { get; set; }
        void Add(ProductAddModel model);
        bool IsExistSlugUrl(string slugUrl);
        int ProductIdBySlugUrl(string slugUrl);
        bool IsExistSlugUrl(string slugUrl, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        ICollection<ProductUnpublishedModel> UnpublishedList(int vendorId);
        ICollection<ProductUnpublishedModel> PublishedList(int vendorId);
        ICollection<ProductPendingApprovalListModel> PendingApprovalList();
        ProductDetailsModel Details(int productId);
        ProductDetailsViewModel DetailsView(int productId);
        bool IsProductExist(int vendorId, int productId);
        void QuantityAdd(ProductQuantityAddModel model);
        void QuantitySale(int productQuantitySetId, int quantity);
        void QuantityRestock(int productQuantitySetId, int quantity);
        void QuantityUpdate(ProductQuantityViewModel model);
        ProductQuantitySetViewModel GetQuantitySet(ProductQuantityCheckModel model);
        ProductQuantityViewModel GetQuantitySetById(int productQuantitySetId);
        int GetQuantityBySetId(int productQuantitySetId);

        ICollection<ProductQuantitySetViewModel> GetQuantitySetList(int productId);
        ProductQuantitySetViewModel GetQuantitySetDetailsById(int productQuantitySetId);


        void PublishedUpdate(int productId, bool published);
        void UpdateMainQuantity(int productId);
        int GetStock(int productId);
        PagedResult<ProductListViewModel> GetFlashDeals(ProductFilterRequest request);
        PagedResult<ProductListViewModel> GetTopRated(ProductFilterRequest request);
        PagedResult<ProductListViewModel> GetMoreToLove(ProductFilterRequest request);
        PagedResult<ProductListViewModel> GetCatalogWiseList(List<int> catalogIds, ProductFilterRequest request);
        PagedResult<ProductListViewModel> GetVendorWiseList(int vendorId, ProductFilterRequest request);
    }


}