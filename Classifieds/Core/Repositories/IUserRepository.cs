using Classifieds.Core.Models.Domains;

namespace Classifieds.Core.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser Get(string userId);
        void UpdateContactNumber(string userId, string contactNumber);
    }
}
