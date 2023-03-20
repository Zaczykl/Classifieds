using Classifieds.Core.Email;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Classifieds.Persistence.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Classifieds.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public EmailController(IUserService userService, IEmailService emailService, ILogger<EmailController> logger)
        {
            _userService = userService;
            _emailService = emailService;
            _logger = logger;
        }
        
        public ActionResult Write(string title, string receiverEmail, int classifiedId)
        {            
            var userId = User.GetUserId();
            var user = _userService.Get(userId);
            var email = new Email { Title = title, ReceiverEmail = receiverEmail, SenderEmail = user.Email, ClassifiedId = classifiedId };

            ViewData["ReturnUrl"] = HttpContext.Request.Headers["Referer"].ToString();

            return View(email);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Write(Email email, string returnUrl)
        {
            try
            {
                await _emailService.SendEmail(email);
                _emailService.SaveToDatabase(email);
                TempData["Message"] = "Wiadomość została wysłana.";

                return Redirect(returnUrl);
            }
            catch
            {
                TempData["Message"] = "Wystąpił błąd podczas wysyłania wiadomości.";
                _logger.LogError("Błąd podczas wysyłania e-mail");
                return Redirect(returnUrl);
            }
        }
    }
}
