using Classifieds.Core.Models.Domains;
using Classifieds.Core.ViewModels;
using System.Collections.Generic;

namespace Classifieds.Core.Repositories
{
    public interface IClassifiedRepository
    {
        Classified GetClassified(int id);
        IEnumerable<Classified> GetClassifieds(FilterClassifieds classifiedParams);
        void Add(Classified classified);
        void Update(CreateClassifiedViewModel viewModel);
        void Activate(int id, string userId);
        void Deactivate(int id, string userId);
        void Delete(int id, string userId);
    }
}
