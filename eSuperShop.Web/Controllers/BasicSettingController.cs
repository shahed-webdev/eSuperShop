using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSuperShop.BusinessLogic;
using eSuperShop.Data;
using eSuperShop.Repository.Repositories;
using JqueryDataTables.LoopsIT;

namespace eSuperShop.Web.Controllers
{
    public class BasicSettingController : Controller
    {
        private readonly IRegionCore _region;

        public BasicSettingController(IRegionCore region)
        {
            _region = region;
        }


        //**** Region *****
        public IActionResult Region()
        {
            return View();
        }

        //get data table
        public IActionResult GetRegionData(DataRequest request)
        {
            var response = _region.List(request);
            return Json(response);
        }

        // insert
        [HttpPost]
        public IActionResult PostRegion(RegionAddEditModel model)
        {
            var response = _region.Add(model);
            return Json(response);
        }

        // update
        [HttpPost]
        public IActionResult UpdateRegion(RegionAddEditModel model)
        {
            var response = _region.Edit(model);
            return Json(response);
        } 
        
        // delete
        [HttpPost]
        public IActionResult DeleteRegion(int id)
        {
            var response = _region.Delete(id);
            return Json(response);
        }



        //**** Area *****
        public IActionResult Area()
        {
            return View();
        }

        //get data table
        public IActionResult GetAreaData(DataRequest request)
        {
            var response = _region.List(request);
            return Json(response);
        }

        // insert
        [HttpPost]
        public IActionResult PostArea(AreaAddEditModel model)
        {
            var response = _region.Add(model);
            return Json(response);
        }

        // update
        [HttpPost]
        public IActionResult UpdateArea(AreaAddEditModel model)
        {
            var response = _region.Edit(model);
            return Json(response);
        }

        // delete
        [HttpPost]
        public IActionResult DeleteArea(int id)
        {
            var response = _region.Delete(id);
            return Json(response);
        }
    }
}
