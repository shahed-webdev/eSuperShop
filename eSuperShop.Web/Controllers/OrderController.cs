using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSuperShop.BusinessLogic;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;

namespace eSuperShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderCore _order;

        public OrderController(IOrderCore order)
        {
            _order = order;
        }

        //***Admin***//
        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult PendingList()
        {
            return View();
        }


        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult ConfirmedList()
        {
            return View();
        }


        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult DeliveredList()
        {
            return View();
        }

        //order details
        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult OrderDetails(int? id)
        {
            if (!id.HasValue) return RedirectToAction("PendingList");

            return View();
        }


        [Authorize(Roles = "admin, sub-admin")]
        [HttpPost]
        public IActionResult ConfirmOrder(int? id)
        {
            var response = _order.OrderPlace();
            return Json(response);
        }

        [Authorize(Roles = "admin, sub-admin")]
        [HttpPost]
        public IActionResult DeleteOrder(int? id)
        {
            return View();
        }


        //data-table
        [Authorize(Roles = "admin, sub-admin")]
        public IActionResult GetOrderListData(DataRequest request)
        {
            var response = _order.AdminWiseList(request);
            return Json(response.Data);
        }
    }
}
