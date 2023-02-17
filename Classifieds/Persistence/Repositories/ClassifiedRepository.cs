using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
                .OrderByDescending(x=>x.Id)
                .Include(x => x.ProductImages);
                
        }

        public IEnumerable<Classified> GetFilteredClassifieds(int categoryId)
        {
            return _context.Classifieds
                .Where(x=>x.CategoryId==categoryId)
                .OrderByDescending(x => x.Id)
                .Include(x => x.ProductImages);
        }
        public void Add(Classified classified)
        {
            _context.Classifieds.Add(classified);
        }
    }
}
