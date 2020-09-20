using eSuperShop.Data;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface IVendorProductCategoryRepository
    {
        VendorProductCategory VendorProductCategory { get; set; }

        void Add(VendorProductCategoryModel model);
        void Delete(int vendorProductCategoryId);
        void Update(VendorProductCategoryModel model);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsNull(int vendorProductCategoryId);
        bool IsRelatedDataExist(int id);
        List<VendorProductCategoryDisplayModel> DisplayList(int vendorId);

        List<DDL> ListDdl(int vendorId);
        VendorProductCategoryModel Get(int id);
        void PlaceAssign(VendorProductCategoryAssignModel model);
        bool IsPlaceAssign(int vendorProductCategoryId, int productId);
        void PlaceDelete(int vendorProductCategoryId, int productId);

        List<VendorCategoryProductsModel> Products(int vendorProductCategoryId);
    }


}