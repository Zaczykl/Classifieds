using Classifieds.Core.Models.Domains;
using Classifieds.Core.ViewModels;
using Microsoft.CodeAnalysis.Classification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Classifieds.Core.Services
{
    public interface IClassifiedService
    {
        Classified GetClassified(int id);
        IEnumerable<Classified> GetClassifieds(FilterClassifieds classifiedParams);
        IEnumerable<Classified> SortClassifieds(string sortByRule, IEnumerable<Classified> classifiedsToSort);
        void Add(Classified classified);
        void Update(CreateClassifiedViewModel viewModel);
        void Activate(int id, string userId);
        void Deactivate(int id, string userId);        
        void Delete(int id, string userId);
        void GetThumbnailsUrls(IEnumerable<Classified> classifieds);
        void GetImagesUrls(Classified classified);
        string GetThumbnailUrl(Classified classified);
        Task AttachPhotosToClassifiedAsync(CreateClassifiedViewModel viewModel);
        void convertPrice(Classified classified, string formattedPrice);
        
    }
}
