using System.Collections.Generic;

namespace Classifieds.Core.Repositories
{
    public interface IEmailRepository
    {
        void SaveToDatabase(Models.Domains.Email email);
        IEnumerable<Models.Domains.Email> GetReceived(string userId);
        IEnumerable<Models.Domains.Email> GetSent(string userId);
    }
}
