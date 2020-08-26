
using eSuperShop.BusinessLogic;
using eSuperShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderCore _slider;
        private readonly ICatalogCore _catalog;

        public HomeController(ISliderCore slider, ICatalogCore catalog)
        {
            _slider = slider;
            _catalog = catalog;
        }
        public IActionResult Index()
        {
            return View();
        }

        //get image slider
        public IActionResult GetSliderData(SliderDisplayPlace place)
        {
            var response = _slider.Display(place);
            return Json(response);
        }

        //get category
        public IActionResult GetCategory(CatalogDisplayPlace place, int numberOfData)
        {
            var response = _catalog.GetDisplayList(place, numberOfData);
            return Json(response);
        }
    }
}