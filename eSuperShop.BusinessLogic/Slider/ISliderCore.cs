using eSuperShop.Data;
using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface ISliderCore
    {
        DbResponse<SliderListModel> Add(SliderAddModel model, string userName);
        DbResponse Delete(int id);
        DbResponse<List<SliderSlideModel>> Display(SliderDisplayPlace place);
        DbResponse<List<SliderListModel>> List();
        List<DDL> DisplayPlaceDdl();
    }
}