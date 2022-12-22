namespace Dry.Core.Utilities;

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
    public static async Task<string> EncryptAsync(string str, string strKey = null)
    {
        var encryptKey = string.IsNullOrEmpty(strKey) ? _key : Convert.FromBase64String(strKey);
        var stream = new MemoryStream();
        var transform = new TripleDESCryptoServiceProvider
        {
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        }.CreateEncryptor(encryptKey, _iv);
        var cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write);
        try
        {
            var data = Encoding.UTF8.GetBytes(str);
            await cryptoStream.WriteAsync(data, 0, data.Length);
            await cryptoStream.FlushFinalBlockAsync();
            var bytes = stream.ToArray();
            return Convert.ToBase64String(bytes);
        }
        catch
        {
            return null;
        }
        finally
        {
            cryptoStream.Close();
            stream.Close();
        }
    }

    /// <summary>
    /// DES3 ECB模式解密
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strKey"></param>
    /// <returns></returns>
    public static async Task<string> DecryptAsync(string str, string strKey = null)
    {
        var decryptKey = string.IsNullOrEmpty(strKey) ? _key : Convert.FromBase64String(strKey);
        var stream = new MemoryStream();
        var transform = new TripleDESCryptoServiceProvider
        {
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        }.CreateDecryptor(decryptKey, _iv);
        var cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write);
        try
        {
            var data = Convert.FromBase64String(str);
            await cryptoStream.WriteAsync(data, 0, data.Length);
            await cryptoStream.FlushFinalBlockAsync();
            var bytes = stream.ToArray();
            return Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            return null;
        }
        finally
        {
            cryptoStream.Close();
            stream.Close();
        }
    }
}