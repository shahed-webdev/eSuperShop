using eSuperShop.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eSuperShop.Web.Controllers
{
    public class StoreProductController : Controller
    {
        private readonly IProductCore _Product;


        public StoreProductController(IProductCore product)
        {
            _Product = product;
        }

        //FindBrand
        public async Task<IActionResult> FindBrand(int catalogId, string name)
        {
            var data = await _Product.SearchBrandAsync(catalogId, name);
            return Json(data);
        }

        public IActionResult ProductCategory()
        {
            var model = _Product.VendorWiseCatalogList(User.Identity.Name);

            return View(model.Data);
        }

        public IActionResult AddProduct(int? id)
        {
            if (!id.HasValue) return RedirectToAction("ProductCategory");

            return View();
        }
    }
}
