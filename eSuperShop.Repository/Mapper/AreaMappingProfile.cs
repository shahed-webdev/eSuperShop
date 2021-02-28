using AutoMapper;
using eSuperShop.Data;
using eSuperShop.Repository.Repositories;

namespace eSuperShop.Repository
{
    public class AreaMappingProfile : Profile
    {
        public AreaMappingProfile()
        {
            CreateMap<AreaAddEditModel, Area>().ReverseMap();
        }
    }
}