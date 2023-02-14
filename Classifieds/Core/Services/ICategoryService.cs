using Classifieds.Core.Models.Domains;
using System.Collections.Generic;

namespace Classifieds.Core.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
