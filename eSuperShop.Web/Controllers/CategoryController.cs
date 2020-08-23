using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSuperShop.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICatalogCore _catalog;
        private readonly ICloudStorage _cloudStorage;

        public CategoryController(ICloudStorage cloudStorage, ICatalogCore catalog)
        {
            _catalog = catalog;
            _cloudStorage = cloudStorage;
        }

        public IActionResult Index()
        {
            var model = _catalog.List();
            return View(model.Data);
        }

        //Add Catalog
        public IActionResult Add()
        {
            ViewBag.ParentCatalog = new SelectList(_catalog.ListDdl().Data, "value", "label");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CatalogAddModel model, IFormFile image)
        {
            ViewBag.ParentCatalog = new SelectList(_catalog.ListDdl().Data, "value", "label");

            if (image != null)
            {
                var fileName = FileBuilder.FileNameImage("catalog", image.FileName);
                model.ImageUrl = await _cloudStorage.UploadFileAsync(image, fileName);
            }

            var response = _catalog.Add(model, User.Identity.Name);

            if (!response.IsSuccess)
                ModelState.AddModelError(response.FieldName, response.Message);

            if (!ModelState.IsValid) return View(model);

            return RedirectToAction("Index");
        }

        //Post Show placement
        public IActionResult Placement()
        {
            ViewBag.Catalog = new SelectList(_catalog.ListDdl().Data, "value", "label");
            ViewBag.ShownPlace = new SelectList(_catalog.DisplayPlaceDdl(), "label", "label");
            return View();
        }

        [HttpPost]
        public IActionResult Placement(CatalogAssignModel model)
        {
            var response = _catalog.AssignPlace(model, User.Identity.Name);

            if (!response.IsSuccess) return UnprocessableEntity(response.Message);

            return Json(response);
        }

        //Get Placement
        //public IActionResult GetPlacement()
        //{
        //    var response = _catalog.Display();
        //    return Json(response);
        //}
    }
}
