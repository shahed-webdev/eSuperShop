using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            //Catalog Mapping
            CreateMap<Catalog, CatalogAddModel>().ReverseMap();
            CreateMap<Catalog, CatalogModel>().MaxDepth(10).ReverseMap();
            CreateMap<Catalog, CatalogHierarchyModel>().MaxDepth(10).ReverseMap();
            CreateMap<Catalog, CatalogDisplayModel>().ReverseMap();
            //CatalogShownPlace Mapping
            CreateMap<CatalogShownPlace, CatalogAssignModel>().ReverseMap();
            CreateMap<CatalogShownPlace, CatalogDisplayModel>()
                .ForMember(d => d.CatalogName, opt => opt.MapFrom(c => c.Catalog.CatalogName))
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(c => c.Catalog.ImageUrl))
                .ForMember(d => d.SlugUrl, opt => opt.MapFrom(c => c.Catalog.SlugUrl));

        }
    }
}