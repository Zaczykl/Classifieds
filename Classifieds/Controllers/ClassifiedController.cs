using Classifieds.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Classifieds.Persistence.Extensions;
using Classifieds.Persistence.Repositories;
using Classifieds.Core.Models.Domains;
using Classifieds.Persistence;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Classifieds.Core;
using Classifieds.Persistence.Services;
using Classifieds.Core.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
