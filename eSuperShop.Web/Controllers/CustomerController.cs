using eSuperShop.BusinessLogic;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly IOrderCore _order;
        private readonly ICustomerCore _customer;


        public CustomerController(IOrderCore order, ICustomerCore customer)
        {
            _order = order;
            _customer = customer;
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

        //Send Code To Mobile
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SendCode(string mobile, int time)
        {
            var response = _customer.SendCode(mobile, time);
            return Json(response);
        }


        //POST: Customer Registration
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CustomerRegistration(CustomerMobileSignUpModel model)
        {
            var response = await _customer.MobileSignUpAsync(model);
            return Json(response);
        }
    }
}
