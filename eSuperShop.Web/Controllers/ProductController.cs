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
        private readonly ICustomerCore _customer;

        public ProductController(IProductCore product, IOrderCore order, ICustomerCore customer)
        {
            _product = product;
            _order = order;
            _customer = customer;
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
        public IActionResult Checkout()
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Account/CustomerLogin/?returnUrl=/Product/Checkout");

            if (!User.IsInRole("Customer"))
                return Redirect("/Home/Index");

            //var response = _customer.AddressList();

            return View();
        }

        //add shipping address
        [HttpPost]
        public IActionResult PostShippingAddress(CustomerAddressBookModel model)
        {
            var response = _customer.AddressAdd(model);
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
    }
}
