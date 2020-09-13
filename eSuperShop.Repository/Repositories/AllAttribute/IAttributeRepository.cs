using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface IAttributeRepository
    {
        AllAttribute Attribute { get; set; }
        CatalogAttribute CatalogAttribute { get; set; }
        void Add(AttributeAddModel model);
        void Delete(int id);
        AttributeModel Get(int id);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        DataResult<AttributeModel> List(DataRequest request);
        List<DDL> ListDdl();
        void AssignCatalog(AttributeAssignModel model);
    }
}