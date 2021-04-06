using eSuperShop.Repository;

namespace eSuperShop.BusinessLogic
{
    public interface IGeneralSettingCore
    {
        DbResponse ChangeOrderQuantityLimit(int quantity);
        DbResponse<int> GetOrderQuantityLimit();
    }
}