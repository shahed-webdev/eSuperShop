using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderPlaceModel>().ReverseMap();

            CreateMap<OrderList, OrderListModel>().ReverseMap();
        }
    }
}