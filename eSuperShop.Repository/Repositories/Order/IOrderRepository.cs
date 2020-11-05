using eSuperShop.Data;
using JqueryDataTables.LoopsIT;

namespace eSuperShop.Repository
{
    public interface IOrderRepository
    {
        Order Order { get; set; }
        int GetNewSn();
        void PlaceAnOrder(OrderPlaceModel model);
        OrderReceiptModel OrderReceipt(int orderId);
        void ConfirmOrder(OrderConfirmModel model);
        void CancelOrder(OrderCancelModel model);
        DataResult<OrderVendorWiseListModel> VendorWiseList(int vendorId);
        DataResult<OrderCustomerWistListModel> CustomerWistList(int vendorId);
    }
}