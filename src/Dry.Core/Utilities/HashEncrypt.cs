using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dry.Core.Utilities
{
    /// <summary>
    /// Hash加密
    /// </summary>
    public class HashEncrypt
    {
        /// <summary>
        /// 字符加密
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hash"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Encrypt<T>(T hash, string data, Encoding encoding = null) where T : HashAlgorithm
        {
            if (encoding is null)
            {
                encoding = Encoding.UTF8;
            }
            return Encrypt(hash, encoding.GetBytes(data));
        }

        /// <summary>
        /// 字节加密
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hash"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encrypt<T>(T hash, byte[] data) where T : HashAlgorithm
        {
            var bytes = hash.ComputeHash(data);
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        /// <summary>
        /// 流加密
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hash"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encrypt<T>(T hash, Stream data) where T : HashAlgorithm
        {
            var bytes = hash.ComputeHash(data);
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}