using AutoMapper;
using eSuperShop.Data;
using eSuperShop.Repository.Repositories;

namespace eSuperShop.Repository
{
    public class AreaMappingProfile : Profile
    {
        public AreaMappingProfile()
        {
            CreateMap<Area, AreaAddEditModel>()
                .ForMember(d => d.RegionName, opt => opt.MapFrom(c => c.Region.RegionName))
                .ReverseMap();
        }
    }
}