using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Email> GetReceived(string userId)
        {
            var userEmail = _context.Users.Single(x => x.Id == userId).Email;
            return _context.Emails.Where(x => x.ReceiverEmail == userEmail).OrderByDescending(x=>x.Id);
        }

        public IEnumerable<Email> GetSent(string userId)
        {
            var userEmail = _context.Users.Single(x => x.Id == userId).Email;
            return _context.Emails.Where(x => x.SenderEmail == userEmail).OrderByDescending(x => x.Id);
        }
    }
}
