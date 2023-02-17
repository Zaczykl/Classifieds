using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Classifieds.Core.ViewModels;
using Classifieds.Persistence;
using Classifieds.Persistence.Extensions;
using Classifieds.Persistence.Repositories;
using Classifieds.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Classifieds.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IClassifiedService _classifiedService;

        public HomeController(ICategoryService categoryService, IClassifiedService classifiedService)
        {
            _categoryService = categoryService;
            _classifiedService = classifiedService;
        }

        public IActionResult Index()
        { 
            var categories= _categoryService.GetCategories();
            var classifieds = _classifiedService.GetClassifieds();
            var vm = new HomePageViewModel { Categories = categories, Classifieds = classifieds };

            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();

            return View(vm);
        }

        public IActionResult FilterCategory(int id)
        {
            IEnumerable<Classified> classifieds = _classifiedService.GetFilteredClassifieds(id);
            return PartialView("_Classifieds", classifieds);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
