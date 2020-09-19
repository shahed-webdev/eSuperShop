using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
   
    public class DashboardController : Controller
    {
        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Seller")]
        public IActionResult Seller()
        {
            return View();
        }
    }
}
