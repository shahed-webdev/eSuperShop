using AutoMapper;
using eSuperShop.Data;

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
        }
    }
}