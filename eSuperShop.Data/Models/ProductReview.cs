using System;

namespace eSuperShop.Data
{
    public class ProductReview
    {
        public int ProductReviewId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewedOnUtc { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
