using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface ISpecificationCore
    {
        DbResponse<SpecificationModel> Add(SpecificationAddModel model, string userName);
        DbResponse Delete(int id);
        DbResponse AssignCatalog(SpecificationAssignModel model, string userName);
        DbResponse AssignCatalogMultiple(SpecificationAssignMultipleModel model, string userName);
        DbResponse UnAssignCatalog(int specificationId, int catalogId);
        Task<ICollection<SpecificationModel>> SearchAsync(string key);
        DataResult<SpecificationModel> List(DataRequest request);
        DbResponse<List<DDL>> Ddl();
        DbResponse<List<SpecificationModel>> CatalogWiseList(int catalogId);
        DbResponse<List<DDL>> CatalogWiseDdl(int catalogId);
    }


}