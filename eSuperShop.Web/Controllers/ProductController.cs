using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductCore _product;
        private readonly IOrderCore _order;

        public ProductController(IProductCore product, IOrderCore order)
        {
            _product = product;
            _order = order;
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

        //product details
        [Route("[controller]/[action]")]
        [Route("[controller]/[action]/{slugUrl}")]
        public IActionResult Item(string slugUrl)
        {
            if (string.IsNullOrEmpty(slugUrl)) return RedirectToAction("Index", "Home");

            var model = _product.DetailsBySlugUrl(slugUrl);
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

       //get available quantity
        public IActionResult GetAvailableQuantity(int quantitySetId)
        {
            var response = _product.GetQuantityBySetId(quantitySetId);
            return Json(response);
        }


        //cart product List
        [Authorize(Roles = "Customer")]
        public IActionResult Checkout()
        {
            return View();
        }


        //place order
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public IActionResult PlaceOrder(OrderPlaceModel model, string userName)
        {
            var response = _order.OrderPlace(model, userName);
            return Json(response);
        }
    }
}
