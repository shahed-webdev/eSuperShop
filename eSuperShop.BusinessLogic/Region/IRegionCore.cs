using eSuperShop.Repository;
using eSuperShop.Repository.Repositories;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IRegionCore
    {
        DbResponse<RegionAddEditModel> Add(RegionAddEditModel model);
        DbResponse Edit(RegionAddEditModel model);
        DbResponse Delete(int id);
        DbResponse<RegionAddEditModel> Get(int id);
        DataResult<RegionAddEditModel> List(DataRequest request);
        List<DDL> ListDdl();
    }
}