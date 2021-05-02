using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SellerPromotionsController : Controller
    {
        public IActionResult Buy1Get1Free()
        {
            return View();
        }
    }
}
