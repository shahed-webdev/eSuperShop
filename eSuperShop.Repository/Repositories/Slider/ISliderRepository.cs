using eSuperShop.Data;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface ISliderRepository
    {
        Slider Slider { get; set; }
        void Add(SliderAddModel model);
        void Delete(int id);
        bool IsNull(int id);
        List<SliderSlideModel> Display(SliderDisplayPlace place);
        List<SliderListModel> List();
        List<DDL> SliderPlaceDdl();

    }
}