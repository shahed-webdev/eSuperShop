using AutoMapper;
using eSuperShop.Data;
using Paging.Infrastructure;

namespace eSuperShop.Repository
{
    public class ProductFaqRepository : Repository, IProductFaqRepository
    {
        public ProductFaqRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public void Add(ProductFaqAddModel model)
        {
            throw new System.NotImplementedException();
        }

        public ProductFaqAnswerModel Details(int productFaqId)
        {
            throw new System.NotImplementedException();
        }

        public void Answer(ProductFaqAnswerModel model)
        {
            throw new System.NotImplementedException();
        }

        public int TotalVisibleFaq(int productId)
        {
            throw new System.NotImplementedException();
        }

        public PagedResult<FaqProductWiseViewModel> ProductWiseList(ProductReviewFilerRequest request)
        {
            throw new System.NotImplementedException();
        }

        public PagedResult<FaqCustomerWiseViewModel> CustomerWiseList(ProductReviewFilerRequest request)
        {
            throw new System.NotImplementedException();
        }

        public PagedResult<FaqVendorWiseViewModel> VendorWiseList(ProductReviewFilerRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}