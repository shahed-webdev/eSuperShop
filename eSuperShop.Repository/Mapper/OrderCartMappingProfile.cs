using AutoMapper;
using eSuperShop.Data;
using System.Linq;

namespace eSuperShop.Repository
{
    public class OrderCartMappingProfile : Profile
    {
        public OrderCartMappingProfile()
        {
            CreateMap<OrderCartAddModel, OrderCart>();
            CreateMap<OrderCart, OrderCartViewModel>()
                .ForMember(d => d.AttributesWithValue, opt => opt.MapFrom(c =>
                    c.ProductQuantitySet.ProductQuantitySetAttribute.Select(s => new OrderCartAttributesSetModel
                    {
                        KeyName = s.ProductAttributeValue.Value,
                        Value = s.ProductAttributeValue.ProductAttribute.Attribute.KeyName
                    })))
                .ForMember(d => d.Price, opt => opt.MapFrom(c => c.Product.Price))
                .ForMember(d => d.ProductName, opt => opt.MapFrom(c => c.Product.Name))
                .ForMember(d => d.ProductSlugUrl, opt => opt.MapFrom(c => c.Product.SlugUrl))
                .ForMember(d => d.StoreName, opt => opt.MapFrom(c => c.Product.Vendor.StoreName))
                .ForMember(d => d.StoreSlugUrl, opt => opt.MapFrom(c => c.Product.Vendor.StoreSlugUrl))
                .ForMember(d => d.VendorId, opt => opt.MapFrom(c => c.Product.Vendor.VendorId))
                ;
        }
    }
}