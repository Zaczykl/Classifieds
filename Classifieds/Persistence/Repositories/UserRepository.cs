using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Repositories;
using System.Linq;

namespace Classifieds.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IApplicationDbContext _context;
        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser Get(string userId)
        {
            return _context.Users.Single(x => x.Id == userId);
        }
        public void UpdateContactNumber(string userId, string contactNumber)
        {
            _context.Users.Single(x => x.Id == userId).PhoneNumber = contactNumber;
        }
    }
}
