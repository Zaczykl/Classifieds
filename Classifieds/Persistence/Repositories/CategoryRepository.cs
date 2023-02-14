using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Repositories;
using System.Collections.Generic;

namespace Classifieds.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IApplicationDbContext _context;
        public CategoryRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories;
        }
    }
}
