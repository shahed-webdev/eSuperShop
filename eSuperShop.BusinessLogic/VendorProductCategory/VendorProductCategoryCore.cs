using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class VendorProductCategoryCore : IVendorProductCategoryCore
    {
        public DbResponse<VendorProductCategoryModel> Add(VendorProductCategoryAddModel model)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse<List<VendorProductCategoryDisplayModel>> DisplayList(string vendorUserName)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse<List<DDL>> ListDdl(string vendorUserName)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse<VendorProductCategoryModel> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse PlaceAssign(VendorProductCategoryAssignModel model)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse PlaceDelete(int vendorProductCategoryId, int productId)
        {
            throw new System.NotImplementedException();
        }
    }
}