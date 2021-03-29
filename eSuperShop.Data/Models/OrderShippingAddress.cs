using System;

namespace eSuperShop.Data
{
    public class OrderShippingAddress
    {
        public int OrderShippingAddressId { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AlternativePhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public virtual Order Order { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
    }
}