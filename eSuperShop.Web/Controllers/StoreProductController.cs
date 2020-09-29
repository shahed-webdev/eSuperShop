using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class StoreProductController : Controller
    {
        private readonly ICatalogCore _catalog;
        private readonly IUnitOfWork _db;
        private readonly IBrandCore _brand;

        public StoreProductController(ICatalogCore catalog, IUnitOfWork db, IBrandCore brand)
        {
            _catalog = catalog;
            _db = db;
            _brand = brand;
        }

        //FindBrand
        public async Task<IActionResult> FindBrand(string name)
        {
            var data = await _brand.SearchAsync(name);
            return Json(data);
        }

        public IActionResult ProductCategory()
        {
            var id = _db.Registration.VendorIdByUserName(User.Identity.Name);
            var model = _catalog.VendorWiseList(id);

            return View(model.Data);
        }

        public IActionResult AddProduct(int? id)
        {
            if (!id.HasValue) return RedirectToAction("ProductCategory");

            return View();
        }
    }
}
