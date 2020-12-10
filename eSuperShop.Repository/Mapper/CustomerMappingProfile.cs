using AutoMapper;
using eSuperShop.Data;
namespace eSuperShop.Repository
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerAddressBook, CustomerAddressBookModel>().ReverseMap();

            CreateMap<Customer, CustomerInfoModel>()
                .ForMember(d => d.Email, opt => opt.MapFrom(c => c.Registration.Email))
                .ForMember(d => d.UserName, opt => opt.MapFrom(c => c.Registration.UserName))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Registration.Name))
                .ReverseMap();


        }
    }
}