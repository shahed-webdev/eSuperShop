using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "admin, sub-admin")]
    public class ApprovalInfoController : Controller
    {
        private readonly IVendorCore _vendor;
        private readonly IVendorProductCategoryCore _vendorCategory;
        private readonly IVendorSliderCore _vendorSlider;
        private readonly IProductCore _product;
        private readonly IBrandCore _brand;
        private readonly ICloudStorage _cloudStorage;

        public ApprovalInfoController(
            IVendorCore vendor,
            ICloudStorage cloudStorage,
            IVendorSliderCore vendorSlider,
            IVendorProductCategoryCore vendorCategory, 
            IProductCore product, IBrandCore brand)
        {
            _vendor = vendor;
            _cloudStorage = cloudStorage;
            _vendorSlider = vendorSlider;
            _vendorCategory = vendorCategory;
            _product = product;
            _brand = brand;
        }

        // *** profile info ***
        #region Profile Info
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

        
        // *** image Slider ***
        #region Image Slider
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


        // *** category ***
        #region Category
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


        // *** product info ***
        #region Product Info
        public IActionResult PendingProductInfo()
        {
            return View();
        }

        //get data-table
        public IActionResult GetPendingProduct(DataRequest request)
        {
            var response = _product.PendingApprovalList(request);
            return Json(response);
        }

        //details
        public IActionResult PendingProductDetails(int? id)
        {
            if (id == null) return RedirectToAction("PendingProductInfo");

            var response = _product.Details(id.GetValueOrDefault());

            ViewBag.Brands = new SelectList(_brand.CatalogWiseDdl(response.Data.CatalogInfo.CatalogId).Data, "value", "label");

            return View(response.Data);
        }


        //Approve (ajax)
        [HttpPost]
        public IActionResult UpdateProductInfo(ProductApprovedModel model)
        {
            var response = _product.ApprovedByAdmin(model);
            return Json(response);
        }

        //add new image
        [HttpPost]
        public async Task<IActionResult> AddProductImage(ProductBlobFileChangeModel model, IFormFile fileImage)
        {
            var response = await _product.BlobFileAddAsync(model, fileImage);
            return Json(response);
        }

        //delete image (ajax)
        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(ProductBlobFileChangeModel model)
        {
            var response = await _product.BlobFileDeleteAsync(model);
            return Json(response);
        }
        #endregion
    }
}
