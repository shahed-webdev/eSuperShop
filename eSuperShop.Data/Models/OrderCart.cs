namespace eSuperShop.Data
{
    public class OrderCart
    {
        public int OrderCartId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantitySetId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public bool IsSelected { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductQuantitySet ProductQuantitySet { get; set; }
        public Customer Customer { get; set; }
    }
}