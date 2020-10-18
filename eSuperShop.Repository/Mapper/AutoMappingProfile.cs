using AutoMapper;
using eSuperShop.Data;
using System.Linq;

namespace eSuperShop.Repository
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            //Slider Mapping
            CreateMap<Slider, SliderAddModel>().ReverseMap();
            CreateMap<Slider, SliderSlideModel>().ReverseMap();
            CreateMap<Slider, SliderListModel>().ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<SliderListModel, SliderSlideModel>().ReverseMap();
            //Catalog Mapping
            CreateMap<Catalog, CatalogAddModel>().ReverseMap();
            CreateMap<Catalog, CatalogModel>().MaxDepth(10).ReverseMap();
            CreateMap<Catalog, CatalogDisplayModel>().ReverseMap();
            //CatalogShownPlace Mapping
            CreateMap<CatalogShownPlace, CatalogAssignModel>().ReverseMap();
            CreateMap<CatalogShownPlace, CatalogDisplayModel>()
                .ForMember(d => d.CatalogName, opt => opt.MapFrom(c => c.Catalog.CatalogName))
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(c => c.Catalog.ImageUrl))
                .ForMember(d => d.SlugUrl, opt => opt.MapFrom(c => c.Catalog.SlugUrl));

            //SEO Mapping
            CreateMap<Seo, SeoModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<Seo, SeoAddModel>().ReverseMap();

            //Attribute Mapping
            CreateMap<AllAttribute, AttributeAddModel>().ReverseMap();
            CreateMap<AllAttribute, AttributeModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<CatalogAttribute, AttributeAssignModel>().ReverseMap();

            //Brand Mapping
            CreateMap<AllBrand, BrandAddModel>().ReverseMap();
            CreateMap<AllBrand, BrandModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<CatalogBrand, BrandAssignModel>().ReverseMap();

            //Specification Mapping
            CreateMap<AllSpecification, SpecificationAddModel>().ReverseMap();
            CreateMap<AllSpecification, SpecificationModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<CatalogSpecification, SpecificationAssignModel>().ReverseMap();

            //Warehouse Mapping
            CreateMap<Warehouse, WarehouseAddModel>().ReverseMap();
            CreateMap<Warehouse, WarehouseModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<VendorWarehouse, WarehouseAssignModel>().ReverseMap();

            //Vendor Mapping
            CreateMap<Vendor, VendorAddModel>().ReverseMap();
            CreateMap<Vendor, VendorModel>().ReverseMap();
            CreateMap<Vendor, VendorInfoModel>().ReverseMap();
            CreateMap<Vendor, VendorStoreInfoUpdateModel>().ReverseMap();


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
                .ForMember(d => d.Attributes, opt => opt.MapFrom(c => c.ProductAttribute.OrderBy(p => p.Attribute.KeyName)))
                .ForMember(d => d.Blobs, opt => opt.MapFrom(c => c.ProductBlob))
                .ForMember(d => d.Specifications, opt => opt.MapFrom(c => c.ProductSpecification))
                .ForMember(d => d.BrandInfo, opt => opt.MapFrom(c => c.Brand))
                .ForMember(d => d.CatalogInfo, opt => opt.MapFrom(c => c.Catalog))
                .ForMember(d => d.VendorInfo, opt => opt.MapFrom(c => c.Vendor))
                .ForMember(d => d.QuantitySets, opt => opt.MapFrom(c => c.ProductQuantitySet));


            //Vendor Product Quantity show

            CreateMap<ProductQuantitySetAttribute, ProductQuantitySetAttributeModel>().ReverseMap();
            CreateMap<ProductQuantitySet, ProductQuantityAddModel>().ReverseMap();

            //Product Quantity Set
            CreateMap<ProductQuantitySet, ProductQuantityViewModel>().ReverseMap();
            CreateMap<ProductQuantitySet, ProductQuantitySetUpdateReturnModel>().ReverseMap();
            CreateMap<ProductQuantityViewModel, ProductQuantitySetUpdateReturnModel>().ReverseMap();
            CreateMap<ProductQuantitySet, ProductQuantitySetViewModel>()
                .ForMember(d => d.AttributesWithValue, opt => opt.MapFrom(c => c.ProductQuantitySetAttribute.OrderBy(p => p.ProductAttributeValue.ProductAttribute.Attribute.KeyName)));

            CreateMap<ProductQuantitySetAttribute, ProductQuantitySetAttributeViewModel>()
                .ForMember(d => d.AttributeId,
                    opt => opt.MapFrom(c => c.ProductAttributeValue.ProductAttribute.AttributeId))
                .ForMember(d => d.KeyName,
                    opt => opt.MapFrom(c => c.ProductAttributeValue.ProductAttribute.Attribute.KeyName))
                .ForMember(d => d.Value, opt => opt.MapFrom(c => c.ProductAttributeValue.Value));




        }
    }
}