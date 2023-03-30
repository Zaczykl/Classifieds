using Classifieds.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Classifieds.Persistence.Extensions;
using Classifieds.Core.Models.Domains;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Classifieds.Core.Services;
using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Classifieds.Controllers
{
    public class ClassifiedController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IClassifiedService _classifiedService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public ClassifiedController(
            ICategoryService categoryService, 
            IClassifiedService classifiedService, 
            IUserService userService, 
            ILogger<ClassifiedController> logger)
        {
            _categoryService = categoryService;
            _classifiedService = classifiedService;
            _userService = userService;
            _logger = logger;
        }

        public IActionResult Display(int classifiedId)
        {
            try
            {
                var classified = _classifiedService.GetClassified(classifiedId);

                if (TempData["Message"] != null)
                    ViewBag.Message = TempData["Message"].ToString();

                return View(classified);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Deleted");
            }
        }

        [Authorize]
        public IActionResult Create(int classifiedId = 0)
        {            
            Classified classified;
            string userId = User.GetUserId();

            if (classifiedId == 0)
                classified = new Classified { UserId = userId, ContactNumber = _userService.Get(userId).PhoneNumber };
            else
                classified = _classifiedService.GetClassified(classifiedId);

            var vm = new CreateClassifiedViewModel
            {
                Classified = classified,
                Categories = _categoryService.GetCategories(),
                thumbnailUrl = classified.ProductImages.FirstOrDefault() == null ? string.Empty : classified.ProductImages.First().ImageUrl,
                FormattedPrice = classified.Id == 0 ? string.Empty : classified.Price.ToString("N2", CultureInfo.InvariantCulture)
            };

            return View(vm);            
        }

        public IActionResult Deleted()
        { 
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateClassifiedViewModel vm)
        {                       
            if (!ModelState.IsValid || (vm.ThumbnailChanged && !IsValidFile(vm.File)) || !AreValidFiles(vm.Files))
                return View("Create", vm);

            var userId = User.GetUserId();
            vm.Classified.UserId = userId;

            try
            {
                _classifiedService.convertPrice(vm.Classified, vm.FormattedPrice);
                _classifiedService.AttachImagesToClassified(vm);
                if (vm.Classified.Id == 0)
                {                    
                    _classifiedService.Add(vm.Classified);
                    TempData["Message"] = "Ogłoszenie zostało dodane.";
                }
                else
                {                                            
                    _classifiedService.Update(vm);
                    TempData["Message"] = "Ogłoszenie zostało edytowane.";
                }            
                _userService.UpdateContactNumber(userId, vm.Classified.ContactNumber);
            
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas dodawania ogłoszenia. {ex.Message}");
                TempData["Message"] = "Błąd podczas dodawania ogłoszenia.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Deactivate(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _classifiedService.Deactivate(id, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas deaktywacji ogłoszenia. {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Activate(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _classifiedService.Activate(id, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas aktywacji ogłoszenia. {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _classifiedService.Delete(id, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas usuwania ogłoszenia. {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        private bool IsValidFile(IFormFile file)
        {
            if (file.Length <= 0 || !file.ContentType.StartsWith("image"))
            {
                ModelState.AddModelError("File", "Niepoprawne pliki");
                _logger.LogError($"Niepoprawna miniaturka przepuszczona do kontrolera. {file.FileName} {file.ContentType}");
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
                    _logger.LogError($"Niepoprawny plik przepuszczony do kontrolera. {file.FileName} {file.ContentType}");
                    return false;
                }
            }
            return true;
        }
    }
}
