using Classifieds.Core.Models.Domains;
using System.Collections.Generic;

namespace Classifieds.Core.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
    }
}
