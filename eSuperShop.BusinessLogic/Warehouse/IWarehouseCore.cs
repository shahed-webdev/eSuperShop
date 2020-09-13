using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IWarehouseCore
    {
        DbResponse<WarehouseModel> Add(WarehouseAddModel model, string userName);
        DbResponse Delete(int id);
        DbResponse AssignCatalog(WarehouseAssignModel model, string userName);
        DataResult<WarehouseModel> List(DataRequest request);
        DbResponse<List<DDL>> Ddl();
    }

}