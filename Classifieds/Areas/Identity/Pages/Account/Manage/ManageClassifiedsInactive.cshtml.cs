using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Classifieds.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Classifieds.Areas.Identity.Pages.Account.Manage
{
    public class ManageClassifiedsInactiveModel : PageModel
    {
        private IClassifiedService _classifiedService;

        public ManageClassifiedsInactiveModel(IClassifiedService classifiedService)
        {
            _classifiedService = classifiedService;
        }

        public IEnumerable<Classified> ClassifiedsInactive { get; set; }
        public void OnGet()
        {
            string userId = User.GetUserId();
            ClassifiedsInactive = _classifiedService.GetClassifieds(new FilterClassifieds { UserId = userId });
        }
    }
}
