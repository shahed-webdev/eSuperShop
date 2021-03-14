using CloudStorage;
using eSuperShop.Data;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface ISliderCore
    {
        Task<DbResponse<SliderListModel>> AddAsync(SliderAddModel model, string userName, ICloudStorage cloudStorage, IFormFile file);
        DbResponse Delete(int id);
        DbResponse<List<SliderSlideModel>> Display(SliderDisplayPlace place);
        DbResponse<List<SliderListModel>> List();
        List<DDL> DisplayPlaceDdl();
    }
}