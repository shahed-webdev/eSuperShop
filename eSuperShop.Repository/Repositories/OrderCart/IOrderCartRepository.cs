using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface IOrderCartRepository
    {
        DbResponse<int> Add(OrderCartAddModel model);
        DbResponse<int> Delete(int orderCartId);
        DbResponse DeleteAll(int customerId);
        bool IsExistProduct(int productId, int productQuantitySetId, int customerId);
        DbResponse<int> QuantityChange(int orderCartId, int quantity);
        DbResponse SelectedChange(OrderCartSelectChangeModel model);
        DbResponse SelectedAll(int customerId);

        bool IsNull(int orderCartId);
        List<OrderCartStoreWiseModel> List(int customerId);
        int OrderProductCount(int customerId);
    }
}