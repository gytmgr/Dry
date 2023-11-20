namespace Dry.Core.Utilities;

/// <summary>
/// Hash加密
/// </summary>
public class HashEncrypt
{
    /// <summary>
    /// 字符加密
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="hash">加密方式</param>
    /// <param name="data">加密数据</param>
    /// <param name="encoding">字符编码</param>
    /// <returns></returns>
    public static string Encrypt<T>(T hash, string data, Encoding? encoding = null) where T : HashAlgorithm
    {
        encoding ??= Encoding.UTF8;
        return Encrypt(hash, encoding.GetBytes(data));
    }

    /// <summary>
    /// 字节加密
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="hash">加密方式</param>
    /// <param name="data">加密数据</param>
    /// <returns></returns>
    public static string Encrypt<T>(T hash, byte[] data) where T : HashAlgorithm
    {
        hash.CheckParamNull(nameof(hash));
        data.CheckParamNull(nameof(data));

        var bytes = hash.ComputeHash(data);
        return BitConverter.ToString(bytes).Replace("-", "");
    }

    /// <summary>
    /// 流加密
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="hash">加密方式</param>
    /// <param name="data">加密数据</param>
    /// <returns></returns>
    public static async Task<string> EncryptAsync<T>(T hash, Stream data) where T : HashAlgorithm
    {
        hash.CheckParamNull(nameof(hash));

        var bytes = await hash.ComputeHashAsync(data);
        return BitConverter.ToString(bytes).Replace("-", "");
    }
}