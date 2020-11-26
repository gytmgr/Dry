using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dry.Core.Utilities
{
    /// <summary>
    /// ES3 ECB加密类
    /// </summary>
    public class Des3Encrypt
    {
        private static readonly byte[] _key = Convert.FromBase64String("MjA4MjUxODM1MjUwMzA2MjQxNDAzNzM3");

        private static readonly byte[] _iv = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// ES3 ECB模式加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string Encrypt(string str, string strKey = null)
        {
            try
            {
                var encryptKey = string.IsNullOrEmpty(strKey) ? _key : Convert.FromBase64String(strKey);
                var mStream = new MemoryStream();
                var tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                tdsp.Padding = PaddingMode.PKCS7;
                var cStream = new CryptoStream(mStream, tdsp.CreateEncryptor(encryptKey, _iv), CryptoStreamMode.Write);
                var utf8 = Encoding.UTF8;
                var data = utf8.GetBytes(str);
                cStream.Write(data, 0, data.Length);
                cStream.FlushFinalBlock();
                var ret = mStream.ToArray();

                cStream.Close();
                mStream.Close();
                return Convert.ToBase64String(ret);
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// DES3 ECB模式解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string Decrypt(string str, string strKey = null)
        {
            try
            {
                var decryptKey = string.IsNullOrEmpty(strKey) ? _key : Convert.FromBase64String(strKey);
                var data = Convert.FromBase64String(str);

                var tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                tdsp.Padding = PaddingMode.PKCS7;
                var msDecrypt = new MemoryStream();
                var csDecrypt = new CryptoStream(msDecrypt, tdsp.CreateDecryptor(decryptKey, _iv), CryptoStreamMode.Write);
                csDecrypt.Write(data, 0, data.Length);
                csDecrypt.FlushFinalBlock();
                var ret = msDecrypt.ToArray();
                csDecrypt.Close();
                msDecrypt.Close();
                return Encoding.UTF8.GetString(ret);
            }
            catch
            {
                return null;
            }
        }
    }
}