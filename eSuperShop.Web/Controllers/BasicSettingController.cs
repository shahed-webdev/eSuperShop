using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSuperShop.Web.Controllers
{
    public class BasicSettingController : Controller
    {
        //**** Region *****
        public IActionResult Region()
        {
            return View();
        }

        // POST:
        //[HttpPost]
        //public IActionResult Create()
        //{

        //}

        //**** Area *****
        public IActionResult Area()
        {
            return View();
        }
    }
}
