using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class SellerController : Controller
    {
        private readonly IVendorCore _vendor;
        public SellerController(IVendorCore vendor)
        {
            _vendor = vendor;
        }

        public IActionResult Login()
        {
            return View();
        }

        //seller sign-up
        public IActionResult Registration()
        {
            return View();
        }

        //send code to mobile
        [HttpPost]
        public IActionResult SendCode(string mobile, int time)
        {
            var response = _vendor.SendCode(mobile, time);
            return Json(response);
        }

        //verify code
        [HttpPost]
        public IActionResult CodeVerify(string code, string mobile) 
        {
            var response = _vendor.VerifyMobileNumber(code, mobile);
            return Json(response);
        }

        //Post registration
        [HttpPost]
        public IActionResult SignUp(VendorAddModel model)
        {
            var response = _vendor.Add(model);
            return Json(response);
        }
    }
}
