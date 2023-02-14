using Classifieds.Core.Repositories;

namespace Classifieds.Core
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; set; }
        IClassifiedRepository ClassifiedRepository { get; set; }
        IUserRepository UserRepository { get; set; }

        void Complete();
    }
}
