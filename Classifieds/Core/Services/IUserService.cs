using Classifieds.Core.Models.Domains;

namespace Classifieds.Core.Services
{
    public interface IUserService
    {
        ApplicationUser Get(string userId);
        void UpdateContactNumber(string userId, string contactNumber);
    }
}
