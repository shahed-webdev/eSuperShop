using System;
using System.IO;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CloudStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "Seller")]
    public class StoreThemesController : Controller
    {
        private readonly ICloudStorage _cloudStorage;
        private readonly IVendorProductCategoryCore _category;
        private readonly IVendorSliderCore _vendorSlider;
        private readonly IVendorCore _vendor;

        public StoreThemesController(ICloudStorage cloudStorage, IVendorProductCategoryCore category, IVendorSliderCore vendorSlider, IVendorCore vendor)
        {
            _cloudStorage = cloudStorage;
            _category = category;
            _vendorSlider = vendorSlider;
            _vendor = vendor;
        }

        //theme
        public IActionResult FullSlider()
        {
            return View();
        }

        public IActionResult HalfSlider()
        {
            return View();
        }

        public IActionResult Defaults()
        {
            return View();
        } 
        
        public IActionResult BannerBigImage()
        {
            return View();
        }
    }
}
