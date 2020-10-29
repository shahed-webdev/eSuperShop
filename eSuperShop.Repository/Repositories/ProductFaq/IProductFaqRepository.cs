using Paging.Infrastructure;

namespace eSuperShop.Repository
{
    public interface IProductFaqRepository
    {
        void Add(ProductFaqAddModel model);
        ProductFaqAnswerModel Details(int productFaqId);
        void Answer(ProductFaqAnswerModel model);
        int TotalVisibleFaq(int productId);
        PagedResult<FaqProductWiseViewModel> ProductWiseList(ProductReviewFilerRequest request);
        PagedResult<FaqCustomerWiseViewModel> CustomerWiseList(ProductReviewFilerRequest request);
        PagedResult<FaqVendorWiseViewModel> VendorWiseList(ProductReviewFilerRequest request);

    }
}