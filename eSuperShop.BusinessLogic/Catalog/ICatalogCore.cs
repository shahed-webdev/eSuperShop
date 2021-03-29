using CloudStorage;
using eSuperShop.Data;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Http;
using Paging.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface ICatalogCore
    {
        Task<DbResponse<CatalogDisplayModel>> AddAsync(CatalogAddModel model, string userName, ICloudStorage cloudStorage, IFormFile image);
        Task<DbResponse> EditAsync(CatalogDisplayModel model, ICloudStorage cloudStorage, IFormFile image);

        DbResponse<CatalogDisplayModel> Get(int id);
        Task<DbResponse> DeleteAsync(int id, ICloudStorage cloudStorage);
        DbResponse<List<CatalogDisplayModel>> GetDisplayList(CatalogDisplayPlace place, int numberOfItem);
        DbResponse<List<CatalogDisplayModel>> GetDisplayList(CatalogDisplayPlace place);
        DbResponse<List<ICatalogModel>> List();
        DbResponse<List<ICatalogModel>> BrandWiseList(int brandId);
        DbResponse<List<ICatalogModel>> AttributeWiseList(int attributeId);
        DbResponse<List<ICatalogModel>> SpecificationWiseList(int specificationId);
        DbResponse<List<ICatalogVendorModel>> VendorWiseList(int vendorId);
        DbResponse<CatalogAssignDetailsModel> AssignDetails(int catalogId);
        DbResponse<string> Breadcrumb(int id);
        List<DDL> DisplayPlaceDdl();
        DbResponse<List<DDL>> ListDdl();
        DbResponse<CatalogDisplayModel> AssignPlace(CatalogAssignModel model, string userName);
        DbResponse<CatalogProductListViewModel> ProductListPageData(string slugUrl, int pageSize);
        DbResponse<PagedResult<ProductListViewModel>> GetCatalogWiseList(string slugUrl, ProductFilterRequest request);
        DbResponse DeletePlace(int catalogId, CatalogDisplayPlace shownPlace);
        DbResponse<CatalogShippingCostViewModel> GetShippingCost(int catalogId);
        DbResponse ShippingCostChanged(CatalogShippingCostViewModel model);

    }


}