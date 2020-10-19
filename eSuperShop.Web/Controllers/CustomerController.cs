using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSuperShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles ="Customer")]
    public class CustomerController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
