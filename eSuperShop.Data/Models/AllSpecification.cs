using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class AllSpecification
    {
        public AllSpecification()
        {
            CatalogSpecification = new HashSet<CatalogSpecification>();
            ProductSpecification = new HashSet<ProductSpecification>();
        }

        public int SpecificationId { get; set; }
        public string KeyName { get; set; }
        public bool AllowFiltering { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedByRegistrationId { get; set; }

        public virtual Registration CreatedByRegistration { get; set; }
        public virtual ICollection<CatalogSpecification> CatalogSpecification { get; set; }
        public virtual ICollection<ProductSpecification> ProductSpecification { get; set; }
    }
}
