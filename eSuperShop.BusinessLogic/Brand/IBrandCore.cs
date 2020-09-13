using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IBrandCore
    {
        DbResponse<BrandModel> Add(BrandAddModel model, string userName);
        DbResponse Delete(int id);
        DbResponse AssignCatalog(BrandAssignModel model, string userName);
        DataResult<BrandModel> List(DataRequest request);
        DbResponse<List<DDL>> Ddl();
    }
}