using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class OrderShippingAddressProfile : Profile
    {
        public OrderShippingAddressProfile()
        {
            CreateMap<OrderShippingAddress, OrderShippingAddressAddModel>().ReverseMap();
        }
    }
}