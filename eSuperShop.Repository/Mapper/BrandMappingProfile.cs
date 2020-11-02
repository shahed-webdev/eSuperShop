using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {

            //Brand Mapping
            CreateMap<AllBrand, BrandAddModel>().ReverseMap();
            CreateMap<AllBrand, BrandModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<CatalogBrand, BrandAssignModel>().ReverseMap();

        }
    }
}