namespace eSuperShop.Repository
{
    public class OrderCartAddModel
    {
        public int ProductId { get; set; }
        public int ProductQuantitySetId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}