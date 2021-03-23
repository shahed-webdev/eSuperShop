using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "Seller")]
    public class StoreProductController : Controller
    {
        private readonly IProductCore _product;
        private readonly IBrandCore _brand;
        private readonly IAttributeCore _attribute;
        private readonly ISpecificationCore _specifications;

        public StoreProductController(IProductCore product, IBrandCore brand, IAttributeCore attribute, ISpecificationCore specifications)
        {
            _product = product;
            this._brand = brand;
            _attribute = attribute;
            _specifications = specifications;
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
        #region Add Product
        public IActionResult AddProduct(int? id)
        {
            if (!id.HasValue) return RedirectToAction("ProductCategory");

            ViewBag.Brands = new SelectList(_brand.CatalogWiseDdl(id.Value).Data, "value", "label");
            ViewBag.Attributes = new SelectList(_attribute.CatalogWiseDdl(id.Value).Data, "value", "label");
            ViewBag.Specifications = new SelectList(_specifications.CatalogWiseDdl(id.Value).Data, "value", "label");
           
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
        #endregion


        //Add stock
        #region Add Stock
        public IActionResult AddProductStock(int? id)
        {
            if (id == null) return RedirectToAction("UnPublishedProduct");

            var response = _product.DetailsForSeller(id.GetValueOrDefault());
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
        #endregion


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
        #region Product SEO
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
        #endregion
    }
}
