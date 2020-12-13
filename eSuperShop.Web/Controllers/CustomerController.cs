using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSuperShop.BusinessLogic;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles ="Customer")]
    public class CustomerController : Controller
    {
        private readonly IOrderCore _order;
   

        public CustomerController(IOrderCore order)
        {
            _order = order;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult OrderList()
        {
            return View();
        }

        public IActionResult GetOrderListData(DataRequest request)
        {
            var response = _order.CustomerWiseList(User.Identity.Name, request);
            return Json(response.Data);
        }
    }
}
