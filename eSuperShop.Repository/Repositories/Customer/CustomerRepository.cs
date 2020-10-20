using AutoMapper;
using eSuperShop.Data;
using System.Linq;

namespace eSuperShop.Repository
{
    public class CustomerRepository : Repository, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public void Add(CustomerAddModel model)
        {
            var customer = new Customer
            {
                Registration = new Registration
                {
                    UserName = model.UserName,
                    Validation = true,
                    Type = UserType.Customer,
                    Name = model.Name,
                    Email = model.Email
                }
            };
            Db.Customer.Add(customer);
        }

        public CustomerinfoModel Get(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public CustomerDashboardModel Dashboard(int customerId)
        {
            throw new System.NotImplementedException();
        }
    }
}