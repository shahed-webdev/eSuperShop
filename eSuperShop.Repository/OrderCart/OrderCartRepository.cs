using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class OrderCartRepository : Repository, IOrderCartRepository
    {
        public OrderCartRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }
    }
}