using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface ICustomerCore
    {
        DbResponse Add(CustomerAddModel model);
        DbResponse AddressAdd(CustomerAddressBookModel model, string userName);
        DbResponse<List<CustomerAddressBookModel>> AddressList(string userName);
    }
}