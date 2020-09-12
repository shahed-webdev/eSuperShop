using System;

namespace eSuperShop.Data
{
    public class VendorWarehouse
    {
        public int VendorWarehouseId { get; set; }
        public int VendorId { get; set; }
        public int WarehouseId { get; set; }
        public DateTime AssignedOnUtc { get; set; }
        public int AssignedByRegistrationId { get; set; }
        public virtual Registration AssignedByRegistration { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
