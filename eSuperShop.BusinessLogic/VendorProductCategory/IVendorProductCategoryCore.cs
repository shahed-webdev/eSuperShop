using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorProductCategoryCore
    {
        DbResponse<VendorProductCategoryModel> Add(VendorProductCategoryAddModel model, string vendorUserName);
        DbResponse Delete(int id);
        DbResponse<List<VendorProductCategoryDisplayModel>> DisplayList(string vendorUserName);
        DbResponse<List<DDL>> ListDdl(string vendorUserName);
        DbResponse<VendorProductCategoryModel> Get(int id);
        DbResponse PlaceAssign(VendorProductCategoryAssignModel model);
        DbResponse PlaceDelete(VendorProductCategoryAssignModel model);
    }
}