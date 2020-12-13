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
        void ConfirmOrder(int orderId);
        void CancelOrder(int orderId);
        bool IsExist(int orderId);
        DataResult<OrderAdminWiseListModel> AdminWiseList(DataRequest request);
        DataResult<OrderVendorWiseListModel> VendorWiseList(int vendorId);
        DataResult<OrderCustomerWistListModel> CustomerWistList(DataRequest request);
    }
}