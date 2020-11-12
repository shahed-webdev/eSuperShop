using eSuperShop.Data;
using eSuperShop.Repository;
using System.Collections.Generic;
using Paging.Infrastructure;

namespace eSuperShop.BusinessLogic
{
    public interface ICatalogCore
    {
        DbResponse<CatalogDisplayModel> Add(CatalogAddModel model, string userName);
        DbResponse Delete(int id);
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


    }


}