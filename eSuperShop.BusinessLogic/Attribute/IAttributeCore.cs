using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IAttributeCore
    {
        DbResponse<AttributeModel> Add(AttributeAddModel model, string userName);
        DbResponse Delete(int id);
        DbResponse AssignCatalog(AttributeAssignModel model, string userName);
        DataResult<AttributeModel> List(DataRequest request);
        DbResponse<List<DDL>> Ddl();
    }


}