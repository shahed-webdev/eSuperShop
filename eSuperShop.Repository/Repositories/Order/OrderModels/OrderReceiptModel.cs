using System;

namespace eSuperShop.Repository
{
    public class OrderReceiptModel
    {
        public int OrderId { get; set; }
        public CustomerInfoModel CustomerInfo { get; set; }
        public int OrderSn { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal NetAmount { get; set; }

        public OrderShippingAddressViewModel OrderShippingAddress { get; set; }
    }

    public class OrderShippingAddressViewModel
    {
        public int OrderShippingAddressId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AlternativePhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}