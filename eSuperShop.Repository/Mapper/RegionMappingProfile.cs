using AutoMapper;
using eSuperShop.Data;
using eSuperShop.Repository.Repositories;

namespace eSuperShop.Repository
{
    public class RegionMappingProfile : Profile
    {
        public RegionMappingProfile()
        {
            CreateMap<RegionAddEditModel, Region>().ReverseMap();
        }
    }
}