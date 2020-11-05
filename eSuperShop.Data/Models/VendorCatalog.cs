using System;

namespace eSuperShop.Data
{
    public class VendorCatalog
    {
        public int VendorCatalogId { get; set; }
        public int VendorId { get; set; }
        public int CatalogId { get; set; }
        public decimal CommissionPercentage { get; set; }
        public DateTime AssignedOnUtc { get; set; }
        public int AssignedByRegistrationId { get; set; }
        public virtual Registration AssignedByRegistration { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual Catalog Catalog { get; set; }
    }
}