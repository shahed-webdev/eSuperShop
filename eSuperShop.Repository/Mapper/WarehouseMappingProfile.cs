using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {

            //Warehouse Mapping
            CreateMap<Warehouse, WarehouseAddModel>().ReverseMap();
            CreateMap<Warehouse, WarehouseModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<VendorWarehouse, WarehouseAssignModel>().ReverseMap();

        }
    }
}