using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller.Cryptography
{
    public class MD5TripleDESEncryptDecrypt
    {
        string hash = "putinhujlo";
        string output = null;

        public string DoEncrypt(string inputText)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(inputText);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));//Get hash key
                //Encrypt data by hash key
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    output = Convert.ToBase64String(results, 0, results.Length);
                }
            }
            return output;
        }

        public string DoDecrypt(string inputEncrypted)
        {
            //Convert a string to byte array
            byte[] data = Convert.FromBase64String(inputEncrypted);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));//Get hash key
                //Decrypt data by hash key
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    output = UTF8Encoding.UTF8.GetString(results);
                }
            }
            return output;
        }
    }
}
