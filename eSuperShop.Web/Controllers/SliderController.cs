using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Threading.Tasks;

namespace eSuperShop.Web.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Add(SliderAddModel model, IFormFile image)
        {
            var fileName = FileBuilder.FileNameImage("slider", image.FileName);
            model.ImageUrl = await _cloudStorage.UploadFileAsync(image, fileName);
            model.FileName = fileName;

            var response = _slider.Add(model, User.Identity.Name);
            return Json(response);
        }

        //delete slider
        public async Task<IActionResult> Delete(string imageUrl, int id)
        {
            var response = _slider.Delete(id);

            var uri = new Uri(imageUrl);
            var fileName = Path.GetFileName(uri.AbsolutePath);

            if (response.IsSuccess)
            {
                await _cloudStorage.DeleteFileAsync(fileName);
            }

            return Json(response);
        }

    }
}
