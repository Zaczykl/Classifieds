namespace Classifieds.Core.Cipher
{
    public class Keys
    {        
        public Keys(byte[] key, byte[] iv, byte[] encryptedPassword)
        {
            Key = key;
            Iv = iv;
            EncryptedPassword = encryptedPassword;
        }
        public byte[] Key { get; }
        public byte[] Iv { get;}
        public byte[] EncryptedPassword { get; }
    }
}
