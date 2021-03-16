using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "Seller")]
    public class StoreController : Controller
    {
        private readonly ICloudStorage _cloudStorage;
        private readonly IVendorProductCategoryCore _category;
        private readonly IVendorSliderCore _vendorSlider;
        private readonly IVendorCore _vendor;
        private readonly IStoreCore _store;

        private readonly IRegionCore _region;
        private readonly IAreaCore _area;

        public StoreController(ICloudStorage cloudStorage, IVendorProductCategoryCore category, IVendorSliderCore vendorSlider, IVendorCore vendor, IStoreCore store, IAreaCore area, IRegionCore region)
        {
            _cloudStorage = cloudStorage;
            _category = category;
            _vendorSlider = vendorSlider;
            _vendor = vendor;
            _store = store;
            _area = area;
            _region = region;
        }

        //Profile Update
        public IActionResult ProfileUpdate()
        {
            ViewBag.Regions = new SelectList(_region.ListDdl(), "value", "label");

            var response = _vendor.ProfileDetails(User.Identity.Name);
            return View(response.Data);
        }

        //post profile
        [HttpPost]
        public async Task<IActionResult> ProfileUpdate(VendorInfoUpdateModel model, VendorInfoDocFile files)
        {
            model.StoreLogoFileName = await _cloudStorage.UpdateFileAsync(files.FileStoreLogo, model.StoreLogoFileName, "store-logo");
            model.StoreBannerFileName = await _cloudStorage.UpdateFileAsync(files.FileStoreBanner, model.StoreBannerFileName, "store-banner");
            model.NIdImageBackFileName = await _cloudStorage.UpdateFileAsync(files.FileNidPhotoBack, model.NIdImageBackFileName, "nid");
            model.NIdImageFrontFileName = await _cloudStorage.UpdateFileAsync(files.FileNidPhotoFront, model.NIdImageFrontFileName, "nid");
            model.ChequeImageFileName = await _cloudStorage.UpdateFileAsync(files.FileChequeCopy, model.ChequeImageFileName, "cheque");
            model.TradeLicenseImageFileName = await _cloudStorage.UpdateFileAsync(files.FileTradeLicense, model.TradeLicenseImageFileName, "trade-license");

            //multiple certificate insert
            if (files.FileOthersCertificate != null)
            {
                if (model.VendorCertificateFileNames != null)
                {
                    foreach (var s in model.VendorCertificateFileNames)
                    {
                        await _cloudStorage.DeleteFileAsync(FileBuilder.FileNameFromUrl(s));
                    }
                }

                var newFile = new List<string>();
                foreach (var file in files.FileOthersCertificate)
                {
                    newFile.Add(await _cloudStorage.UploadFileAsync(file, FileBuilder.FileNameImage("certificate", file.FileName)));
                }

                model.VendorCertificateFileNames = newFile.ToArray();
            }

            var response = _vendor.StoreUpdate(model, User.Identity.Name);
            return Json(response);
        }


        //get data by region
        public IActionResult GetAreaByRegion(int id)
        {
            var response = _area.GetRegionWiseArea(id);
            return Json(response);
        }


        //Image Slider
        #region Image Slider
        public IActionResult ImageSlider()
        {
            var response = _vendorSlider.List(User.Identity.Name);
            return View(response.Data);
        }

        //Add Image Slider
        public async Task<IActionResult> AddImageSlider(VendorSliderModel model, IFormFile fileImage)
        {
            var response = await _vendorSlider.AddAsync(model, User.Identity.Name, _cloudStorage, fileImage);
            return Json(response);
        }

        //Delete Image Slider
        public async Task<IActionResult> DeleteImageSlider(string fileName, int id)
        {
            var response = _vendorSlider.Delete(id);

            if (response.IsSuccess)
                await _cloudStorage.DeleteFileAsync(fileName);

            return Json(response);
        }
        #endregion


        //Add Category
        #region Add Product Category
        public IActionResult AddCategory()
        {
            var response = _category.DisplayList(User.Identity.Name);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(VendorProductCategoryAddModel model, IFormFile fileImage)
        {
            if (fileImage != null)
            {
                var fileName = FileBuilder.FileNameImage("store-product-category", fileImage.FileName);
                model.ImageFileName = await _cloudStorage.UploadFileAsync(fileImage, fileName);
            }

            var response = _category.Add(model, User.Identity.Name);

            return Json(response);
        }

        //Delete Category
        public async Task<IActionResult> DeleteCategory(string fileName, int id)
        {
            var response = _category.Delete(id);

            if (response.IsSuccess)
                await _cloudStorage.DeleteFileAsync(fileName);

            return Json(response);
        }

        //Update Category
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(VendorProductCategoryUpdateModel model, IFormFile fileImage)
        {
            if (fileImage != null)
            {
                var fileName = FileBuilder.FileNameImage("store-product-category", fileImage.FileName);
                model.ImageFileName = await _cloudStorage.UploadFileAsync(fileImage, fileName);
            }

            var response = _category.Update(model);

            return Json(response);
        }
        #endregion

        //Assign Category In Product
        public IActionResult AssignCategory()
        {
            ViewBag.StoreCategory = new SelectList(_category.ListDdl(User.Identity.Name).Data, "value", "label");
            return View();
        }

        public IActionResult GetCategory(DataRequest request, int categoryId)
        {
            var response = _category.ProductList(request, User.Identity.Name, categoryId);
            return Json(response.Data);
        }

        [HttpPost]
        public IActionResult PostAssignCategory(VendorProductCategoryAssignModel model)
        {
            var response = _category.AssignToggle(model);
            return Json(response);
        }


        //Store Theme
        [AllowAnonymous]
        public IActionResult Profile(string slugUrl)
        {
            if (string.IsNullOrEmpty(slugUrl)) return RedirectToAction("AllStores", "Product");

            var store = _store.StoreThemeDetails(slugUrl);

            if (store.IsSuccess)
                return View($"../StoreThemes/{store.Data.StoreTheme}", store.Data);

            return RedirectToAction("AllStores", "Product");
        }

        //Get Category Product
        [AllowAnonymous]
        public IActionResult GetCategoryProduct(StoreProductFilterRequest filter)
        {
            var response = _store.StoreProductList(filter);
            return Json(response);
        }
    }
}
