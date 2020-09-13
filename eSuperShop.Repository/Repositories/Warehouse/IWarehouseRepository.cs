using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface IWarehouseRepository
    {
        Warehouse Warehouse { get; set; }
        VendorWarehouse CatalogWarehouse { get; set; }
        void Add(WarehouseAddModel model);
        void Delete(int id);
        WarehouseModel Get(int id);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        DataResult<WarehouseModel> List(DataRequest request);
        List<DDL> ListDdl();
        void AssignCatalog(WarehouseAssignModel model);
    }
}