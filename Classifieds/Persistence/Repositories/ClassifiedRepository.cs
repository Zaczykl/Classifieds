using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IEnumerable<Classified> GetClassifieds()
        {
            return _context.Classifieds
                .Include(x => x.ProductImages);
        }
        public void Add(Classified classified)
        {
            _context.Classifieds.Add(classified);
        }
    }
}
