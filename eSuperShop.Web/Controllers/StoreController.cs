using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "Seller")]
    public class StoreController : Controller
    {
        private readonly IVendorCore _vendor;
        private readonly IVendorSliderCore _vendorSlider;
        public StoreController(IVendorCore vendor, ICatalogCore catalog, IVendorSliderCore vendorSlider)
        {
            _vendor = vendor;
            _vendorSlider = vendorSlider;
        }

        //theme
        public IActionResult Theme()
        {
            return View();
        }

        //ImageSlider
        public IActionResult ImageSlider()
        {
           // var response = _vendorSlider.List();
            return View();
        }

        //Add Image Slider
        public IActionResult AddImageSlider(VendorSliderModel model)
        {
            return View();
        }
    }
}
