using AutoMapper;
using eSuperShop.Data;
using System.Linq;

namespace eSuperShop.Repository
{
    public class VendorMappingProfile : Profile
    {
        public VendorMappingProfile()
        {

            //Vendor Mapping
            CreateMap<Vendor, VendorAddModel>().ReverseMap();
            CreateMap<Vendor, VendorModel>().ReverseMap();
            CreateMap<Vendor, VendorInfoModel>().ReverseMap();
            CreateMap<Vendor, VendorStoreInfoUpdateModel>().ReverseMap();

            //Vendor store Mapping
            CreateMap<Vendor, StoreViewModel>()
                .ForMember(d => d.ProductImageUrls,
                    opt => opt.MapFrom(c => c.Product.Select(p => p.ProductBlob.First().BlobUrl).ToArray()))
                .ForMember(d => d.RatingBy, opt => opt.MapFrom(c => c.VendorReview.Count()))
                .ForMember(d => d.Rating,
                    opt => opt.MapFrom(c => (double)c.VendorReview.Sum(r => r.Rating) / (double)c.VendorReview.Count()))
                .ReverseMap();

            //Vendor Slider Mapping
            CreateMap<VendorStoreSlider, VendorSliderModel>().ReverseMap();
            CreateMap<VendorStoreSlider, VendorSliderSlideModel>().ReverseMap();

            //Vendor ProductCategory Mapping
            CreateMap<VendorProductCategory, VendorProductCategoryAddModel>().ReverseMap();
            CreateMap<VendorProductCategory, VendorProductCategoryModel>().ReverseMap();
            CreateMap<VendorProductCategory, VendorProductCategoryDisplayModel>().ReverseMap();
            CreateMap<VendorProductCategory, VendorProductCategoryUpdateModel>().ReverseMap();


            //Vendor ProductCategory Assign Mapping
            CreateMap<VendorProductCategoryList, VendorProductCategoryAssignModel>().ReverseMap();

            //Vendor Product Add
            CreateMap<ProductBlob, ProductBlobAddModel>().ReverseMap();
            CreateMap<ProductSpecification, ProductSpecificationAddModel>().ReverseMap();
            CreateMap<ProductAttributeValue, ProductAttributeValueAddModel>().ReverseMap();
            CreateMap<ProductAttribute, ProductAttributeAddModel>()
                .ForMember(d => d.Values, opt => opt.MapFrom(c => c.ProductAttributeValue))
                .ReverseMap();

            CreateMap<Product, ProductAddModel>()
                .ForMember(d => d.Attributes, opt => opt.MapFrom(c => c.ProductAttribute))
                .ForMember(d => d.Blobs, opt => opt.MapFrom(c => c.ProductBlob))
                .ForMember(d => d.Specifications, opt => opt.MapFrom(c => c.ProductSpecification))
                .ReverseMap();


            //Vendor Product show
            CreateMap<Product, ProductUnpublishedModel>()
                .ForMember(d => d.BrandName, opt => opt.MapFrom(c => c.Brand.Name))
                .ForMember(d => d.CatalogName, opt => opt.MapFrom(c => c.Catalog.CatalogName));


            CreateMap<ProductBlob, ProductBlobViewModel>().ReverseMap();
            CreateMap<ProductSpecification, ProductSpecificationViewModel>()
                .ForMember(d => d.KeyName, opt => opt.MapFrom(c => c.Specification.KeyName))
                .ReverseMap();
            CreateMap<ProductAttributeValue, ProductAttributeValueViewModel>().ReverseMap();
            CreateMap<ProductAttribute, ProductAttributeViewModel>()
                .ForMember(d => d.Values, opt => opt.MapFrom(c => c.ProductAttributeValue))
                .ForMember(d => d.KeyName, opt => opt.MapFrom(c => c.Attribute.KeyName))
                .ReverseMap();
            CreateMap<Product, ProductDetailsModel>()
                .ForMember(d => d.Attributes,
                    opt => opt.MapFrom(c => c.ProductAttribute.OrderBy(p => p.Attribute.KeyName)))
                .ForMember(d => d.Blobs, opt => opt.MapFrom(c => c.ProductBlob))
                .ForMember(d => d.Specifications, opt => opt.MapFrom(c => c.ProductSpecification))
                .ForMember(d => d.BrandInfo, opt => opt.MapFrom(c => c.Brand))
                .ForMember(d => d.CatalogInfo, opt => opt.MapFrom(c => c.Catalog))
                .ForMember(d => d.VendorInfo, opt => opt.MapFrom(c => c.Vendor))
                .ForMember(d => d.QuantitySets, opt => opt.MapFrom(c => c.ProductQuantitySet));


            //Vendor Product Quantity show Mapping

            CreateMap<ProductQuantitySetAttribute, ProductQuantitySetAttributeModel>().ReverseMap();
            CreateMap<ProductQuantitySet, ProductQuantityAddModel>().ReverseMap();

        }
    }
}