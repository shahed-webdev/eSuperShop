using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Warehouse
    {
        public Warehouse()
        {
            VendorWarehouse = new HashSet<VendorWarehouse>();
        }

        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedByRegistrationId { get; set; }

        public virtual Registration CreatedByRegistration { get; set; }
        public virtual ICollection<VendorWarehouse> VendorWarehouse { get; set; }
    }
}
