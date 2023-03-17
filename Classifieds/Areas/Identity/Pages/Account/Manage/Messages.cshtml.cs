using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Classifieds.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Classifieds.Areas.Identity.Pages.Account.Manage
{
    public class MessagesModel : PageModel
    {
        private IEmailService _emailService;

        public MessagesModel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IEnumerable<Email> Received { get; set; }
        public IEnumerable<Email> Sent { get; set; }
        public void OnGet()
        {
            var userId = User.GetUserId();            
            Received = _emailService.GetReceived(userId);
            Sent = _emailService.GetSent(userId);
        }
    }
}
