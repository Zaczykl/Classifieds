using Classifieds.Core.Cipher;

namespace Classifieds.Core.Models.Domains
{
    public class PasswordData
    {
        private byte[] _key;
        private byte[] _iv;
        private byte[] _encryptedPassword;

        public int Id { get; set; }
        public byte[] Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public byte[] Iv
        {
            get { return _iv; }
            set { _iv = value; }
        }
        public byte[] EncryptedPassword
        {
            get { return _encryptedPassword; }
            set { _encryptedPassword = value; }
        }

        public void SetPassword(Keys keys)
        {
            _key = keys.Key;
            _iv = keys.Iv;
            _encryptedPassword = keys.EncryptedPassword;
        }
    }
}
