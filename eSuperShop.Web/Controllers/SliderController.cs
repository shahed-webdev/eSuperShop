using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "admin, sub-admin")]
    public class SliderController : Controller
    {
        private readonly ISliderCore _slider;
        private readonly ICloudStorage _cloudStorage;

        public SliderController(ICloudStorage cloudStorage, ISliderCore slider)
        {
            _slider = slider;
            _cloudStorage = cloudStorage;
        }

        public IActionResult Index()
        {
            ViewBag.DisplayPlace = new SelectList(_slider.DisplayPlaceDdl(), "value", "label");
            var response = _slider.List();

            return View(response.Data);
        }

        //add slider
        [HttpPost]
        public async Task<IActionResult> Add(SliderAddModel model, IFormFile fileImage)
        {
            var response = await _slider.AddAsync(model, User.Identity.Name, _cloudStorage, fileImage);
            return Json(response);
        }

        //delete slider
        [HttpPost]
        public async Task<IActionResult> Delete(int id, string fileName)
        {
            var response = _slider.Delete(id);

            if (response.IsSuccess)
                await _cloudStorage.DeleteFileAsync(fileName);

            return Json(response);
        }

    }
}
