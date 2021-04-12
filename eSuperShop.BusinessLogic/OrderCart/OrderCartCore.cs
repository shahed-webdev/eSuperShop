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

        public List<OrderCartStoreWiseModel> List(string customerUserName)
        {
            var customerId = _db.Registration.CustomerIdByUserName(customerUserName);
            return _db.OrderCart.List(customerId);
        }

        public DbResponse DeleteAll(string customerUserName)
        {
            try
            {
                var customerId = _db.Registration.CustomerIdByUserName(customerUserName);

                if (customerId == 0)
                    return new DbResponse(false, "Invalid User");


                return _db.OrderCart.DeleteAll(customerId);

            }
            catch (Exception e)
            {
                return new DbResponse(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse SelectedChange(OrderCartSelectChangeModel model)
        {
            try
            {
                return _db.OrderCart.SelectedChange(model);

            }
            catch (Exception e)
            {
                return new DbResponse(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse<int> OrderProductCount(string customerUserName)
        {
            try
            {
                var customerId = _db.Registration.CustomerIdByUserName(customerUserName);

                if (customerId == 0)
                    return new DbResponse<int>(false, "Invalid User");
                var data = _db.OrderCart.OrderProductCount(customerId);
                return new DbResponse<int>(true, "Success", data);

            }
            catch (Exception e)
            {
                return new DbResponse<int>(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse SelectedAll(string customerUserName)
        {
            try
            {
                var customerId = _db.Registration.CustomerIdByUserName(customerUserName);

                if (customerId == 0)
                    return new DbResponse(false, "Invalid User");
                return _db.OrderCart.SelectedAll(customerId);

            }
            catch (Exception e)
            {
                return new DbResponse(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }
    }
}