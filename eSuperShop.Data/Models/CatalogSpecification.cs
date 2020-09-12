using System;

namespace eSuperShop.Data
{
    public class CatalogSpecification
    {
        public int CatalogSpecificationId { get; set; }
        public int CatalogId { get; set; }
        public int SpecificationId { get; set; }
        public DateTime AssignedOnUtc { get; set; }
        public int AssignedByRegistrationId { get; set; }

        public virtual Registration AssignedByRegistration { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual AllSpecification Specification { get; set; }
    }
}
