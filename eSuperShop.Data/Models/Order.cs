using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Order
    {
        public Order()
        {
            OrderList = new HashSet<OrderList>();
        }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int OrderSn { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime ConfirmedOnUtc { get; set; }
        public DateTime DeliveredOnUtc { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDelivered { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal NetAmount { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual OrderShippingAddress OrderShippingAddress { get; set; }
        public virtual ICollection<OrderList> OrderList { get; set; }
    }
}