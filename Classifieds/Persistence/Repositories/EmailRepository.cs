using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Repositories;

namespace Classifieds.Persistence.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private IApplicationDbContext _context;
        public EmailRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveToDatabase(Email email)
        {
            _context.Emails.Add(email);
        }
    }
}
