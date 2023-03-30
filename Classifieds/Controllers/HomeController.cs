using Classifieds.Core;
using Classifieds.Core.Services;
using Classifieds.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            var categories = _categoryService.GetCategories();
            var classifieds = _classifiedService.GetClassifieds(new FilterClassifieds { Active = true });
            var vm = new HomePageViewModel { Categories = categories, Classifieds = classifieds, SortClassifieds=new SortClassifieds()};

            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();
            return View(vm);
        }

        [HttpPost]
        public IActionResult FilterClassifieds(string title,int categoryId, string sortBy)
        {            
            var classifieds = _classifiedService.GetClassifieds(new FilterClassifieds { Title = title, categoryId = categoryId, Active = true });
            classifieds = _classifiedService.SortClassifieds(sortBy, classifieds);

            return PartialView("_Classifieds", classifieds);
        }        
    }
}
