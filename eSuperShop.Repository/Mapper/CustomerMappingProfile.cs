using AutoMapper;
using eSuperShop.Data;
namespace eSuperShop.Repository
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerAddressBook, CustomerAddressBookModel>().ReverseMap();
        }
    }
}