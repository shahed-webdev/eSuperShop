using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using Paging.Infrastructure;
using System.Linq;

namespace eSuperShop.Repository
{
    public class ProductReviewRepository : Repository, IProductReviewRepository
    {
        public ProductReviewRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {

        }

        public ProductReview ProductReview { get; set; }

        public void Add(ProductReviewAddModel model)
        {
            ProductReview = _mapper.Map<ProductReview>(model);
            Db.ProductReview.Add(ProductReview);
        }

        public bool IsReviewExist(int productId, int customerId)
        {
            return Db.ProductReview.Any(p => p.ProductId == productId && p.CustomerId == customerId);
        }

        public ProductReviewEditModel Get(int productId, int customerId)
        {
            return Db.ProductReview
                .Where(p => p.ProductId == productId && p.CustomerId == customerId)
                .ProjectTo<ProductReviewEditModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public void Edit(ProductReviewEditModel model)
        {
            var review = _mapper.Map<ProductReview>(model);
            Db.ProductReview.Update(review);
        }

        public PagedResult<ProductReviewViewModel> ProductWiseList(ProductReviewFilerRequest request)
        {
            return Db.ProductReview
                .Where(p => p.ProductId == request.ProductId)
                .ProjectTo<ProductReviewViewModel>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.ReviewedOnUtc)
                .GetPaged(request.Page, request.PageSize);
        }

        public ProductReviewAverageModel AverageReview(int productId)
        {
            var review = new ProductReviewAverageModel();
            var average = Db.ProductReview
                  .Where(p => p.ProductId == productId);

            review.TotalReview = average.Count();
            review.AverageRating = (decimal)average.Sum(a => a.Rating) / (decimal)review.TotalReview;
            review.Star1 = average.Count(a => a.Rating == 1);
            review.Star2 = average.Count(a => a.Rating == 2);
            review.Star3 = average.Count(a => a.Rating == 3);
            review.Star4 = average.Count(a => a.Rating == 4);
            review.Star5 = average.Count(a => a.Rating == 5);
            return review;
        }
    }
}