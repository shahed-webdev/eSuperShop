using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Mvc;

namespace eSuperShop.Web.Controllers
{
    public class SpecificationController : Controller
    {
        private readonly ISpecificationCore _specification;
        private readonly ICatalogCore _catalog;
        public SpecificationController(ISpecificationCore specification, ICatalogCore catalog)
        {
            _specification = specification;
            _catalog = catalog;
        }

        //Add Specification
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(SpecificationAddModel model)
        {
            var response = _specification.Add(model, User.Identity.Name);
            return Json(response);
        }

        //Get Specification
        public IActionResult GetSpecification(DataRequest request)
        {
            var response = _specification.List(request);
            return Json(response);
        }

        //Delete Specification
        public IActionResult DeleteSpecification(int? id)
        {
            var response = _specification.Delete(id.GetValueOrDefault());
            return Json(response);
        }

        //Find Specification
        public async Task<IActionResult> FindSpecification(string name)
        {
            var data = await _specification.SearchAsync(name);
            return Json(data);
        }

        //assign
        public IActionResult Assign()
        {
            var model = _catalog.List();
            return View(model.Data);
        }

        [HttpPost]
        public IActionResult Assign(SpecificationAssignMultipleModel model)
        {
            var response = _specification.AssignCatalogMultiple(model, User.Identity.Name);
            return Json(response);
        }

        public IActionResult AssignedCatalog(int? id)
        {
            if (id == null) return RedirectToAction("Add");

            var model = _catalog.SpecificationWiseList(id.GetValueOrDefault());
            return View(model.Data);
        }
    }
}
