using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class SpecificationMappingProfile : Profile
    {
        public SpecificationMappingProfile()
        {

            //Specification Mapping
            CreateMap<AllSpecification, SpecificationAddModel>().ReverseMap();
            CreateMap<AllSpecification, SpecificationModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<CatalogSpecification, SpecificationAssignModel>().ReverseMap();

        }
    }
}