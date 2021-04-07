using AutoMapper;
using eSuperShop.Repository;
using System;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class OrderCartCore : IOrderCartCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public OrderCartCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public DbResponse<int> Add(OrderCartAddModel model, string customerUserName)
        {
            try
            {
                var customerId = _db.Registration.CustomerIdByUserName(customerUserName);

                if (customerId == 0)
                    return new DbResponse<int>(false, "Invalid User");
                model.CustomerId = customerId;

                return _db.OrderCart.Add(model);

            }
            catch (Exception e)
            {
                return new DbResponse<int>(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse<int> Delete(int orderCartId)
        {
            try
            {
                if (_db.OrderCart.IsNull(orderCartId))
                    return new DbResponse<int>(false, "Item not found in the cart");


                return _db.OrderCart.Delete(orderCartId);

            }
            catch (Exception e)
            {
                return new DbResponse<int>(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse<int> QuantityChange(int orderCartId, int quantity)
        {
            try
            {
                if (_db.OrderCart.IsNull(orderCartId))
                    return new DbResponse<int>(false, "Item not found in the cart");


                return _db.OrderCart.QuantityChange(orderCartId, quantity);

            }
            catch (Exception e)
            {
                return new DbResponse<int>(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public List<OrderCartViewModel> List(string customerUserName)
        {
            var customerId = _db.Registration.CustomerIdByUserName(customerUserName);
            return _db.OrderCart.List(customerId);
        }
    }
}