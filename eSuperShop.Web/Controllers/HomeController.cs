using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}