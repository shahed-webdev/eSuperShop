using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using System.Collections.Generic;
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

        public CustomerInfoModel Get(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public CustomerDashboardModel Dashboard(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public void AddressAdd(CustomerAddressBookModel model)
        {
            var address = _mapper.Map<CustomerAddressBook>(model);
            Db.CustomerAddressBook.Add(address);
        }

        public bool IsThreeAddressSaved(int customerId)
        {
            var count = Db.CustomerAddressBook.Count(a => a.CustomerId == customerId);
            return count >= 3;
        }


        public List<CustomerAddressBookModel> AddressList(int customerId)
        {
            var list = Db.CustomerAddressBook
                .Where(a => a.CustomerId == customerId)
                .ProjectTo<CustomerAddressBookModel>(_mapper.ConfigurationProvider)
                .ToList();
            return list;
        }
    }
}