using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudStorage;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class AttributeController : Controller
    {
        private readonly IAttributeCore _attribute;
        private readonly ICatalogCore _catalog;
        public AttributeController(ICatalogCore catalog, IAttributeCore attribute)
        {
            _catalog = catalog;
            _attribute = attribute;
        }

        //Add attribute
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AttributeAddModel model)
        {
            var response = _attribute.Add(model, User.Identity.Name);
            return Json(response);
        }

        //Get attribute
        public IActionResult GetAttribute(DataRequest request)
        {
            var response = _attribute.List(request);
            return Json(response);
        }

        //Delete attribute
        public IActionResult DeleteAttribute(int? id)
        {
            var response = _attribute.Delete(id.GetValueOrDefault());
            return Json(response);
        }

        //Find attribute
        public async Task<IActionResult> FindAttribute(string name)
        {
            var data = await _attribute.SearchAsync(name);
            return Json(data);
        }

        //assign
        public IActionResult Assign()
        {
            var model = _catalog.List();
            return View(model.Data);
        }

        [HttpPost]
        public IActionResult Assign(AttributeAssignMultipleModel model)
        {
            var response = _attribute.AssignCatalogMultiple(model, User.Identity.Name);
            return Json(response);
        }

        public IActionResult AssignedCatalog(int? id)
        {
            if (id == null) return RedirectToAction("Add");

            var model = _catalog.AttributeWiseList(id.GetValueOrDefault());
            return View(model.Data);
        }
    }
}
