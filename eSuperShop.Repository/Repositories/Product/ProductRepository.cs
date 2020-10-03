using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public void Add(ProductAddModel model)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistSlugUrl(string slugUrl)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistSlugUrl(string slugUrl, int updateId)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNull(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool IsRelatedDataExist(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}