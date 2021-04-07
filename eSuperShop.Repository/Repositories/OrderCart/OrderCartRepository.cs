using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class OrderCartRepository : Repository, IOrderCartRepository
    {
        public OrderCartRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public DbResponse<int> Add(OrderCartAddModel model)
        {
            var cart = new OrderCart();
            var message = "";
            if (IsExistProduct(model.ProductId, model.ProductQuantitySetId, model.CustomerId))
            {
                cart = Db.OrderCart.FirstOrDefault(o =>
                   o.ProductId == model.ProductId && o.ProductQuantitySetId == model.ProductQuantitySetId &&
                   o.CustomerId == model.CustomerId);

                cart.Quantity += model.Quantity;
                message = "An item's quantity has been updated";
            }
            else
            {
                cart = _mapper.Map<OrderCart>(model);
                Db.OrderCart.Add(cart);
                message = "New item added into the Cart";
            }


            Db.SaveChanges();
            var quantity = this.OrderProductCount(model.CustomerId);
            return new DbResponse<int>(true, message, quantity);
        }

        public DbResponse<int> Delete(int orderCartId)
        {
            var cart = Db.OrderCart.Find(orderCartId);
            Db.OrderCart.Remove(cart);
            Db.SaveChanges();
            var quantity = this.OrderProductCount(cart.CustomerId);
            return new DbResponse<int>(true, "message", quantity);
        }

        public bool IsExistProduct(int productId, int productQuantitySetId, int customerId)
        {
            return Db.OrderCart.Any(o => o.ProductId == productId && o.ProductQuantitySetId == productQuantitySetId && o.CustomerId == customerId);
        }

        public DbResponse<int> QuantityChange(int orderCartId, int quantity)
        {
            var cart = Db.OrderCart.Find(orderCartId);
            cart.Quantity = quantity;
            Db.OrderCart.Update(cart);
            Db.SaveChanges();
            var items = this.OrderProductCount(cart.CustomerId);
            return new DbResponse<int>(true, "message", items);
        }

        public bool IsNull(int orderCartId)
        {
            return !Db.OrderCart.Any(o => o.OrderCartId == orderCartId);
        }

        public List<OrderCartViewModel> List(int customerId)
        {
            return Db.OrderCart.Where(o => o.CustomerId == customerId)
                .ProjectTo<OrderCartViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public int OrderProductCount(int customerId)
        {
            return Db.OrderCart
                .Where(c => c.CustomerId == customerId)
                .Sum(o => o.Quantity);
        }
    }
}