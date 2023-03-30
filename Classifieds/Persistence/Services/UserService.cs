using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;

namespace Classifieds.Persistence.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApplicationUser Get(string userId)
        {
            return _unitOfWork.UserRepository.Get(userId);
        }
        public void UpdateContactNumber(string userId, string contactNumber)
        {
            _unitOfWork.UserRepository.UpdateContactNumber(userId, contactNumber);
            _unitOfWork.Complete();
        }
    }
}
