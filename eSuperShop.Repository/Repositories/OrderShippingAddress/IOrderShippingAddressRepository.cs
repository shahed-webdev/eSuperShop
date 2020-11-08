using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public interface IOrderShippingAddressRepository
    {
        OrderShippingAddress OrderShippingAddress { get; set; }

        void Add(OrderShippingAddressAddModel model);


    }
}