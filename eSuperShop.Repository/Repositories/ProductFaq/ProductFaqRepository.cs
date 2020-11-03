using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using Paging.Infrastructure;
using System.Linq;

namespace eSuperShop.Repository
{
    public class ProductFaqRepository : Repository, IProductFaqRepository
    {
        public ProductFaqRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public void Add(ProductFaqAddModel model)
        {
            var faq = _mapper.Map<ProductFaq>(model);
            Db.ProductFaq.Add(faq);
        }

        public ProductFaqAnswerModel Details(int productFaqId)
        {
            return Db.ProductFaq
                .Where(p => p.ProductFaqId == productFaqId)
                .ProjectTo<ProductFaqAnswerModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public void Answer(ProductFaqAnswerModel model)
        {
            var faq = _mapper.Map<ProductFaq>(model);
            Db.ProductFaq.Update(faq);
        }

        public int TotalVisibleFaq(int productId)
        {
            return Db.ProductFaq.Count(f => f.IsVisible);
        }

        public PagedResult<FaqProductWiseViewModel> ProductWiseList(ProductReviewFilerRequest request)
        {
            return Db.ProductFaq
                .Where(f => f.IsVisible && !string.IsNullOrEmpty(f.Answer) && f.ProductId == request.ProductId)
                .ProjectTo<FaqProductWiseViewModel>(_mapper.ConfigurationProvider)
                .OrderByDescending(f => f.AnswerOnUtc)
                .GetPaged(request.Page, request.PageSize);
        }

        public PagedResult<FaqCustomerWiseViewModel> CustomerWiseList(ProductReviewFilerRequest request)
        {
            return Db.ProductFaq
                .Where(f => f.ProductId == request.ProductId)
                .ProjectTo<FaqCustomerWiseViewModel>(_mapper.ConfigurationProvider)
                .OrderByDescending(f => f.AnswerOnUtc)
                .GetPaged(request.Page, request.PageSize);
        }

        public PagedResult<FaqVendorWiseViewModel> VendorWiseList(ProductReviewFilerRequest request)
        {
            return Db.ProductFaq
                .Where(f => f.ProductId == request.ProductId)
                .ProjectTo<FaqVendorWiseViewModel>(_mapper.ConfigurationProvider)
                .GetPaged(request.Page, request.PageSize);
        }
    }
}