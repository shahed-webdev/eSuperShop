using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.Repository.Repositories
{
    public interface IAreaRepository
    {
        DbResponse<AreaAddEditModel> Add(AreaAddEditModel model);
        DbResponse Edit(AreaAddEditModel model);
        DbResponse Delete(int id);
        DbResponse<AreaAddEditModel> Get(int id);
        bool IsExistName(string name, int regionId);
        bool IsExistName(string name, int regionId, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        DataResult<AreaAddEditModel> List(DataRequest request);
        List<DDL> ListDdl();
        List<DDL> ListDdlRegionWise(int regionId);
        List<AreaAddEditModel> GetRegionWiseArea(int regionId);
    }
}