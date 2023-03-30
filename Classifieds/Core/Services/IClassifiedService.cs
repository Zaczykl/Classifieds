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
        void AttachImagesToClassified(CreateClassifiedViewModel viewModel);
        void convertPrice(Classified classified, string formattedPrice);
        
    }
}
