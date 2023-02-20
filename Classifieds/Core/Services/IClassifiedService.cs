using Classifieds.Core.Models.Domains;
using Classifieds.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Classifieds.Core.Services
{
    public interface IClassifiedService
    {
        IEnumerable<Classified> GetClassifieds(string title = null, int categoryId = 0);
        Task AttachPhotosAsync(CreateClassifiedViewModel viewModel);        
        void Add(Classified classified);
        void convertPrice(Classified classified, string formattedPrice);
    }
}
