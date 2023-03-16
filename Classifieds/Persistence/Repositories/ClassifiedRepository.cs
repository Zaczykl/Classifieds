using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Repositories;
using Classifieds.Core.ViewModels;
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

        public Classified GetClassified(int id)
        {
            var classified = _context.Classifieds.Include(x => x.ProductImages).Single(x => x.Id == id);
            classified.User = _context.Users.Single(x => x.Id == classified.UserId);
            return classified;
        }
        public IEnumerable<Classified> GetClassifieds(FilterClassifieds classifiedParams)
        {
            var classifieds = _context.Classifieds
                .Where(x => x.Active == classifiedParams.Active)           
                .AsQueryable();

                if (classifiedParams.categoryId != 0)
                    classifieds = classifieds.Where(x => x.CategoryId == classifiedParams.categoryId);
                if (classifiedParams.Title != null)
                    classifieds = classifieds.Where(x => x.Title.Contains(classifiedParams.Title));
                if (classifiedParams.UserId != null)
                    classifieds = classifieds.Where(x => x.UserId == classifiedParams.UserId);
            return classifieds
                .Include(x => x.ProductImages)
                .OrderByDescending(x => x.Id);
        }

        public void Add(Classified classified)
        {            
            _context.Classifieds.Add(classified);
        }

        public void Update(CreateClassifiedViewModel vm)
        {
            var classifiedToUpdate = _context.Classifieds.Include(c => c.ProductImages).Single(c => c.Id == vm.Classified.Id);

            classifiedToUpdate.Title=vm.Classified.Title;
            classifiedToUpdate.Price= vm.Classified.Price;
            classifiedToUpdate.CategoryId = vm.Classified.CategoryId;
            classifiedToUpdate.ContactNumber = vm.Classified.ContactNumber;
            classifiedToUpdate.Description = vm.Classified.Description;
            classifiedToUpdate.Active = true;

            if (vm.ThumbnailChanged)
            {
                var thumbnailToUpdate = _context.ProductImages.Single(x => x.ClassifiedId == vm.Classified.Id && x.Thumbnail);
                thumbnailToUpdate.Image = vm.Classified.ProductImages.Single(x => x.Thumbnail).Image;
            }

            if (vm.ImagesChanged)
            {
                _context.ProductImages.RemoveRange(_context.ProductImages.Where(x => x.ClassifiedId == vm.Classified.Id && x.Thumbnail == false));

                foreach (var image in vm.Classified.ProductImages.Where(x=>x.Thumbnail==false))
                {
                    image.ClassifiedId = vm.Classified.Id;
                    _context.ProductImages.Add(image);
                }
            }            
        }

        public void Activate(int id, string userId)
        {
            _context.Classifieds.Single(x => x.Id == id && x.UserId == userId).Active = true;
        }

        public void Deactivate(int id, string userId)
        {
            _context.Classifieds.Single(x => x.Id == id && x.UserId == userId).Active = false;
        }

        public void Delete(int id, string userId)
        {
            var classifiedToDelete = _context.Classifieds.Single(x => x.Id == id && x.UserId == userId);
            _context.Classifieds.Remove(classifiedToDelete);
        }
    }
}
