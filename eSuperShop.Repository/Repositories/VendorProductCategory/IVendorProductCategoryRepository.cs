using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface IVendorProductCategoryRepository
    {
        VendorProductCategory VendorProductCategory { get; set; }

        void Add(VendorProductCategoryAddModel model);
        void Delete(int vendorProductCategoryId);
        void Update(VendorProductCategoryUpdateModel model);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsNull(int vendorProductCategoryId);
        bool IsRelatedDataExist(int id);
        List<VendorProductCategoryDisplayModel> DisplayList(int vendorId);

        List<DDL> ListDdl(int vendorId);
        VendorProductCategoryUpdateModel Get(int vendorProductCategoryId);


        DataResult<ProductListVendorCategoryWiseModel> ProductList(DataRequest request, int vendorId, int vendorProductCategoryId);

        void PlaceAssign(VendorProductCategoryAssignModel model);
        bool IsPlaceAssign(VendorProductCategoryAssignModel model);
        void PlaceDelete(VendorProductCategoryAssignModel model);

        List<VendorCategoryProductsModel> Products(int vendorProductCategoryId);
    }


}