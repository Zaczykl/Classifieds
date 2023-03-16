using Classifieds.Core.Models.Domains;
using System.Threading.Tasks;

namespace Classifieds.Core.Services
{
    public interface IEmailService
    {
        Task SendEmail(Models.Domains.Email email);
    }
}
