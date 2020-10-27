namespace eSuperShop.Repository
{
    public class ProductReviewAddModel
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
    }
}