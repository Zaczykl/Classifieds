using Classifieds.Core;
using Classifieds.Core.Cipher;
using Classifieds.Core.Email;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Microsoft.Extensions.Configuration;

namespace Classifieds.Persistence.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWritableOptions<EmailParams> _writableOptions;
        private readonly IConfiguration _configuration;
        
        public PasswordService(IUnitOfWork unitOfWork, IConfiguration configuration, IWritableOptions<EmailParams> writableOptions)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _writableOptions = writableOptions;
        }
        public PasswordData GetKeys()
        {
            return _unitOfWork.PasswordRepository.GetKeys();
        }
        public void Add(PasswordData passwordData)
        {
            _unitOfWork.PasswordRepository.Add(passwordData);
            _unitOfWork.Complete();
        }

        public string GetPassword()
        {
            var password = _configuration.GetValue<string>("EmailSettings:SenderEmailPassword");

            if (!password.StartsWith("Encrypted"))            
                EncryptPassword(password);            
            else            
                password = DecryptPassword();
            
            return password;
        }

        private void EncryptPassword(string password)
        {
            Cipher cipher = new Cipher();
            var keys = cipher.EncryptPassword(password);
            var passwordData = new PasswordData();

            passwordData.SetPassword(keys);
            Add(passwordData);
            _writableOptions.Update(x =>
            {
                x.SenderEmailPassword = "Encrypted";
            });
        }
        private string DecryptPassword()
        {
            var cipher = new Cipher();
            var passwordData = _unitOfWork.PasswordRepository.GetKeys();
            var keys = new Keys(passwordData.Key, passwordData.Iv, passwordData.EncryptedPassword);
            return cipher.DecryptPassword(keys);
        }
    }
}
