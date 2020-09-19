using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSuperShop.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IVendorCore _vendor;
        public StoreController(IVendorCore vendor, ICatalogCore catalog)
        {
            _vendor = vendor;
        }

        //theme
        public IActionResult Theme()
        {
            return View();
        }
    }
}
