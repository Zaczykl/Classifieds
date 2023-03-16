using Classifieds.Core.Models.Domains;

namespace Classifieds.Core.Services
{
    public interface IPasswordService
    {
        PasswordData GetKeys();
        void Add(PasswordData passwordData);
        string GetPassword();
    }
}
