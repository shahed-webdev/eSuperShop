using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<ICollection<AttributeModel>> SearchAsync(string key);

        void AssignCatalog(AttributeAssignModel model);
        void AssignCatalogMultiple(AttributeAssignMultipleModel model);
        void UnAssignCatalog(int attributeId, int catalogId);
        bool IsExistAttributeInCatalog(int attributeId, int catalogId);
    }


}