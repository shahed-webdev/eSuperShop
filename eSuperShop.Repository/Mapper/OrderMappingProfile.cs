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
            CreateMap<OrderList, OrderListViewModel>()
                .ForMember(d => d.AttributeValues, opt => opt.MapFrom(c => c.ProductQuantitySet.ProductQuantitySetAttribute))
                .ForMember(d => d.ProductName, opt => opt.MapFrom(c => c.Product.Name))
                .ReverseMap();

            CreateMap<Order, OrderAdminWiseListModel>()
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(c => c.Customer.Registration.Name))
                .ForMember(d => d.CustomerVerifiedPhone, opt => opt.MapFrom(c => c.Customer.VerifiedPhone))
                .ReverseMap();


            CreateMap<Order, OrderReceiptModel>()
                .ForMember(d => d.CustomerInfo, opt => opt.MapFrom(c => c.Customer))

                ;
        }
    }
}