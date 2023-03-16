using Classifieds.Core.Repositories;

namespace Classifieds.Core
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; set; }
        IClassifiedRepository ClassifiedRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        IPasswordRepository PasswordRepository { get; set; }
        IEmailRepository EmailRepository { get; set; }

        void Complete();
    }
}
