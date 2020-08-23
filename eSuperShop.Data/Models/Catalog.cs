using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Catalog
    {
        public Catalog()
        {
            SubCatalog = new HashSet<Catalog>();
            CatalogShownPlace = new HashSet<CatalogShownPlace>();
        }

        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public int? ParentCatalogId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public int? SeoId { get; set; }
        public int CreatedByRegistrationId { get; set; }

        public virtual Registration CreatedByRegistration { get; set; }
        public virtual Seo Seo { get; set; }
        public virtual ICollection<CatalogShownPlace> CatalogShownPlace { get; set; }

        public virtual Catalog ParentCatalog { get; set; }
        public virtual ICollection<Catalog> SubCatalog { get; set; }
    }
}
