using Classifieds.Core.Models.Domains;

namespace Classifieds.Core.Repositories
{
    public interface IPasswordRepository
    {
        PasswordData GetKeys();
        void Add(PasswordData passwordData);
    }
}
