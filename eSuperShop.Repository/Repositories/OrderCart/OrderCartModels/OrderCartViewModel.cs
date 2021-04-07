using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public class OrderCartViewModel
    {
        public OrderCartViewModel()
        {
            AttributesWithValue = new List<OrderCartAttributesSetModel>();
        }
        public int OrderCartId { get; set; }
        public bool IsSelected { get; set; }
        public int VendorId { get; set; }
        public string StoreName { get; set; }
        public string StoreSlugUrl { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantitySetId { get; set; }
        public string ProductName { get; set; }
        public string ProductSlugUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
        public List<OrderCartAttributesSetModel> AttributesWithValue { get; set; }
    }

    public class OrderCartAttributesSetModel
    {
        public string KeyName { get; set; }
        public string Value { get; set; }
    }
}