using eSuperShop.Data;
using Paging.Infrastructure;

namespace eSuperShop.Repository
{
    public interface IProductReviewRepository
    {
        ProductReview ProductReview { get; set; }
        void Add(ProductReviewAddModel model);
        bool IsReviewExist(int productId, int customerId);
        ProductReviewEditModel Get(int productId, int customerId);
        void Edit(ProductReviewEditModel model);
        PagedResult<ProductReviewViewModel> ProductWiseList(ProductReviewFilerRequest request);
        ProductReviewAverageModel AverageReview(int productId);
    }


}