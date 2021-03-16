using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudStorage;
using eSuperShop.BusinessLogic;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "admin, sub-admin")]
    public class ApprovalInfoController : Controller
    {
        private readonly IVendorCore _vendor;
        private readonly IVendorProductCategoryCore _vendorCategory;
        private readonly IVendorSliderCore _vendorSlider;
        private readonly ICloudStorage _cloudStorage;

        public ApprovalInfoController(IVendorCore vendor, ICloudStorage cloudStorage, IVendorSliderCore vendorSlider, IVendorProductCategoryCore vendorCategory)
        {
            _vendor = vendor;
            _cloudStorage = cloudStorage;
            _vendorSlider = vendorSlider;
            _vendorCategory = vendorCategory;
        }

        // **** Pending profile info ****
        #region Pending Profile Info
        public IActionResult PendingProfileInfo()
        {
            return View();
        }

        //pending profile info (ajax)
        public IActionResult GetPendingProfileInfo(DataRequest request)
        {
            var response = _vendor.DataChangeUnapprovedList(request);
            return Json(response);
        }

        //Approve Profile Info (ajax)
        [HttpPost]
        public async Task<IActionResult> ApproveProfileInfo(int id)
        {
            var response = await _vendor.DataChangeApproved(id, _cloudStorage);
            return Json(response);
        }

        //Reject Profile Info (ajax)
        public IActionResult RejectProfileInfo(int id)
        {
            var response = _vendor.DataChangeReject(id, _cloudStorage);
            return Json(response);
        }
        #endregion

        
        // **** Pending image Slider ****
        #region Pending Image Slider
        public IActionResult PendingSlider()
        {
            return View();
        }

        //get data-table
        public IActionResult GetPendingSlider(DataRequest request)
        {
            var response = _vendorSlider.SliderUnapprovedList(request);
            return Json(response);
        }

        //Approve (ajax)
        [HttpPost]
        public IActionResult ApproveSlider(int id)
        {
            var response = _vendorSlider.Approved(id);
            return Json(response);
        }

        //Reject (ajax)
        public IActionResult RejectSlider(int id)
        {
            var response = _vendorSlider.Delete(id);
            return Json(response);
        }
        #endregion


        // **** Pending Category ****
        #region Pending Category
        public IActionResult PendingCategory()
        {
            return View();
        }

        //get pending list (ajax)
        public IActionResult GetPendingCategory(DataRequest request)
        {
            var response = _vendorCategory.CategoryUnapprovedList(request);
            return Json(response);
        }

        //Approve (ajax)
        [HttpPost]
        public async Task<IActionResult> ApproveCategory(int id)
        {
            var response = await _vendorCategory.Approved(id, _cloudStorage);
            return Json(response);
        }

        //Reject (ajax)
        public IActionResult RejectCategory(int id)
        {
            var response = _vendorCategory.Reject(id, _cloudStorage);
            return Json(response);
        }
        #endregion
    }
}
