using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface ICustomerCore
    {
        DbResponse Add(CustomerAddModel model);
        DbResponse AddressAdd(CustomerAddressBookModel model);
        DbResponse<List<CustomerAddressBookModel>> AddressList(int customerId);
    }
}