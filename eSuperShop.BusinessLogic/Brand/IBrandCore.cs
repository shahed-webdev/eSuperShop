using CloudStorage;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface IBrandCore
    {
        DbResponse<BrandModel> Add(BrandAddModel model, string userName);
        DbResponse<BrandModel> Get(int id);
        DbResponse Delete(int id);
        Task<DbResponse> Edit(BrandEditModel model, IFormFile fileLogo, ICloudStorage cloudStorage);
        DbResponse AssignCatalog(BrandAssignModel model, string userName);
        DbResponse AssignCatalogMultiple(BrandAssignMultipleModel model, string userName);
        DbResponse UnAssignCatalog(int brandId, int catalogId);
        Task<ICollection<BrandModel>> SearchAsync(string key);
        DataResult<BrandModel> List(DataRequest request);
        DbResponse<List<DDL>> Ddl();
        DbResponse<List<BrandModel>> CatalogWiseList(int catalogId);
    }
}