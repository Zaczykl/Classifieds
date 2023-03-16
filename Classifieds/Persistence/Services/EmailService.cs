using Classifieds.Core.Email;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Classifieds.Persistence.Services
{
    public class EmailService : IEmailService
    {
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;
        public EmailService(IPasswordService passwordService, IConfiguration configuration)
        {
            _passwordService = passwordService;
            _configuration = configuration;
        }
        
        public async Task SendEmail(Email emailParams)
        {
            var body = GenerateHtml(emailParams);
            var email = new EmailCore(new EmailParams
            {
                HostSmtp = _configuration.GetValue<string>("EmailSettings:HostSmtp"),
                Port = _configuration.GetValue<int>("EmailSettings:Port"),
                EnableSsl = _configuration.GetValue<bool>("EmailSettings:EnableSsl"),
                SenderName = emailParams.SenderEmail,
                SenderEmail = _configuration.GetValue<string>("EmailSettings:SenderEmail"),
                SenderEmailPassword = _passwordService.GetPassword()
            });
            await email.Send(emailParams.Title, body, emailParams.ReceiverEmail);
        }
        private string GenerateHtml(Email email)
        {
            return $"<p>{email.Message}</p><br /><h4>Nadawca: {email.SenderEmail}</h4>";
        }
    }
}
