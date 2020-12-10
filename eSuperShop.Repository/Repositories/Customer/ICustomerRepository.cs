using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface ICustomerRepository
    {
        void Add(CustomerAddModel model);
        CustomerInfoModel Get(int customerId);
        CustomerDashboardModel Dashboard(int customerId);
        void AddressAdd(CustomerAddressBookModel model);
        bool IsThreeAddressSaved(int customerId);
        List<CustomerAddressBookModel> AddressList(int customerId);
    }
}