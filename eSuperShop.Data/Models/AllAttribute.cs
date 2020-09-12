using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class AllAttribute
    {
        public AllAttribute()
        {
            CatalogAttribute = new HashSet<CatalogAttribute>();
            ProductAttribute = new HashSet<ProductAttribute>();
        }

        public int AttributeId { get; set; }
        public string KeyName { get; set; }
        public bool AllowFiltering { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedByRegistrationId { get; set; }

        public virtual Registration CreatedByRegistration { get; set; }
        public virtual ICollection<CatalogAttribute> CatalogAttribute { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
    }
}
