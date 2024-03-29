﻿using AutoMapper;
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
    [Authorize(Roles = "admin, sub-admin")]
    public class CategoryController : Controller
    {
        private readonly IBrandCore _brand;
        private readonly IAttributeCore _attribute;
        private readonly ISpecificationCore _specification;

        private readonly ICatalogCore _catalog;
        private readonly ICloudStorage _cloudStorage;
        private readonly ISeoCore _seo;

        public CategoryController(IBrandCore brand, IAttributeCore attribute, ISpecificationCore specification, ICloudStorage cloudStorage, ICatalogCore catalog, IMapper mapper, IUnitOfWork db)
        {
            _brand = brand;
            _attribute = attribute;
            _specification = specification;

            _catalog = catalog;
            _cloudStorage = cloudStorage;
            _seo = new SeoCoreCatalog(mapper, db);
        }

        //Products
        [AllowAnonymous]
        public IActionResult Products(string slugUrl)
        {
            if (string.IsNullOrEmpty(slugUrl)) return RedirectToAction("Index", "Home");

            ViewBag.slugUrl = slugUrl;
            return View();
        }

        //get data on page load
        [AllowAnonymous]
        public IActionResult GetProducts(string slugUrl, int pageSize)
        {
            var model = _catalog.ProductListPageData(slugUrl, pageSize);
            return Json(model.Data);
        }

        //get data on scroll
        [AllowAnonymous]
        public IActionResult GetProductsOnDemand(ProductFilterRequest filter)
        {
            var model = _catalog.GetCatalogWiseList(filter.SlugUrl, filter);
            return Json(model);
        }


        #region Category
        public IActionResult Index()
        {
            var model = _catalog.List();
            return View(model.Data);
        }


        //***Add Catalog ***

        public IActionResult Add()
        {
            ViewBag.ParentCatalog = new SelectList(_catalog.ListDdl().Data, "value", "label");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CatalogAddModel model, IFormFile fileImage)
        {
            ViewBag.ParentCatalog = new SelectList(_catalog.ListDdl().Data, "value", "label");

            var response = await _catalog.AddAsync(model, User.Identity.Name, _cloudStorage, fileImage);

            if (!response.IsSuccess)
                ModelState.AddModelError(response.FieldName, response.Message);

            if (!ModelState.IsValid) return View(model);

            return RedirectToAction("Index");
        }

        //**** Update***
        public IActionResult Update(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");

            ViewBag.ParentCatalog = new SelectList(_catalog.ListDdl().Data, "value", "label");
            var response = _catalog.Get(id.GetValueOrDefault());

            return View(response.Data);
        }

        //post update
        [HttpPost]
        public async Task<IActionResult> Update(CatalogDisplayModel model, IFormFile fileImage)
        {
            ViewBag.ParentCatalog = new SelectList(_catalog.ListDdl().Data, "value", "label");

            var response = await _catalog.EditAsync(model, _cloudStorage, fileImage);

            if (!response.IsSuccess)
                ModelState.AddModelError(response.FieldName, response.Message);

            if (!ModelState.IsValid) return View(model);

            return RedirectToAction("Index");
        }


        //**Delete Catalog**
        public async Task<IActionResult> DeleteCatalog(int id)
        {
            var response = await _catalog.DeleteAsync(id, _cloudStorage);
            return Json(response);
        }
        #endregion


        #region Assigned in Category Brand, Spacification, Attibute
        //Assigned Details
        public IActionResult AssignedDetails(int? id)
        {
            if (id == null) return RedirectToAction("index");

            var response = _catalog.AssignDetails(id.GetValueOrDefault());
            return View(response.Data);
        }

        //Un-assign Catalog
        public IActionResult UnAssignCatalog(int id, int catalogId, string type)
        {
            switch (type)
            {
                case "brand":
                    return Json(_brand.UnAssignCatalog(id, catalogId));
                case "attribute":
                    return Json(_attribute.UnAssignCatalog(id, catalogId));
                case "specification":
                    return Json(_specification.UnAssignCatalog(id, catalogId));
                default:
                    return UnprocessableEntity("unable to delete");
            }
        }
        #endregion


        #region Category Placement
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
        #endregion


        #region SEO
        //*****SEO******
        public IActionResult GetSeo(int id)
        {
            var response = _seo.Get(id);
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
        #endregion


        #region Shipping Cost
        public IActionResult ShippingCost(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");

            var model = _catalog.GetShippingCost(id.GetValueOrDefault());
            return View(model.Data);
        }

        [HttpPost]
        public IActionResult ShippingCost(CatalogShippingCostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = _catalog.ShippingCostChanged(model);

            if (!response.IsSuccess)
                return View(model);

            return RedirectToAction("Index");
        }
        #endregion
    }
}
