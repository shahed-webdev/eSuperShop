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

        public ProductController(IProductCore product)
        {
            _product = product;
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

        //get stock
        [HttpPost]
        public IActionResult GetInsertedStock(ProductQuantityCheckModel model)
        {
            var response = _product.GetQuantitySet(model, User.Identity.Name);
            return Json(response);
        }
    }
}
