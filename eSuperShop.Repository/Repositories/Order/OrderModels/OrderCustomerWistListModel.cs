using System;

namespace eSuperShop.Repository
{
    public class OrderCustomerWistListModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerVerifiedPhone { get; set; }
        public int OrderSn { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ConfirmedOnUtc { get; set; }
        public DateTime? DeliveredOnUtc { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDelivered { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal NetAmount { get; set; }
    }
}