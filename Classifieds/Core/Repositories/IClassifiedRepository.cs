using Classifieds.Core.Models.Domains;
using System.Collections.Generic;

namespace Classifieds.Core.Repositories
{
    public interface IClassifiedRepository
    {
        IEnumerable<Classified> GetClassifieds();
        IEnumerable<Classified> GetFilteredClassifieds(int categoryId);
        void Add(Classified classified);
    }
}
