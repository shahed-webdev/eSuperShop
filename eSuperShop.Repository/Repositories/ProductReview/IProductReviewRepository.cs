using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public interface IProductReviewRepository
    {
        ProductReview ProductReview { get; set; }
        void Add(ProductReviewAddModel model);
        bool IsReviewExist(int productId, int customerId);
    }
}