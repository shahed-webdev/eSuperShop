using eSuperShop.Repository;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface ICustomerCore
    {
        DbResponse Add(CustomerAddModel model);
        DbResponse AddressAdd(CustomerAddressBookModel model, string userName);
        DbResponse<List<CustomerAddressViewBookModel>> AddressList(string userName);

        DbResponse SendCode(string mobileNumber, int codeValidSecond);
        Task<DbResponse<IdentityUser>> MobileSignUpAsync(CustomerMobileSignUpModel model);
    }

    public class CustomerMobileSignUpModel
    {
        public string Password { get; set; }
        public string Code { get; set; }
        public string MobileNumber { get; set; }
    }
}