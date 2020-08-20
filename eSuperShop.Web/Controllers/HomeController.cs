
using eSuperShop.BusinessLogic;
using eSuperShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderCore _slider;

        public HomeController(ISliderCore slider)
        {
            _slider = slider;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetSliderData(SliderDisplayPlace place)
        {
            var response = _slider.Display(place);
            return Json(response);
        }
    }
}