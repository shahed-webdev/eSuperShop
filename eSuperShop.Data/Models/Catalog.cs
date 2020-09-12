using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Catalog
    {
        public Catalog()
        {
            CatalogAttribute = new HashSet<CatalogAttribute>();
            CatalogBrand = new HashSet<CatalogBrand>();
            CatalogShownPlace = new HashSet<CatalogShownPlace>();
            CatalogSpecification = new HashSet<CatalogSpecification>();
            SubCatalog = new HashSet<Catalog>();
            Product = new HashSet<Product>();
        }

        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public int? ParentCatalogId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public int? SeoId { get; set; }
        public int CreatedByRegistrationId { get; set; }

        public virtual Registration CreatedByRegistration { get; set; }
        public virtual Catalog ParentCatalog { get; set; }
        public virtual Seo Seo { get; set; }
        public virtual ICollection<CatalogAttribute> CatalogAttribute { get; set; }
        public virtual ICollection<CatalogBrand> CatalogBrand { get; set; }
        public virtual ICollection<CatalogShownPlace> CatalogShownPlace { get; set; }
        public virtual ICollection<CatalogSpecification> CatalogSpecification { get; set; }
        public virtual ICollection<Catalog> SubCatalog { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
