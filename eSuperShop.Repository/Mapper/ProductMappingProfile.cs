using AutoMapper;
using eSuperShop.Data;
using System.Linq;

namespace eSuperShop.Repository
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            //Product Quantity Set Mapping
            CreateMap<ProductQuantitySet, ProductQuantityViewModel>().ReverseMap();
            CreateMap<ProductQuantitySet, ProductQuantitySetUpdateReturnModel>().ReverseMap();
            CreateMap<ProductQuantityViewModel, ProductQuantitySetUpdateReturnModel>().ReverseMap();
            CreateMap<ProductQuantitySet, ProductQuantitySetViewModel>()
                .ForMember(d => d.AttributesWithValue,
                    opt => opt.MapFrom(c =>
                        c.ProductQuantitySetAttribute.OrderBy(p =>
                            p.ProductAttributeValue.ProductAttribute.Attribute.KeyName)));

            CreateMap<ProductQuantitySetAttribute, ProductQuantitySetAttributeViewModel>()
                .ForMember(d => d.AttributeId,
                    opt => opt.MapFrom(c => c.ProductAttributeValue.ProductAttribute.AttributeId))
                .ForMember(d => d.KeyName,
                    opt => opt.MapFrom(c => c.ProductAttributeValue.ProductAttribute.Attribute.KeyName))
                .ForMember(d => d.Value, opt => opt.MapFrom(c => c.ProductAttributeValue.Value));

            //Product Show Mapping
            CreateMap<Product, ProductListViewModel>()
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(c => c.ProductBlob.FirstOrDefault().BlobUrl))
                .ForMember(d => d.RatingBy, opt => opt.MapFrom(c => c.ProductReview.Count()))
                .ForMember(d => d.Rating, opt => opt.MapFrom(c => (double)c.ProductReview.Sum(r => r.Rating) / (double)c.ProductReview.Count()))
                .ForMember(d => d.StoreName, opt => opt.MapFrom(c => c.Vendor.StoreName))
                .ReverseMap();

            CreateMap<Product, ProductShortInfo>()
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(c => c.ProductBlob.FirstOrDefault().BlobUrl))
                .ReverseMap();

            // ProductReview Mapping
            CreateMap<ProductReview, ProductReviewAddModel>().ReverseMap();
            CreateMap<ProductReview, ProductReviewEditModel>().ReverseMap();
            CreateMap<ProductReview, ProductReviewViewModel>()
                .ForMember(d => d.CustomerUserName, opt => opt.MapFrom(c => c.Customer.Registration.UserName))
                .ReverseMap();

            //Product Faq Mapping
            CreateMap<ProductFaq, ProductFaqAddModel>().ReverseMap();
            CreateMap<ProductFaq, ProductFaqAnswerModel>()
                .ForMember(d => d.CustomerUserName, opt => opt.MapFrom(c => c.Customer.Registration.UserName))
                .ReverseMap();
            CreateMap<ProductFaq, FaqCustomerWiseViewModel>().ReverseMap();
            CreateMap<ProductFaq, FaqProductWiseViewModel>()
                .ForMember(d => d.CustomerUserName, opt => opt.MapFrom(c => c.Customer.Registration.UserName))
                .ReverseMap();
            CreateMap<ProductFaq, FaqVendorWiseViewModel>()
                .ForMember(d => d.CustomerUserName, opt => opt.MapFrom(c => c.Customer.Registration.UserName))
                .ReverseMap();

        }
    }
}