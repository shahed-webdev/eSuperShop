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
            return View();
        }

        public IActionResult Add()
        {
            ViewBag.ParentCatalog = new SelectList(_catalog.ListDdl().Data, "value", "label");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CatalogAddModel model, IFormFile image)
        {
            ViewBag.ParentCatalog = new SelectList(_catalog.ListDdl().Data, "value", "label");

            if (!ModelState.IsValid) return View(model);

            if (image != null)
            {
                var fileName = FileBuilder.FileNameImage("catalog", image.FileName);
                model.ImageUrl = await _cloudStorage.UploadFileAsync(image, fileName);
                //model.FileName = fileName;
            }

            _catalog.Add(model, User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}
