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
            OrderCart cart;
            var message = "";
            if (IsExistProduct(model.ProductId, model.ProductQuantitySetId, model.CustomerId))
            {
                cart = Db.OrderCart.FirstOrDefault(o =>
                   o.ProductId == model.ProductId && o.ProductQuantitySetId == model.ProductQuantitySetId &&
                   o.CustomerId == model.CustomerId);

                if (cart != null) cart.Quantity += model.Quantity;

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
            return new DbResponse<int>(true, "Item deleted from the cart", quantity);
        }

        public DbResponse DeleteAll(int customerId)
        {
            var carts = Db.OrderCart.Where(o => o.CustomerId == customerId).ToList();
            Db.OrderCart.RemoveRange(carts);
            Db.SaveChanges();
            return new DbResponse(true, "All items deleted from the cart");
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
            return new DbResponse<int>(true, "Item quantity changed successfully", items);
        }

        public DbResponse SelectedChange(OrderCartSelectChangeModel model)
        {
            var carts = Db.OrderCart.Where(o => model.OrderCartIds.Contains(o.OrderCartId)).ToList();
            foreach (var cart in carts)
            {
                cart.IsSelected = model.IsSelected;
            }

            Db.OrderCart.UpdateRange(carts);
            Db.SaveChanges();
            return new DbResponse(true, "Item selection changed successfully");
        }

        public DbResponse SelectedAll(int customerId)
        {
            var carts = Db.OrderCart.Where(o => o.CustomerId == customerId).ToList();

            foreach (var cart in carts)
            {
                cart.IsSelected = true;
            }
            Db.OrderCart.UpdateRange(carts);
            Db.SaveChanges();
            return new DbResponse(true, "All items selected");
        }

        public bool IsNull(int orderCartId)
        {
            return !Db.OrderCart.Any(o => o.OrderCartId == orderCartId);
        }

        public List<OrderCartStoreWiseModel> List(int customerId)
        {
            var carts = Db.OrderCart.Where(o => o.CustomerId == customerId)
                .ProjectTo<OrderCartViewModel>(_mapper.ConfigurationProvider)
                .ToList();
            var storeWiseCarts = new List<OrderCartStoreWiseModel>();

            foreach (var item in carts.OrderBy(c => c.VendorId))
            {
                var cart = new OrderCartStoreWiseProductModel
                {
                    OrderCartId = item.OrderCartId,
                    IsSelected = item.IsSelected,
                    ImageFileName = item.ImageFileName,
                    ProductId = item.ProductId,
                    ProductQuantitySetId = item.ProductQuantitySetId,
                    ProductName = item.ProductName,
                    ProductSlugUrl = item.ProductSlugUrl,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    CustomerId = item.CustomerId,
                    AttributesWithValue = item.AttributesWithValue
                };
                var store = storeWiseCarts.Find(s => s.VendorId == item.VendorId);
                if (store == null)
                {
                    store = new OrderCartStoreWiseModel
                    {
                        VendorId = item.VendorId,
                        StoreName = item.StoreName,
                        StoreSlugUrl = item.StoreSlugUrl
                    };
                    storeWiseCarts.Add(store);
                }
                store.Products.Add(cart);
            }
            return storeWiseCarts;
        }

        public int OrderProductCount(int customerId)
        {
            return Db.OrderCart
                .Where(c => c.CustomerId == customerId)
                .Sum(o => o.Quantity);
        }
    }
}