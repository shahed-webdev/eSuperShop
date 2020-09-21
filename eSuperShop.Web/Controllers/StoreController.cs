using System;
using System.IO;
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
        private readonly ICloudStorage _cloudStorage;
        private readonly IVendorProductCategoryCore _category;
        private readonly IVendorSliderCore _vendorSlider;

        public StoreController(ICloudStorage cloudStorage, IVendorProductCategoryCore category, IVendorSliderCore vendorSlider)
        {
            _cloudStorage = cloudStorage;
            _category = category;
            _vendorSlider = vendorSlider;
        }

        //theme
        public IActionResult Theme()
        {
            return View();
        }

        //Image Slider
        public IActionResult ImageSlider()
        {
            var response = _vendorSlider.List(User.Identity.Name);
            return View(response.Data);
        }

        //Add Image Slider
        public async Task<IActionResult> AddImageSlider(VendorSliderModel model, IFormFile image)
        {
            var fileName = FileBuilder.FileNameImage("store", image.FileName);
            model.ImageUrl = await _cloudStorage.UploadFileAsync(image, fileName);
  
            var response = _vendorSlider.Add(model,User.Identity.Name);
            return Json(response);
        }

        //Delete Image Slider
        public async Task<IActionResult> DeleteImageSlider(string imageUrl, int id)
        {
            var response = _vendorSlider.Delete(id);
            var uri = new Uri(imageUrl);
            var fileName = Path.GetFileName(uri.AbsolutePath);

            if (response.IsSuccess)
                await _cloudStorage.DeleteFileAsync(fileName);
            
            return Json(response);
        }

        //Add Category
        public IActionResult AddCategory()
        {
            var response = _category.DisplayList(User.Identity.Name);
            return View(response.Data);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddCategory(VendorProductCategoryAddModel model, IFormFile image)
        {
            if (image != null)
            {
                var fileName = FileBuilder.FileNameImage("product-category", image.FileName);
                model.ImageUrl = await _cloudStorage.UploadFileAsync(image, fileName);
            }

            var response = _category.Add(model, User.Identity.Name);

            return Json(response);
        }

        //Delete category
        public async Task<IActionResult> DeleteCategory(string imageUrl, int id)
        {
            var response = _category.Delete(id);

            if (string.IsNullOrEmpty(imageUrl)) return Json(response);

            if (!response.IsSuccess) return Json(response);

            var uri = new Uri(imageUrl);
            var fileName = Path.GetFileName(uri.AbsolutePath);
            await _cloudStorage.DeleteFileAsync(fileName);

            return Json(response);
        }

        //Update Store
        public IActionResult UpdateStore()
        {
            var response = _category.DisplayList(User.Identity.Name);
            return View(response.Data);
        }
    }
}
