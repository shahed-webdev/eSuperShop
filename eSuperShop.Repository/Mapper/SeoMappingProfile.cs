using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class SeoMappingProfile : Profile
    {
        public SeoMappingProfile()
        {
            //SEO Mapping
            CreateMap<Seo, SeoModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<Seo, SeoAddModel>().ReverseMap();

        }
    }
}