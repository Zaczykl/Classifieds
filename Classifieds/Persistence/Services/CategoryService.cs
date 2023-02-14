using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Classifieds.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.CategoryRepository.GetCategories();
        }
    }
}
