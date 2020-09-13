using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
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

        //Brand
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
    }
}
