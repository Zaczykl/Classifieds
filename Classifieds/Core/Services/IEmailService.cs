using Classifieds.Core.Models.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Classifieds.Core.Services
{
    public interface IEmailService
    {
        void SaveToDatabase(Models.Domains.Email email);
        Task SendEmail(Models.Domains.Email email);
        IEnumerable<Models.Domains.Email> GetReceived(string userId);
        IEnumerable<Models.Domains.Email> GetSent(string userId);
    }
}
