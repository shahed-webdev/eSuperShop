using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;

namespace eSuperShop.BusinessLogic
{
    public interface IOrderCore
    {
        DbResponse<int> OrderPlace(OrderPlaceModel model, string customerUserName);
        DbResponse ConfirmOrder(int orderId);
        DbResponse CancelOrder(int orderId);
        DbResponse<DataResult<OrderAdminWiseListModel>> AdminWiseList(DataRequest request);
        DbResponse<DataResult<OrderCustomerWistListModel>> CustomerWiseList(int customerId, DataRequest request);
        DbResponse<OrderReceiptModel> OrderReceipt(int orderId);

    }
}