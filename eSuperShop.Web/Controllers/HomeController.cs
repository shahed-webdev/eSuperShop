
using eSuperShop.BusinessLogic;
using eSuperShop.Data;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderCore _slider;
        private readonly ICatalogCore _catalog;
        private readonly IProductCore _product;
        private readonly IStoreCore _store;

        public HomeController(ISliderCore slider, ICatalogCore catalog, IProductCore product, IStoreCore store)
        {
            _slider = slider;
            _catalog = catalog;
            _product = product;
            _store = store;
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
        
        //get FlashDeals
        public IActionResult GetFlashDeals(ProductFilterRequest filter)
        {
            var response = _product.GetFlashDeals(filter);
            return Json(response);
        }

        //get TopRated
        public IActionResult GetTopRated(ProductFilterRequest filter)
        {
            var response = _product.GetTopRated(filter);
            return Json(response);
        }

        //Get TopStore
        public IActionResult GetTopStore(StoreFilterRequest filter)
        {
            var response = _store.TopStores(filter);
            return Json(response);
        }

        //Get more to Love
        public IActionResult GetMoreToLove(ProductFilterRequest filter)
        {
            var response = _product.GetMoreToLove(filter);
            return Json(response);
        }
    }
}