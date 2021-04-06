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
    [Authorize(Roles = "admin, sub-admin")]
    public class OrderController : Controller
    {
        private readonly IOrderCore _order;
        private readonly IGeneralSettingCore _setting;

        public OrderController(IOrderCore order, IGeneralSettingCore setting)
        {
            _order = order;
            _setting = setting;
        }

        //***Admin***//
        public IActionResult PendingList()
        {
            return View();
        }


        public IActionResult ConfirmedList()
        {
            return View();
        }


        public IActionResult DeliveredList()
        {
            return View();
        }

        //order details
        public IActionResult OrderDetails(int? id)
        {
            if (!id.HasValue) return RedirectToAction("PendingList");

            var response = _order.OrderReceipt(id.GetValueOrDefault());
            return View(response.Data);
        }


        [HttpPost]
        public IActionResult ConfirmOrder(int? id)
        {
            var response = _order.ConfirmOrder(id.GetValueOrDefault());
            return Json(response);
        }


        [HttpPost]
        public IActionResult DeleteOrder(int? id)
        {
            var response = _order.CancelOrder(id.GetValueOrDefault());
            return Json(response);
        }


        //data-table
        public IActionResult GetOrderListData(DataRequest request)
        {
            var response = _order.AdminWiseList(request);
            return Json(response.Data);
        }

        #region Order Setting
        //order setting
        public IActionResult OrderSettings()
        {
            var model = _setting.GetOrderQuantityLimit();
            return View(model.Data);
        }

        [HttpPost]
        public IActionResult OrderSettings(int quantity)
        {
            var model = _setting.ChangeOrderQuantityLimit(quantity);

            if (model.IsSuccess)
                return RedirectToAction("OrderSettings");

            ViewBag.ErrorMessage = model.Message;
            return View(quantity);
        }

        #endregion
    }
}
