using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface ISpecificationCore
    {
        DbResponse<SpecificationModel> Add(SpecificationAddModel model, string userName);
        DbResponse Delete(int id);
        DbResponse AssignCatalog(SpecificationAssignModel model, string userName);
        DataResult<SpecificationModel> List(DataRequest request);
        DbResponse<List<DDL>> Ddl();
    }


}