using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CloudStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "Seller")]
    public class StoreController : Controller
    {
        private readonly IVendorCore _vendor;
        private readonly IVendorSliderCore _vendorSlider;
        private readonly ICloudStorage _cloudStorage;

        public StoreController(ICloudStorage cloudStorage, IVendorCore vendor, ICatalogCore catalog, IVendorSliderCore vendorSlider)
        {
            _vendor = vendor;
            _vendorSlider = vendorSlider;
            _cloudStorage = cloudStorage;
        }

        //theme
        public IActionResult Theme()
        {
            return View();
        }

        //Image Slider
        public IActionResult ImageSlider()
        {
           // var response = _vendorSlider.List();
            return View();
        }

        //Add Image Slider
        public async Task<IActionResult> AddImageSlider(VendorSliderModel model, IFormFile image)
        {
            var fileName = FileBuilder.FileNameImage("slider", image.FileName);
            model.ImageUrl = await _cloudStorage.UploadFileAsync(image, fileName);
            //model.VendorId = 
            var response = _vendorSlider.Add(model);
            return Json(response);
        }
    }
}
