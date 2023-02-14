using Classifieds.Core.Models.Domains;

namespace Classifieds.Core.Repositories
{
    public interface IClassifiedRepository
    {
        void Add(Classified classified);
    }
}
