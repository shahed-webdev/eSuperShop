using System;
using System.IO;
using eSuperShop.BusinessLogic;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CloudStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSuperShop.Web.Controllers
{

    public class StoreThemesController : Controller
    {
        //theme
        public IActionResult FullSlider()
        {
            return View();
        }

        public IActionResult HalfSlider()
        {
            return View();
        }

        public IActionResult Defaults()
        {
            return View();
        } 
        
        public IActionResult BannerBigImage()
        {
            return View();
        }
    }
}
