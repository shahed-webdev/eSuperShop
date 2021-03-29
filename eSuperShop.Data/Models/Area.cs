using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Area
    {
        public Area()
        {
            VendorStore = new HashSet<Vendor>();
            VendorWarehouse = new HashSet<Vendor>();
            VendorReturn = new HashSet<Vendor>();
            CustomerAddress = new HashSet<CustomerAddressBook>();
            OrderShippingAddress = new HashSet<OrderShippingAddress>();
        }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Vendor> VendorStore { get; set; }
        public virtual ICollection<Vendor> VendorWarehouse { get; set; }
        public virtual ICollection<Vendor> VendorReturn { get; set; }
        public virtual ICollection<CustomerAddressBook> CustomerAddress { get; set; }
        public virtual ICollection<OrderShippingAddress> OrderShippingAddress { get; set; }
    }
}