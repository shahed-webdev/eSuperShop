using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IOrderCartCore
    {
        DbResponse<int> Add(OrderCartAddModel model, string customerUserName);
        DbResponse<int> Delete(int orderCartId);
        DbResponse<int> QuantityChange(int orderCartId, int quantity);
        List<OrderCartStoreWiseModel> List(string customerUserName);
        DbResponse DeleteAll(string customerUserName);
        DbResponse SelectedChange(OrderCartSelectChangeModel model);
        DbResponse SelectedAll(string customerUserName);
        DbResponse<int> OrderProductCount(string customerUserName);
    }
}