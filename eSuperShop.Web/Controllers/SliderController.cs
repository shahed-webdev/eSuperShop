using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class SliderController : Controller
    {
        private readonly SliderCore _slider;

        public SliderController(SliderCore sliderCore)
        {
            this._slider = sliderCore;
        }

        public IActionResult Index()
        {
            var response = _slider.List();
            return View(response.Data);
        }

        public IActionResult Add(SliderAddModel model)
        {
            var response = _slider.Add(model, User.Identity.Name);
            return View();
        }
    }
}
