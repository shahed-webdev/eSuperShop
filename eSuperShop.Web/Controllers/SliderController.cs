using AutoMapper;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class SliderController : Controller
    {
        private readonly SliderCore _slider;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;
        public SliderController(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
            this._slider = new SliderCore(_mapper, _db);
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
