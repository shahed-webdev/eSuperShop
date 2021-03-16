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
        List<VendorProductCategoryDisplayModel> ApprovedList(int vendorId);
        List<VendorProductCategoryDisplayModel> AllList(int vendorId);

        List<DDL> ListDdl(int vendorId);
        VendorProductCategoryUpdateModel Get(int vendorProductCategoryId);


        DataResult<ProductListVendorCategoryWiseModel> ProductList(DataRequest request, int vendorId, int vendorProductCategoryId);

        void Assign(VendorProductCategoryAssignModel model);
        bool IsPlaceAssign(VendorProductCategoryAssignModel model);
        void Unassigned(VendorProductCategoryAssignModel model);
        List<VendorCategoryProductsModel> Products(int vendorProductCategoryId);



        DataResult<VendorProductCategoryUnapprovedModel> CategoryUnapprovedList(DataRequest request);
        string Approved(int vendorProductCategoryId);
        string Reject(int vendorProductCategoryId);
    }


}