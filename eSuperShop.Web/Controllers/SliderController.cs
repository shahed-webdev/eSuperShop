using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace eSuperShop.Web.Controllers
{
    [Authorize]
    public class SliderController : Controller
    {
        private readonly SliderCore _slider;
        private readonly ICloudStorage _cloudStorage;

        public SliderController(ICloudStorage cloudStorage, SliderCore slider)
        {
            _slider = slider;
            _cloudStorage = cloudStorage;
        }

        public IActionResult Index()
        {
            ViewBag.DisplayPlace = new SelectList(_slider.DisplayPlaceDdl(), "label", "label");
            var response = _slider.List();

            return View(response.Data);
        }

        public async Task<IActionResult> Add(SliderAddModel model, IFormFile image)
        {
            var fileName = FormFileName("slider", image.FileName);
            model.ImageUrl = await _cloudStorage.UploadFileAsync(image, fileName);
            model.FileName = fileName;

            var response = _slider.Add(model, User.Identity.Name);
            return Json(response);
        }


        private static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";

            return fileNameForStorage;
        }
    }
}
