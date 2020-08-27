using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class SellerController : Controller
    {
        public IActionResult Login()
        {
            return View();
        } 
        
        public IActionResult Registration()
        {
            return View();
        }
    }
}
