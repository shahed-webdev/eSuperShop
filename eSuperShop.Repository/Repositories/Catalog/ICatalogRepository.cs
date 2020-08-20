using eSuperShop.Data;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface ICatalogRepository
    {
        Catalog Catalog { get; set; }
        void Add(CatalogAddModel model);
        void Delete(int id);
        bool IsExistSlugUrl(string slugUrl);
        bool IsExistSlugUrl(string slugUrl, int updateId);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsIsNull(int id);
        bool IsRelatedDataExist(int id);
        List<CatalogDisplayModel> Display(CatalogDisplayPlace place, int numberOfItem);
        List<CatalogDisplayModel> DisplaySubCatalog(int parentCatalogId, int numberOfItem);

        List<CatalogModel> List();
        List<DDL> SliderPlaceDdl();
        List<DDL> ListDdl();
        string Breadcrumb(int id);
    }
}