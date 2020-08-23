using eSuperShop.Data;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class CatalogAddModel
    {
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public int? ParentCatalogId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

    public class CatalogDisplayModel
    {
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CatalogModel
    {
        public CatalogModel()
        {
            SubCatalog = new HashSet<CatalogModel>();
        }
        public CatalogModel(Catalog catalog)
        {
            CatalogName = catalog.CatalogName;
            CatalogId = catalog.CatalogId;
            SlugUrl = catalog.SlugUrl;
            ImageUrl = catalog.ImageUrl;
            SubCatalog = catalog.SubCatalog.Select(c => new CatalogModel(c));
        }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<CatalogModel> SubCatalog { get; set; }
    }

    public class CatalogAssignModel
    {
        public int CatalogShownPlaceId { get; set; }
        public int CatalogId { get; set; }
        public CatalogDisplayPlace ShownPlace { get; set; }
        public int DisplayOrder { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

}