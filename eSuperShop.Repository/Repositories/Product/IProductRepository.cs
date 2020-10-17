using eSuperShop.Data;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface IProductRepository : ISeoRepository
    {
        ProductQuantitySet ProductQuantitySet { get; set; }
        void Add(ProductAddModel model);
        bool IsExistSlugUrl(string slugUrl);
        bool IsExistSlugUrl(string slugUrl, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        ICollection<ProductUnpublishedModel> UnpublishedList(int vendorId);
        ICollection<ProductUnpublishedModel> PublishedList(int vendorId);
        ProductDetailsModel Details(int productId);
        bool IsProductExist(int vendorId, int productId);
        void QuantityAdd(ProductQuantityAddModel model);
        void QuantityUpdate(ProductQuantityViewModel model);
        ProductQuantityViewModel GetQuantitySet(ProductQuantityCheckModel model);
        ProductQuantityViewModel GetQuantitySetById(int productQuantitySetId);

        ICollection<ProductQuantitySetViewModel> GetQuantitySetList(int productId);
        ProductQuantitySetViewModel GetQuantitySetDetailsById(int productQuantitySetId);


        void PublishedUpdate(int productId, bool published);
        void UpdateMainQuantity(int productId);
        int GetStock(int productId);
    }

}