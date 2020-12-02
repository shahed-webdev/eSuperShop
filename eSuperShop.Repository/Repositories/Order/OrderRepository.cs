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
            Order = _mapper.Map<Order>(model);
            Order.OrderSn = GetNewSn();
            foreach (var item in Order.OrderList)
            {
                item.CommissionPercentage = (from v in Db.VendorCatalog
                                             join p in Db.Product on new { v.CatalogId, v.VendorId } equals new { p.CatalogId, p.VendorId }
                                             where p.ProductId == item.ProductId
                                             select v.CommissionPercentage).FirstOrDefault();
            }
            Db.Order.Add(Order);
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