using CloudStorage;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorProductCategoryCore
    {
        DbResponse<VendorProductCategoryModel> Add(VendorProductCategoryAddModel model, string vendorUserName);
        DbResponse Delete(int id);
        DbResponse Update(VendorProductCategoryUpdateModel model);
        DbResponse<List<VendorProductCategoryDisplayModel>> ApprovedList(int vendorId);
        DbResponse<List<VendorProductCategoryDisplayModel>> AllList(string vendorUserName);
        DbResponse<List<DDL>> ListDdl(string vendorUserName);
        DbResponse<VendorProductCategoryUpdateModel> Get(int id);
        DbResponse AssignToggle(VendorProductCategoryAssignModel model);
        DbResponse<DataResult<ProductListVendorCategoryWiseModel>> ProductList(DataRequest request, string vendorUserName, int vendorProductCategoryId);

        DataResult<VendorProductCategoryUnapprovedModel> CategoryUnapprovedList(DataRequest request);
        Task<DbResponse> Approved(int vendorProductCategoryId, ICloudStorage cloudStorage);
        Task<DbResponse> Reject(int vendorProductCategoryId, ICloudStorage cloudStorage);
    }
}