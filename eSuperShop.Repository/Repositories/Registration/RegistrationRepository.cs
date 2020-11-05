using AutoMapper;
using eSuperShop.Data;
using System.Linq;

namespace eSuperShop.Repository
{
    public class RegistrationRepository : Repository, IRegistrationRepository
    {
        public RegistrationRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {

        }

        public int GetRegID_ByUserName(string userName)
        {
            return Db.Registration.FirstOrDefault(r => r.UserName == userName)?.RegistrationId ?? 0;
        }

        public int VendorIdByUserName(string userName)
        {
            return Db.Vendor.FirstOrDefault(r => r.Registration.UserName == userName)?.VendorId ?? 0;
        }

        public int CustomerIdByUserName(string userName)
        {
            return Db.Customer.FirstOrDefault(r => r.Registration.UserName == userName)?.CustomerId ?? 0;
        }

        public UserType UserTypeByUserName(string userName)
        {
            return Db.Registration.FirstOrDefault(r => r.UserName == userName).Type;
        }
    }
}