using Classifieds.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Classifieds.Persistence.Extensions;
using Classifieds.Core.Models.Domains;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Classifieds.Core.Services;

namespace Classifieds.Controllers
{
    public class ClassifiedController : Controller
    {
        private ICategoryService _categoryService;
        private IClassifiedService _classifiedService;
        private IUserService _userService;
        public ClassifiedController(ICategoryService categoryService, IClassifiedService classifiedService, IUserService userService)
        {
            _categoryService = categoryService;
            _classifiedService = classifiedService;
            _userService = userService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var userId = User.GetUserId();
            var vm = new CreateClassifiedViewModel
            {
                Categories = _categoryService.GetCategories(),
                Classified = new Classified { UserId = userId },
                ContactNumber = _userService.Get(userId).PhoneNumber
            };
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClassifiedViewModel vm)
        {
            if (!ModelState.IsValid || !IsValidFile(vm.File) || !AreValidFiles(vm.Files))
            {
                return View("Create", vm);
            }

            var userId = User.GetUserId();
            vm.Classified.UserId = userId;

            await _classifiedService.AttachPhotosAsync(vm);
            _classifiedService.convertPrice(vm.Classified, vm.FormattedPrice);
            _classifiedService.Add(vm.Classified);
            _userService.UpdateContactNumber(userId, vm.ContactNumber);
            TempData["Message"] = "Ogłoszenie zostało dodane.";
            return RedirectToAction("Index", "Home");
        }

        private bool IsValidFile(IFormFile file)
        {
            if (file.Length <= 0 || !file.ContentType.StartsWith("image"))
            {
                ModelState.AddModelError("File", "Niepoprawne pliki");
                return false;
            }
            return true;
        }

        private bool AreValidFiles(IEnumerable<IFormFile> files)
        {
            if (files == null)
                return true;

            foreach (var file in files)
            {
                if (!IsValidFile(file))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
