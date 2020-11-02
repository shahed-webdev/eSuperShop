using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class AttributeMappingProfile : Profile
    {
        public AttributeMappingProfile()
        {
            //Attribute Mapping
            CreateMap<AllAttribute, AttributeAddModel>().ReverseMap();
            CreateMap<AllAttribute, AttributeModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<CatalogAttribute, AttributeAssignModel>().ReverseMap();

        }
    }
}