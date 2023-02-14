using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Repositories;
using System.Linq;

namespace Classifieds.Persistence.Repositories
{
    public class ClassifiedRepository : IClassifiedRepository
    {
        private IApplicationDbContext _context;
        public ClassifiedRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Classified classified)
        {
            _context.Classifieds.Add(classified);
        }
    }
}
