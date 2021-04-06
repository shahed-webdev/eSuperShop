using AutoMapper;
using eSuperShop.Data;
using System.Linq;

namespace eSuperShop.Repository
{
    public class GeneralSettingRepository : Repository, IGeneralSettingRepository
    {
        public GeneralSettingRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }


        public DbResponse ChangeOrderQuantityLimit(int quantity)
        {
            var setting = Db.GeneralSetting.First();
            if (setting == null)
                return new DbResponse(false, "No data Found");

            setting.OrderQuantityLimit = quantity;
            Db.GeneralSetting.Update(setting);
            Db.SaveChanges();

            return new DbResponse(true, "Successfully Changed");
        }

        public DbResponse<int> GetOrderQuantityLimit()
        {
            var setting = Db.GeneralSetting.First();
            return setting == null ?
                new DbResponse<int>(false, "No data Found") :
                new DbResponse<int>(true, "Success", setting.OrderQuantityLimit);
        }
    }
}