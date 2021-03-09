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
    [Authorize(Roles = "admin, sub-admin")]
    public class BrandController : Controller
    {
        private readonly IBrandCore _brand;
        private readonly ICloudStorage _cloudStorage;
        private readonly ICatalogCore _catalog;
        public BrandController(ICloudStorage cloudStorage, ICatalogCore catalog, IBrandCore brand)
        {
            _catalog = catalog;
            _brand = brand;
            _cloudStorage = cloudStorage;
        }
       
        //Add Brand
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandAddModel model, IFormFile logo)
        {
            if (logo == null) return UnprocessableEntity("Insert logo!");

            var fileName = FileBuilder.FileNameImage("brand-logo", logo.FileName);
            model.LogoFileName = await _cloudStorage.UploadFileAsync(logo, fileName);

            var response = _brand.Add(model, User.Identity.Name);
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

            var uri = new Uri(imageUrl);
            var fileName = Path.GetFileName(uri.AbsolutePath);

            if (response.IsSuccess)
            {
                await _cloudStorage.DeleteFileAsync(fileName);
            }

            return Json(response);
        }

        //FindBrand
        public async Task<IActionResult> FindBrand(string name)
        {
            var data = await _brand.SearchAsync(name);
            return Json(data);
        }

        //assign
        public IActionResult Assign()
        {
            var model = _catalog.List();
            return View(model.Data);
        }

        [HttpPost]
        public IActionResult Assign(BrandAssignMultipleModel model)
        {
            var response = _brand.AssignCatalogMultiple(model, User.Identity.Name);
            return Json(response);
        }

        public IActionResult AssignedCatalog(int? id)
        {
            if (id == null) return RedirectToAction("Add");

            var model = _catalog.BrandWiseList(id.GetValueOrDefault());
            return View(model.Data);
        }
    }
}
