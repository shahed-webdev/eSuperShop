using AutoMapper;
using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Data;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace eSuperShop.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICatalogCore _catalog;
        private readonly ICloudStorage _cloudStorage;
        private readonly ISeoCore _seo;

        public CategoryController(ICloudStorage cloudStorage, ICatalogCore catalog, IMapper mapper, IUnitOfWork db)
        {
            _catalog = catalog;
            _cloudStorage = cloudStorage;
            _seo = new SeoCoreCatalog(mapper, db);
        }

        //Products
        [AllowAnonymous]
        [Route("[controller]/[action]")]
        [Route("[controller]/[action]/{slugUrl}")]
        public IActionResult Products(string slugUrl)
        {
            ViewBag.data = slugUrl;
            return View();
        }

        //Category list
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

        //Delete Catalog
        public IActionResult DeleteCatalog(int id)
        {
            var response = _catalog.Delete(id);
            return Json(response);
        }

        //Post Show placement
        public IActionResult Placement()
        {
            ViewBag.Catalog = new SelectList(_catalog.ListDdl().Data, "value", "label");
            ViewBag.ShownPlace = new SelectList(_catalog.DisplayPlaceDdl(), "value", "label");
            return View();
        }

        [HttpPost]
        public IActionResult Placement(CatalogAssignModel model)
        {
            var response = _catalog.AssignPlace(model, User.Identity.Name);
            return Json(response);
        }

        //Get Placement
        public IActionResult GetPlacement(CatalogDisplayPlace place)
        {
            var response = _catalog.GetDisplayList(place);
            return Json(response);
        }

        //Delete Placement
        public IActionResult DeletePlacement(int categoryId, CatalogDisplayPlace place)
        {
            var response = _catalog.DeletePlace(categoryId, place);
            return Json(response);
        }

        //SEO
        public IActionResult GetSeo(int id)
        {
            var response = _seo.Get(id);
            if (!response.IsSuccess) return UnprocessableEntity(response.Message);

            return Json(response);
        }

        [HttpPost]
        public IActionResult AddSeo(SeoAddModel model)
        {
            var response = _seo.Post(model, User.Identity.Name);

            if (!response.IsSuccess) return UnprocessableEntity(response.Message);

            return Json(response);
        }

        public IActionResult DeleteSeo(int id)
        {
            var response = _seo.Delete(id);
            if (!response.IsSuccess) return UnprocessableEntity(response.Message);

            return Json(response);
        }
    }
}
