using Classifieds.Core.Models.Domains;
using System.Collections.Generic;

namespace Classifieds.Core.Repositories
{
    public interface IClassifiedRepository
    {
        IEnumerable<Classified> GetClassifieds();
        void Add(Classified classified);
    }
}
