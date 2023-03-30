using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Repositories;
using System.Linq;

namespace Classifieds.Persistence.Repositories
{
    public class PasswordRepository : IPasswordRepository
    {
        private IApplicationDbContext _context;
        public PasswordRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public PasswordData GetKeys()
        {
            return _context.Passwords.First();
        }

        public void Add(PasswordData passwordData)
        {
            _context.Passwords.Add(passwordData);
        }
    }
}
