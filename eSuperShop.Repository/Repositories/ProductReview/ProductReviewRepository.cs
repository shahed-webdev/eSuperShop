using AutoMapper;
using eSuperShop.Data;
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
    }
}