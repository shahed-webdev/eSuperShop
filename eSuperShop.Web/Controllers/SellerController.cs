using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class SellerController : Controller
    {
        private readonly IVendorCore _vendor;
        private readonly ICatalogCore _catalog;
        public SellerController(IVendorCore vendor, ICatalogCore catalog)
        {
            _vendor = vendor;
            _catalog = catalog;
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

        //Success Message
        public IActionResult Success()
        {
            return View();
        }


        /********Un Approve list********/
        [Authorize]
        public IActionResult UnApprovedList()
        {
            return View();
        }

        public IActionResult PendingSeller(DataRequest request)
        {
           var response= _vendor.List(request);
            return Json(response);
        }

        //Assign Catalog
        public IActionResult AssignCatalog()
        {
            var model = _catalog.List();
            return View(model.Data);
        }

        [HttpPost]
        public IActionResult AssignCatalog(VendorCatalogAssignModel model)
        {
            var response = _vendor.AssignCatalogMultiple(model, User.Identity.Name);
            return Json(response);
        }
    }
}
