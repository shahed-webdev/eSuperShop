using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public interface IRegistrationRepository
    {
        int GetRegID_ByUserName(string userName);
        UserType UserTypeByUserName(string userName);
    }
}