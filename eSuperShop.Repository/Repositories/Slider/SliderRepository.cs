using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class SliderRepository : Repository, ISliderRepository
    {
        public SliderRepository(ApplicationDbContext db) : base(db)
        {

        }
    }
}