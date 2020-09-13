using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IBrandCore _brand;
        private readonly ICloudStorage _cloudStorage;
        public ProductController(ICloudStorage cloudStorage, IBrandCore brand)
        {
            _brand = brand;
            _cloudStorage = cloudStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Add Brand
        public IActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandAddModel model, IFormFile logo)
        {
            var fileName = FileBuilder.FileNameImage("brand-logo", logo.FileName);
            model.LogoUrl = await _cloudStorage.UploadFileAsync(logo, fileName);

            var response = _brand.Add(model,User.Identity.Name);
            return Json(response);
        }

        //Get Brand
        public IActionResult GetBrand(DataRequest request)
        {
            var response = _brand.List(request);
            return Json(response);
        }

        //Delete Brand
        public async Task<IActionResult> DeleteBrand(int? id, string imageUrl)
        {
            var response = _brand.Delete(id.GetValueOrDefault());
            var fileName = Path.GetFileName(imageUrl.Substring(0, imageUrl.IndexOf("?", StringComparison.Ordinal)));

            if (response.IsSuccess)
            {
                await _cloudStorage.DeleteFileAsync(fileName);
            }

            return Json(response.IsSuccess);
        }
    }
}
