using eSuperShop.Repository;

namespace eSuperShop.BusinessLogic
{
    public interface ICustomerCore
    {
        DbResponse Add(CustomerAddModel model);

    }
}