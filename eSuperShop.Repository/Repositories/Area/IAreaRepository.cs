using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.Repository.Repositories
{
    public interface IAreaRepository
    {
        Area Area { get; set; }
        void Add(AreaAddEditModel model);
        void Delete(int id);
        AreaAddEditModel Get(int id);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        DataResult<AreaAddEditModel> List(DataRequest request);
        List<DDL> ListDdl();
        List<AreaAddEditModel> GetRegionWiseArea(List<int> regionIds);
    }
}