﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Dry.Core.Utilities
{
    /// <summary>
    /// 文件操作帮助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 计算文件的md5值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<string> GetMd5Async(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return await HashEncrypt.EncryptAsync(MD5.Create(), fs);
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static long GetSize(string path)
        {
            if (!File.Exists(path))
            {
                return 0;
            }
            return new FileInfo(path).Length;
        }

        /// <summary>
        /// 检查文件目录是否存在，如不存在则创建
        /// </summary>
        /// <param name="path"></param>
        public static void CheckDirectory(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// 从流中读取文件，并保存
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task SaveFileAsync(Stream fileData, string path)
        {
            if (fileData is null || string.IsNullOrEmpty(path))
            {
                return;
            }
            CheckDirectory(path);
            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            await fileData.CopyToAsync(fs);
        }

        /// <summary>
        /// 保存Base64编码的文件
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="path"></param>
        /// <returns>读取的文件扩展名</returns>
        public static async Task<string> SaveBase64FileAsync(string fileData, string path)
        {
            var result = default(string);
            if (string.IsNullOrEmpty(fileData) || string.IsNullOrEmpty(path))
            {
                return result;
            }
            var index = fileData.IndexOf(",");
            var bytes = Convert.FromBase64String(fileData.Substring(index + 1));
            if (index >= 0)
            {
                var header = fileData.Substring(0, index);
                var fromIndex = header.IndexOf("/");
                var toIndex = header.IndexOf(";");
                if (fromIndex >= 0 && toIndex > fromIndex)
                {
                    result = header.Substring(fromIndex + 1, toIndex - fromIndex - 1);
                    if (!string.IsNullOrEmpty(result))
                    {
                        path = $"{path}.{result}";
                    }
                }
            }
            CheckDirectory(path);
            await File.WriteAllBytesAsync(path, bytes);
            return result;
        }

        /// <summary>
        /// 检查文件是否有修改（根据同目录下“{initFilePath}.txt”文件里面的md5值对比）
        /// </summary>
        /// <param name="initFilePath"></param>
        /// <returns></returns>
        public static async Task<bool> IsChangedAsync(string initFilePath)
        {
            var fileMd5 = await GetMd5Async(initFilePath);
            var md5FilePath = initFilePath + ".txt";
            if (File.Exists(md5FilePath) && await File.ReadAllTextAsync(md5FilePath) == fileMd5)
            {
                return false;
            }
            await File.WriteAllTextAsync(md5FilePath, fileMd5);
            return true;
        }
    }
}