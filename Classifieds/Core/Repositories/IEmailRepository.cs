namespace Classifieds.Core.Repositories
{
    public interface IEmailRepository
    {
        void SaveToDatabase(Models.Domains.Email email);
    }
}
