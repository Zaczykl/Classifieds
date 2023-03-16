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

        public IActionResult Display(int classifiedId)
        { 
            var classified = _classifiedService.GetClassified(classifiedId);
            _classifiedService.GetImagesUrls(classified);

            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();

            return View(classified);
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
                thumbnailUrl = _classifiedService.GetThumbnailUrl(classified),
                FormattedPrice = classified.Id == 0 ? string.Empty : classified.Price.ToString("N2", CultureInfo.InvariantCulture)
        };

            return View(vm);            
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClassifiedViewModel vm)
        {
            if (!ModelState.IsValid || (vm.ThumbnailChanged && !IsValidFile(vm.File)) || !AreValidFiles(vm.Files))
            {
                return View("Create", vm);
            }

            var userId = User.GetUserId();
            vm.Classified.UserId = userId;            
            _classifiedService.convertPrice(vm.Classified, vm.FormattedPrice);

            if (vm.Classified.Id == 0)
            {
                await _classifiedService.AttachPhotosToClassifiedAsync(vm);
                _classifiedService.Add(vm.Classified);
                TempData["Message"] = "Ogłoszenie zostało dodane.";
            }
            else
            {
                if (vm.ImagesChanged || vm.ThumbnailChanged)
                {
                    await _classifiedService.AttachPhotosToClassifiedAsync(vm);
                }
                _classifiedService.Update(vm);
                TempData["Message"] = "Ogłoszenie zostało edytowane.";
            }
            
            _userService.UpdateContactNumber(userId, vm.Classified.ContactNumber);
            
            return RedirectToAction("Index", "Home");
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
                //logowanie do pliku
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
                //logowanie do pliku
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
                //logowanie do pliku
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
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
