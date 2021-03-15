using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using CloudStorage;

namespace eSuperShop.Web.Controllers
{
    public class SellerController : Controller
    {
        private readonly IVendorCore _vendor;
        private readonly ICatalogCore _catalog;
        private readonly IRegionCore _region;
        private readonly IAreaCore _area;
        private readonly ICloudStorage _cloudStorage;

        public SellerController(IVendorCore vendor, ICatalogCore catalog, IRegionCore region, IAreaCore area, ICloudStorage cloudStorage)
        {
            _vendor = vendor;
            _catalog = catalog;
            _region = region;
            _area = area;
            _cloudStorage = cloudStorage;
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

        //get data-table
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


        //get vendor info in Assign Catalog (ajax)
        public IActionResult GetVendorInfo(int id)
        {
            var model = _vendor.GetDetails(id);
            return Json(model);
        }


        //seller profile
        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult ProfileDetails(int? id)
        {
            if (!id.HasValue) return RedirectToAction("List");

            ViewBag.Regions = new SelectList(_region.ListDdl(), "value", "label");
            var response = _vendor.ProfileDetails(id.GetValueOrDefault());

            return View(response.Data);
        }

        //post update profile
        [HttpPost]
        public IActionResult ProfileUpdate(VendorInfoUpdateByAdminModel model)
        {
            var response = _vendor.StoreUpdateByAdmin(model);
            return Json(response);
        }


        //seller pending profile info
        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult PendingProfileInfo()
        {
            return View();
        }

        //pending profile info
        public IActionResult GetPendingProfileInfo(DataRequest request)
        {
            var response = _vendor.DataChangeUnapprovedList(request);
            return Json(response);
        }

        //Approve Profile Info
        [HttpPost]
        public async Task<IActionResult> ApproveProfileInfo(int id)
        {
            var response = await _vendor.DataChangeApproved(id, _cloudStorage);
            return Json(response);
        }

        //Reject Profile Info
        public IActionResult RejectProfileInfo(int id)
        {
            var response = _vendor.DataChangeReject(id,_cloudStorage);
            return Json(response);
        }



        //store
        [Authorize(Roles = "Seller")]
        public IActionResult Store()
        {
            return View();
        }
    }
}
