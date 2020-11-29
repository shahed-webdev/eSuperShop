using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public class OrderPlaceModel
    {
        public OrderPlaceModel()
        {
            OrderList = new HashSet<OrderListModel>();
        }
        public int CustomerId { get; set; }
        public string PaymentMethod { get; set; }
        public OrderShippingAddressAddModel OrderShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public ICollection<OrderListModel> OrderList { get; set; }
    }

    public class OrderListModel
    {
        public int ProductId { get; set; }
        public int ProductQuantitySetId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}