using eSuperShop.Data;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface ICatalogRepository : ISeoRepository
    {
        Catalog catalog { get; set; }
        CatalogShownPlace catalogShownPlace { get; set; }
        void Add(CatalogAddModel model);
        void Delete(int id);
        bool IsExistSlugUrl(string slugUrl);
        bool IsExistSlugUrl(string slugUrl, int updateId);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        List<CatalogDisplayModel> DisplayList(CatalogDisplayPlace place, int numberOfItem);
        List<CatalogDisplayModel> DisplayList(CatalogDisplayPlace place);
        List<CatalogDisplayModel> DisplaySubCatalog(int parentCatalogId, int numberOfItem);

        IEnumerable<ICatalogModel> List();
        IEnumerable<ICatalogModel> BrandWiseList(int brandId);
        IEnumerable<ICatalogModel> AttributeWiseList(int attributeId);
        IEnumerable<ICatalogModel> SpecificationWiseList(int specificationId);
        List<DDL> SliderPlaceDdl();
        List<DDL> ListDdl();
        string Breadcrumb(int id);
        CatalogDisplayModel Get(int id);
        void PlaceAssign(CatalogAssignModel model);
        bool IsPlaceAssign(int catalogId, CatalogDisplayPlace shownPlace);
        void PlaceDelete(int catalogId, CatalogDisplayPlace shownPlace);
    }
}