using Classifieds.Core.Models.Domains;
using System.Threading.Tasks;

namespace Classifieds.Core.Services
{
    public interface IEmailService
    {
        void SaveToDatabase(Models.Domains.Email email);
        Task SendEmail(Models.Domains.Email email);
    }
}
