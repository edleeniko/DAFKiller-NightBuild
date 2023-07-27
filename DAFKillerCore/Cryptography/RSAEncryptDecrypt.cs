using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller.Cryptography
{
    public class RSAEncryptDecrypt
    {
        UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
        RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
        byte[] data;
        byte[] encryptData;

        byte[] Encrypt(byte[] data, RSAParameters RSAKey, bool fOAEP)
        {
            byte[] encryptedData;
            using (RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider())
            {
                rSACryptoServiceProvider.ImportParameters(RSAKey);
                encryptedData = rSACryptoServiceProvider.Encrypt(data, fOAEP);
            }
            return encryptedData;
        }
        byte[] Decrypt(byte[] data, RSAParameters RSAKey, bool fOAEP)
        {
            byte[] decryptedData;
            using (RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider())
            {
                rSACryptoServiceProvider.ImportParameters(RSAKey);
                decryptedData = rSACryptoServiceProvider.Decrypt(data, fOAEP);
            }
            return decryptedData;
        }

        public string DoEncrypt(string input)
        {
            string output;
            data = unicodeEncoding.GetBytes(input);
            encryptData = Encrypt(data, rSACryptoServiceProvider.ExportParameters(false), false);
            output = unicodeEncoding.GetString(encryptData);
            return output;
        }
        public string DoDecrypt()
        {
            string output;
            byte[] data = Decrypt(encryptData, rSACryptoServiceProvider.ExportParameters(true), false);
            output = unicodeEncoding.GetString(data);
            return output;
        }

    }
}
