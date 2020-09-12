using System;

namespace eSuperShop.Data
{
    public class CatalogAttribute
    {
        public int CatalogAttributeId { get; set; }
        public int CatalogId { get; set; }
        public int AttributeId { get; set; }
        public DateTime AssignedOnUtc { get; set; }
        public int AssignedByRegistrationId { get; set; }

        public virtual Registration AssignedByRegistration { get; set; }
        public virtual AllAttribute Attribute { get; set; }
        public virtual Catalog Catalog { get; set; }
    }
}
