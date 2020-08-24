using eSuperShop.Data;
using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface ICatalogCore
    {
        DbResponse<CatalogDisplayModel> Add(CatalogAddModel model, string userName);
        DbResponse Delete(int id);
        DbResponse<List<CatalogDisplayModel>> GetDisplayList(CatalogDisplayPlace place, int numberOfItem);
        DbResponse<List<CatalogDisplayModel>> GetDisplayList(CatalogDisplayPlace place);
        DbResponse<List<CatalogModel>> List();
        DbResponse<string> Breadcrumb(int id);
        List<DDL> DisplayPlaceDdl();
        DbResponse<List<DDL>> ListDdl();
        DbResponse<CatalogDisplayModel> AssignPlace(CatalogAssignModel model, string userName);
        DbResponse DeletePlace(int catalogId, CatalogDisplayPlace shownPlace);
        //DbResponse AddSeo();
    }


}