using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface IBrandRepository
    {
        AllBrand brand { get; set; }
        CatalogBrand catalogBrand { get; set; }
        void Add(BrandAddModel model);
        void Delete(int id);
        BrandModel Get(int id);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        DataResult<BrandModel> List(DataRequest request);
        List<DDL> ListDdl();
        void AssignCatalog(BrandAssignModel model);
        //void PlaceDelete(int catalogId, CatalogDisplayPlace shownPlace);
    }
}