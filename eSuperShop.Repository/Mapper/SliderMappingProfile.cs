using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    class SliderMappingProfile : Profile
    {
        public SliderMappingProfile()
        {
            //Slider Mapping
            CreateMap<Slider, SliderAddModel>().ReverseMap();
            CreateMap<Slider, SliderSlideModel>().ReverseMap();
            CreateMap<Slider, SliderListModel>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(c => c.CreatedByRegistration.Name));
            CreateMap<SliderListModel, SliderSlideModel>().ReverseMap();
        }
    }
}
