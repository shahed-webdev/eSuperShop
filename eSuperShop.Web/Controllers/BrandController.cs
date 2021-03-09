using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> AddBrand(BrandAddModel model, IFormFile fileLogo)
        {
            if (fileLogo == null) return UnprocessableEntity("Insert logo!");

            var response = await _brand.AddAsync(model, User.Identity.Name, _cloudStorage, fileLogo);
            return Json(response);
        }

        //Get Brand
        public IActionResult GetBrand(DataRequest request)
        {
            var response = _brand.List(request);
            return Json(response);
        }


        //Update Brand
        public IActionResult UpdateBrand(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Add");

            var response = _brand.Get(id.GetValueOrDefault());
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBrand(BrandEditModel model, IFormFile fileLogo)
        {


            var response = await _brand.EditAsync(model, fileLogo, _cloudStorage);
            return Json(response);
        }


        //Delete Brand
        public async Task<IActionResult> DeleteBrand(int? id, string fileName)
        {
            var response = _brand.Delete(id.GetValueOrDefault());

            if (response.IsSuccess && !string.IsNullOrEmpty(fileName))
                await _cloudStorage.DeleteFileAsync(fileName);

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
