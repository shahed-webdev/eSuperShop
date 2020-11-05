using AutoMapper;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Linq;

namespace eSuperShop.Repository
{
    public class OrderRepository : Repository, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {

        }

        public Order Order { get; set; }
        public int GetNewSn()
        {
            var sn = 100000;
            if (Db.Order.Any())
            {
                sn = Db.Order.Max(s => s == null ? 0 : s.OrderSn);
            }

            return sn + 1;
        }

        public void PlaceAnOrder(OrderPlaceModel model)
        {
            var order = _mapper.Map<Order>(model);
            order.OrderSn = GetNewSn();
            Db.Order.Add(order);
        }

        public OrderReceiptModel OrderReceipt(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public void ConfirmOrder(OrderConfirmModel model)
        {
            throw new System.NotImplementedException();
        }

        public void CancelOrder(OrderCancelModel model)
        {
            throw new System.NotImplementedException();
        }

        public DataResult<OrderVendorWiseListModel> VendorWiseList(int vendorId)
        {
            throw new System.NotImplementedException();
        }

        public DataResult<OrderCustomerWistListModel> CustomerWistList(int vendorId)
        {
            throw new System.NotImplementedException();
        }
    }
}