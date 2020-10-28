using Paging.Infrastructure;
using System;

namespace eSuperShop.Repository
{
    public class ProductReviewAddModel
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
    }

    public class ProductReviewEditModel
    {
        public int ProductReviewId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewedOnUtc { get; set; }
    }

    public class ProductReviewViewModel
    {
        public int ProductReviewId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerUserName { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewedOnUtc { get; set; }
    }
    public class ProductReviewAverageModel
    {
        public decimal AverageRating { get; set; }
        public int TotalReview { get; set; }
        public int Star1 { get; set; }
        public int Star2 { get; set; }
        public int Star3 { get; set; }
        public int Star4 { get; set; }
        public int Star5 { get; set; }
    }

    public class ProductReviewFilerRequest : PageRequest
    {
        public int ProductId { get; set; }
    }

}