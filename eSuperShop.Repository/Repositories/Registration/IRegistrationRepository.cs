using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public interface IRegistrationRepository
    {
        int GetRegID_ByUserName(string userName);
        int VendorIdByUserName(string userName);
        int CustomerIdByUserName(string userName);
        UserType UserTypeByUserName(string userName);
    }
}