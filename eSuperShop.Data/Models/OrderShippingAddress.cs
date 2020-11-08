using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class OrderShippingAddress
    {
        public OrderShippingAddress()
        {
            Order = new HashSet<Order>();
        }
        public int OrderShippingAddressId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}