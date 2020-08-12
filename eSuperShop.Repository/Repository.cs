using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class Repository
    {
        protected readonly ApplicationDbContext Db;
        protected readonly IMapper _mapper;

        public Repository(ApplicationDbContext db, IMapper mapper)
        {
            Db = db;
            _mapper = mapper;
        }
    }
}