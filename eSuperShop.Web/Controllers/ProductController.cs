﻿using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSuperShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductCore _product;
        private readonly ICatalogCore _catalog;
        private readonly IOrderCore _order;
        private readonly ICustomerCore _customer;
        private readonly IRegionCore _region;
        private readonly IAreaCore _area;
        private readonly IGeneralSettingCore _setting;

        public ProductController(IProductCore product, IOrderCore order, ICustomerCore customer, IRegionCore region, IAreaCore area, ICatalogCore catalog, IGeneralSettingCore setting)
        {
            _product = product;
            _order = order;
            _customer = customer;
            _region = region;
            _area = area;
            _catalog = catalog;
            _setting = setting;
        }

        public IActionResult FlashDeals()
        {
            return View();
        }

        public IActionResult TopRated()
        {
            return View();
        }

        public IActionResult AllStores()
        {
            return View();
        }

        #region Product Details(item)
        //product details
        public IActionResult Item(string slugUrl)
        {
            if (string.IsNullOrEmpty(slugUrl)) return RedirectToAction("Index", "Home");

            var model = _product.DetailsBySlugUrl(slugUrl);
            ViewBag.MaxQuantityLimit = _setting.GetOrderQuantityLimit().Data;

            return View(model.Data);
        }

        //post FAQ
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public IActionResult PostFaq(ProductFaqAddModel model)
        {
            var response = _product.FaqAdd(model, User.Identity.Name);
            return Json(response);
        }

        //get stock
        [HttpPost]
        public IActionResult GetInsertedStock(ProductQuantityCheckModel model)
        {
            var response = _product.GetQuantitySet(model);
            return Json(response);
        }

        //get available quantity from cart
        public IActionResult GetAvailableQuantity(int quantitySetId)
        {
            var response = _product.GetQuantityBySetId(quantitySetId);
            return Json(response);
        }
        #endregion

        #region Cart list
        public IActionResult Cart()
        {
            return View();
        }


        #endregion

        #region Checkout
        //cart product List
        public IActionResult Checkout()
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Account/CustomerLogin/?returnUrl=/Product/Checkout");

            if (!User.IsInRole("Customer"))
                return Redirect("/Home/Index");

            ViewBag.Regions = new SelectList(_region.ListDdl(), "value", "label");
            var response = _customer.AddressList(User.Identity.Name);

            return View(response.Data);
        }

        //add shipping address
        [HttpPost]
        public IActionResult PostShippingAddress(CustomerAddressBookModel model)
        {
            var response = _customer.AddressAdd(model, User.Identity.Name);
            return Json(response);
        }

        //get area by region in shipping address
        public IActionResult GetAreaByRegion(int id)
        {
            var response = _area.GetRegionWiseArea(id);
            return Json(response);
        }

        //get shipping cost from shipping area address
        [HttpPost]
        public IActionResult GetShippingCost(ShippingCostCalculateModel model)
        {
            var response = _catalog.ShippingCostCalculate(model);
            return Json(response);
        }

        //place order
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public IActionResult PlaceOrder(OrderPlaceModel model)
        {
            var response = _order.OrderPlace(model, User.Identity.Name);
            return Json(response);
        }

        [Authorize(Roles = "Customer")]
        public IActionResult OrderSuccess(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index", "Home");
            ViewBag.OrderNo = id;

            return View();
        }
        #endregion
    }
}
