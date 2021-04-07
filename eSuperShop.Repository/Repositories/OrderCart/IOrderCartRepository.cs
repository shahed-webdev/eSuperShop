using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface IOrderCartRepository
    {
        DbResponse<int> Add(OrderCartAddModel model);
        DbResponse<int> Delete(int orderCartId);
        bool IsExistProduct(int productId, int productQuantitySetId, int customerId);
        DbResponse<int> QuantityChange(int orderCartId, int quantity);
        bool IsNull(int orderCartId);
        List<OrderCartViewModel> List(int customerId);
        int OrderProductCount(int customerId);
    }
}