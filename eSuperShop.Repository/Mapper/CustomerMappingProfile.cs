using AutoMapper;
using eSuperShop.Data;
namespace eSuperShop.Repository
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerAddressBook, CustomerAddressBookModel>().ReverseMap();
            CreateMap<CustomerAddressBook, CustomerAddressViewBookModel>()
                .ForMember(d => d.AreaName, opt => opt.MapFrom(c => c.Area.AreaName))
                .ForMember(d => d.RegionId, opt => opt.MapFrom(c => c.Area.RegionId))
                .ForMember(d => d.RegionName, opt => opt.MapFrom(c => c.Area.Region.RegionName))
                .ForMember(d => d.IsInDhaka, opt => opt.MapFrom(c => c.Area.Region.IsInDhaka))
                .ReverseMap();

            CreateMap<Customer, CustomerInfoModel>()
                .ForMember(d => d.Email, opt => opt.MapFrom(c => c.Registration.Email))
                .ForMember(d => d.UserName, opt => opt.MapFrom(c => c.Registration.UserName))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Registration.Name))
                .ReverseMap();


        }
    }
}