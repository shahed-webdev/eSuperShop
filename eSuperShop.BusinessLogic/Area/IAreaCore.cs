using eSuperShop.Repository;
using eSuperShop.Repository.Repositories;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IAreaCore
    {
        DbResponse<AreaAddEditModel> Add(AreaAddEditModel model);
        DbResponse Edit(AreaAddEditModel model);
        DbResponse Delete(int id);
        DbResponse<AreaAddEditModel> Get(int id);
        DataResult<AreaAddEditModel> List(DataRequest request);
        List<DDL> ListDdl();
        List<DDL> ListDdlRegionWise(int regionId);
        List<AreaAddEditModel> GetRegionWiseArea(int regionId);

    }
}