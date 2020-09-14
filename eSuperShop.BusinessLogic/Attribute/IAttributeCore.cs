using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface IAttributeCore
    {
        DbResponse<AttributeModel> Add(AttributeAddModel model, string userName);
        DbResponse Delete(int id);
        DbResponse AssignCatalog(AttributeAssignModel model, string userName);
        DbResponse AssignCatalogMultiple(AttributeAssignMultipleModel model, string userName);
        DbResponse UnAssignCatalog(int attributeId, int catalogId);
        Task<ICollection<AttributeModel>> SearchAsync(string key);
        DataResult<AttributeModel> List(DataRequest request);
        DbResponse<List<DDL>> Ddl();
    }


}