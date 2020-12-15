using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorProductCategoryCore
    {
        DbResponse<VendorProductCategoryModel> Add(VendorProductCategoryAddModel model, string vendorUserName);
        DbResponse Delete(int id);
        DbResponse Update(VendorProductCategoryUpdateModel model);
        DbResponse<List<VendorProductCategoryDisplayModel>> DisplayList(string vendorUserName);
        DbResponse<List<DDL>> ListDdl(string vendorUserName);
        DbResponse<VendorProductCategoryUpdateModel> Get(int id);
        DbResponse PlaceAssign(VendorProductCategoryAssignModel model);
        DbResponse PlaceDelete(VendorProductCategoryAssignModel model);

        DbResponse<DataResult<ProductListVendorCategoryWiseModel>> ProductList(DataRequest request, string vendorUserName, int vendorProductCategoryId);
    }
}