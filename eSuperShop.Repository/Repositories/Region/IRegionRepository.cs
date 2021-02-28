using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.Repository.Repositories
{
    public interface IRegionRepository
    {
        DbResponse<RegionAddEditModel> Add(RegionAddEditModel model);
        DbResponse Edit(RegionAddEditModel model);
        DbResponse Delete(int id);
        DbResponse<RegionAddEditModel> Get(int id);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        DataResult<RegionAddEditModel> List(DataRequest request);
        List<DDL> ListDdl();
    }
}