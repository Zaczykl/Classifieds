using Classifieds.Core.Models.Domains;
using Classifieds.Core.ViewModels;
using System.Threading.Tasks;

namespace Classifieds.Core.Services
{
    public interface IClassifiedService
    {
        void Add(Classified classified);
        Task AttachPhotosAsync(CreateClassifiedViewModel viewModel);
        void convertPrice(Classified classified, string formattedPrice);
    }
}
