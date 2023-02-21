using Classifieds.Core.Models.Domains;
using System.Collections.Generic;

namespace Classifieds.Core.Repositories
{
    public interface IClassifiedRepository
    {
        Classified GetClassified(int id);
        IEnumerable<Classified> GetClassifieds(string title, int categoryId);
        void Add(Classified classified);
    }
}
