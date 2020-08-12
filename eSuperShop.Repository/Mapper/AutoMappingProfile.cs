using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Slider, SliderAddModel>().ReverseMap();
            CreateMap<Slider, SliderSlideModel>().ReverseMap();
            CreateMap<Slider, SliderListModel>().ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<SliderListModel, SliderSlideModel>().ReverseMap();
        }
    }
}