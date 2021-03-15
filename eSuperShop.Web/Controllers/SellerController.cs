using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace eSuperShop.Web.Controllers
{
    public class SellerController : Controller
    {
        private readonly IVendorCore _vendor;
        private readonly ICatalogCore _catalog;
        private readonly IRegionCore _region;
        private readonly IAreaCore _area;

        public SellerController(IVendorCore vendor, ICatalogCore catalog, IRegionCore region, IAreaCore area)
        {
            _vendor = vendor;
            _catalog = catalog;
            _region = region;
            _area = area;
        }

        //seller sign-up
        public IActionResult Registration()
        {
            ViewBag.Regions = new SelectList(_region.ListDdl(), "value", "label");
            return View();
        }

        //get area by region
        public IActionResult GetAreaByRegion(int id)
        {
            var response = _area.GetRegionWiseArea(id);
            return Json(response);
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




        /******** Un Approve list ********/
        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult List()
        {
            return View();
        }

        public IActionResult SellerList(DataRequest request)
        {
            var response = _vendor.List(request);
            return Json(response);
        }


        //Approve Vendor
        [HttpPost]
        public async Task<IActionResult> ApproveVendor(VendorApprovedModel model)
        {
            var response = await _vendor.ApprovedAsync(model, User.Identity.Name);
            return Json(response);
        }

        //Delete Vendor
        public IActionResult DeleteVendor(int id)
        {
            var response = _vendor.Delete(id);
            return Json(response);
        }

        //Assign Catalog
        public IActionResult AssignCatalog(int? id)
        {
            if (id == null) return RedirectToAction("List");
            ViewBag.VendorId = id;

            var model = _catalog.List();
            return View(model.Data);
        }

        [HttpPost]
        public IActionResult AssignCatalog(VendorCatalogAssignModel model)
        {
            var response = _vendor.AssignCatalogMultiple(model, User.Identity.Name);
            return Json(response);
        }


        //get vendor info(ajax)
        public IActionResult GetVendorInfo(int id)
        {
            var model = _vendor.GetDetails(id);
            return Json(model);
        }


        //store
        [Authorize(Roles = "Seller")]
        public IActionResult Store()
        {
            return View();
        }
    }
}
