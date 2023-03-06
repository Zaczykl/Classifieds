using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Classifieds.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Classifieds.Areas.Identity.Pages.Account.Manage
{
    public class ManageClassifiedsModel : PageModel
    {
        private IClassifiedService _classifiedService;

        public ManageClassifiedsModel(IClassifiedService classifiedService)
        {
            _classifiedService = classifiedService;
        }

        public IEnumerable<Classified> Classifieds { get; set; }
        public void OnGet()
        {
            string userId = User.GetUserId();
            Classifieds = _classifiedService.GetClassifieds(new FilterClassifieds { UserId = userId, Active = true });
            _classifiedService.GetThumbnailsUrls(Classifieds);            
        }
    }
}
