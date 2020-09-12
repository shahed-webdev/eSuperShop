using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class AllBrand
    {
        public AllBrand()
        {
            CatalogBrand = new HashSet<CatalogBrand>();
            Product = new HashSet<Product>();
        }

        public int BrandId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedByRegistrationId { get; set; }

        public virtual Registration CreatedByRegistration { get; set; }
        public virtual ICollection<CatalogBrand> CatalogBrand { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
