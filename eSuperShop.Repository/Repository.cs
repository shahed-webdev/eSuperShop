using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class Repository
    {
        protected readonly ApplicationDbContext Db;

        public Repository(ApplicationDbContext db)
        {
            Db = db;
        }
    }
}