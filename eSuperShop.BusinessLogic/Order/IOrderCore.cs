using eSuperShop.Repository;

namespace eSuperShop.BusinessLogic
{
    public interface IOrderCore
    {
        DbResponse<int> OrderPlace(OrderPlaceModel model, string customerUserName);
    }
}