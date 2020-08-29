using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //add brand
        public IActionResult AddBrand()
        {
            return View();
        }
    }
}
