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

        public Classified GetClassified(int id)
        {
            var classified = _context.Classifieds.Single(x => x.Id == id);
            classified.ProductImages = _context.ProductImages.Where(x => x.ClassifiedId == id).ToList();
            classified.User = _context.Users.Single(x => x.Id == classified.UserId);
            return classified;
        }
        public IEnumerable<Classified> GetClassifieds(string title, int categoryId)
        {
            var classifieds = _context.Classifieds
                .Include(x => x.ProductImages)
                .AsQueryable();

            if (categoryId != 0)
                classifieds = classifieds.Where(x => x.CategoryId == categoryId);
            if (title != null)
                classifieds = classifieds.Where(x => x.Title.Contains(title));

            return classifieds.OrderByDescending(x => x.Id);
        }
        
        public void Add(Classified classified)
        {
            _context.Classifieds.Add(classified);
        }
    }
}
