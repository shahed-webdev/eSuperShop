using AutoMapper;
using eSuperShop.Repository;
using System;
using System.Linq;

namespace eSuperShop.BusinessLogic
{
    public class OrderCore : IOrderCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public OrderCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public DbResponse<int> OrderPlace(OrderPlaceModel model, string customerUserName)
        {
            try
            {
                var customerId = _db.Registration.CustomerIdByUserName(customerUserName);
                if (customerId == 0) return new DbResponse<int>(false, "Invalid User");

                model.CustomerId = customerId;

                if (model.OrderList.Count <= 0)
                    return new DbResponse<int>(false, "No Product Found");


                if ((from item in model.OrderList let stock = _db.Product.GetQuantityBySetId(item.ProductQuantitySetId) where stock < item.Quantity select item).Any())
                    return new DbResponse<int>(false, "Product is stock out");



                _db.Order.PlaceAnOrder(model);

                //product stock update
                foreach (var item in model.OrderList)
                {
                    _db.Product.QuantitySale(item.ProductQuantitySetId, item.Quantity);
                }

                _db.SaveChanges();

                return new DbResponse<int>(true, "Success", _db.Order.Order.OrderId);
            }
            catch (Exception e)
            {
                return new DbResponse<int>(false, e.Message);
            }
        }
    }
}