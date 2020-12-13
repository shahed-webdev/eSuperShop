using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
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
            return Db.Order
                .Include(o => o.Customer)
                .Include(o => o.OrderShippingAddress)
                .Include(o => o.OrderList)
                .Where(o => o.OrderId == orderId)
                .ProjectTo<OrderReceiptModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public void ConfirmOrder(int orderId)
        {
            var order = Db.Order.Find(orderId);
            order.IsConfirmed = true;
            order.ConfirmedOnUtc = DateTime.UtcNow;
            Db.Order.Update(order);
        }

        public void CancelOrder(OrderCancelModel model)
        {
            throw new System.NotImplementedException();
        }

        public DataResult<OrderAdminWiseListModel> AdminWiseList(DataRequest request)
        {
            return Db.Order
                .ProjectTo<OrderAdminWiseListModel>(_mapper.ConfigurationProvider)
                .ToDataResult(request);
        }

        public DataResult<OrderVendorWiseListModel> VendorWiseList(int vendorId)
        {
            throw new System.NotImplementedException();
        }

        public DataResult<OrderCustomerWistListModel> CustomerWistList(DataRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}