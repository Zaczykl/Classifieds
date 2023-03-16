using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Classifieds.Core.Cipher
{
    public class Cipher
    {
        public Keys EncryptPassword(string password)
        {
            byte[] encryptedPassword;

            using (Aes aes = Aes.Create())
            {
                byte[] key = aes.Key;
                byte[] iv = aes.IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(password);
                        }

                       encryptedPassword = ms.ToArray();
                    }
                }
                return new Keys(key, iv, encryptedPassword);
            }
        }

        public string DecryptPassword(Keys keys)
        {
            string decryptedPassword;

            using (Aes aes = Aes.Create())
            {
                aes.Key = keys.Key;
                aes.IV = keys.Iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream((keys.EncryptedPassword)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            decryptedPassword = sr.ReadToEnd();
                        }
                    }
                }
            }
            return decryptedPassword;
        }
    }
}
