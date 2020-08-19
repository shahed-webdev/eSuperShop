using eSuperShop.Data;
using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface ICatalogCore
    {
        DbResponse<SliderListModel> Add(CatalogAddModel model, string userName);
        DbResponse Delete(int id);
        DbResponse<List<CatalogDisplayModel>> Display(CatalogDisplayPlace place, int numberOfItem);
        DbResponse<List<CatalogModel>> List();
        DbResponse<string> Breadcrumb(int id);
        List<DDL> DisplayPlaceDdl();
        DbResponse<List<DDL>> ListDdl();
    }


}