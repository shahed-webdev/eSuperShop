using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "Seller")]
    public class StoreProductController : Controller
    {
        private readonly IProductCore _product;

        public StoreProductController(IProductCore product)
        {
            _product = product;
        }

        //Find Brand
        public async Task<IActionResult> FindBrand(int catalogId, string name)
        {
            var data = await _product.SearchBrandAsync(catalogId, name);
            return Json(data);
        }

        //Find Attribute
        public async Task<IActionResult> FindAttribute(int catalogId, string name)
        {
            var data = await _product.SearchAttributeAsync(catalogId, name);
            return Json(data);
        }

        //Find Specification
        public async Task<IActionResult> FindSpecification(int catalogId, string name)
        {
            var data = await _product.SearchSpecificationAsync(catalogId, name);
            return Json(data);
        }


        public IActionResult ProductCategory()
        {
            var model = _product.VendorWiseCatalogList(User.Identity.Name);

            return View(model.Data);
        }

        //get category details
        public IActionResult CategoryDetails(int id)
        {
            var model = _product.CatalogDetails(id);
            return Json(model);
        }

        //Add product
        public IActionResult AddProduct(int? id)
        {
            if (!id.HasValue) return RedirectToAction("ProductCategory");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddModel model)
        {
            var response = await _product.AddProductAsync(model, User.Identity.Name);
            if (response.IsSuccess) return Json(response);

            return UnprocessableEntity(response.Message);
        }

        //Un Published Product
        public IActionResult UnPublishedProduct()
        {
            var response = _product.UnpublishedList(User.Identity.Name);
            return View(response.Data);
        }

        //Add stock
        public IActionResult AddProductStock(int? id)
        {
            if (id == null) return RedirectToAction("UnPublishedProduct");

            var response = _product.Details(User.Identity.Name, id.GetValueOrDefault());
            return View(response.Data);
        }

        //get stock
        [HttpPost]
        public IActionResult GetInsertedStock(ProductQuantityCheckModel model)
        {
            var response = _product.GetQuantitySet(model, User.Identity.Name);
            return Json(response);
        }

        //post
        [HttpPost]
        public IActionResult AddStock(ProductQuantityAddModel model)
        {
            var response = _product.QuantityAdd(model, User.Identity.Name);
            if (!response.IsSuccess) return UnprocessableEntity(response.Message);
            return Json(response);
        }

        //update
        [HttpPost]
        public IActionResult UpdateStock(ProductQuantityViewModel model)
        {
            var response = _product.QuantityUpdate(model);
            if (!response.IsSuccess) return UnprocessableEntity(response.Message);
            return Json(response);
        }

        //update published status
        [HttpPost]
        public IActionResult PublishedUpdate(int productId, bool isPublished)
        {
            var response = _product.PublishedUpdate(productId, isPublished, User.Identity.Name);
            return Json(response);
        }

        //Published Product
        public IActionResult PublishedProduct()
        {
            var response = _product.PublishedList(User.Identity.Name);
            return View(response.Data);
        }


        //*****SEO******
        public IActionResult GetSeo(int id)
        {
            var response = _product.Seo.Get(id);
            if (!response.IsSuccess) return UnprocessableEntity(response.Message);

            return Json(response);
        }

        [HttpPost]
        public IActionResult AddSeo(SeoAddModel model)
        {
            var response = _product.Seo.Post(model, User.Identity.Name);

            if (!response.IsSuccess) return UnprocessableEntity(response.Message);

            return Json(response);
        }

        public IActionResult DeleteSeo(int id)
        {
            var response = _product.Seo.Delete(id);
            if (!response.IsSuccess) return UnprocessableEntity(response.Message);

            return Json(response);
        }


        //store theme
        [AllowAnonymous]
        [Route("[controller]/[action]")]
        [Route("[controller]/[action]/{slugUrl}")]
        public IActionResult Theme(string slugUrl)
        {
            if (string.IsNullOrEmpty(slugUrl)) return RedirectToAction("AllStores", "Product");

            var model = _product.DetailsBySlugUrl(slugUrl);
            return View(model.Data);
        }
    }
}
