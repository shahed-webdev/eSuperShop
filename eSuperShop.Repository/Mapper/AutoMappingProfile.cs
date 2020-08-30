using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Slider, SliderAddModel>().ReverseMap();
            CreateMap<Slider, SliderSlideModel>().ReverseMap();
            CreateMap<Slider, SliderListModel>().ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<SliderListModel, SliderSlideModel>().ReverseMap();

            CreateMap<Catalog, CatalogAddModel>().ReverseMap();
            CreateMap<Catalog, CatalogModel>().MaxDepth(10).ReverseMap();
            CreateMap<Catalog, CatalogDisplayModel>().ReverseMap();

            CreateMap<CatalogShownPlace, CatalogAssignModel>().ReverseMap();
            CreateMap<CatalogShownPlace, CatalogDisplayModel>()
                .ForMember(d => d.CatalogName, opt => opt.MapFrom(c => c.Catalog.CatalogName))
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(c => c.Catalog.ImageUrl))
                .ForMember(d => d.SlugUrl, opt => opt.MapFrom(c => c.Catalog.SlugUrl));


            CreateMap<Seo, SeoModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<Seo, SeoAddModel>().ReverseMap();
        }
    }
}