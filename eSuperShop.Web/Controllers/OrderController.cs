using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace eSuperShop.Web.Controllers
{
    public class OrderController : Controller
    {
        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult PendingList()
        {
            return View();
        }


        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult ConfirmedList()
        {
            return View();
        }


        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult DeliveredList()
        {
            return View();
        }
    }
}
