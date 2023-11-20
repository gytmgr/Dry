namespace Dry.Core.Utilities;

/// <summary>
/// ftp帮助类
/// </summary>
public class FtpHelper
{
    private FtpWebRequest? _reqFTP;

    /// <summary>
    /// ftp主机地址
    /// </summary>
    public string FtpHost { get; private set; }

    /// <summary>
    /// ftp登录名
    /// </summary>
    public string FtpUserID { get; private set; }

    /// <summary>
    /// ftp登录密码
    /// </summary>
    public string FtpPassword { get; private set; }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="ftpServerIP"></param>
    /// <param name="ftpUserID"></param>
    /// <param name="ftpPassword"></param>
    public FtpHelper(string ftpServerIP, string ftpUserID, string ftpPassword) : this(ftpServerIP, "21", ftpUserID, ftpPassword) { }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="ftpServerIP"></param>
    /// <param name="ftpServerPort"></param>
    /// <param name="ftpUserID"></param>
    /// <param name="ftpPassword"></param>
    public FtpHelper(string ftpServerIP, string? ftpServerPort, string ftpUserID, string ftpPassword)
    {
        if (string.IsNullOrEmpty(ftpServerPort) || ftpServerPort == "21")
        {
            FtpHost = "ftp://" + ftpServerIP;
        }
        else
        {
            FtpHost = "ftp://" + ftpServerIP + ":" + ftpServerPort;
        }
        FtpUserID = ftpUserID;
        FtpPassword = ftpPassword;
    }

    /// <summary>
    /// 连接ftp
    /// </summary>
    /// <param name="path"></param>
    private void Connect(string path)
    {
        path.CheckParamNull(nameof(path));

        // 根据uri创建FtpWebRequest对象
        _reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(path));
        // 指定数据传输类型
        _reqFTP.UseBinary = true;
        // ftp用户名和密码
        _reqFTP.Credentials = new NetworkCredential(FtpUserID, FtpPassword);
    }

    /// <summary>
    /// 从ftp服务器上获得文件列表
    /// </summary>
    /// <param name="path"></param>
    /// <param name="WRMethods"></param>
    /// <returns></returns>
    private async Task<string[]> GetFileListAsync(string path, string WRMethods)
    {
        var result = new StringBuilder();
        Connect(path);
        _reqFTP!.Method = WRMethods;
        var response = _reqFTP.GetResponse();
        var reader = new StreamReader(response.GetResponseStream(), Encoding.Default);//中文文件名
        var line = await reader.ReadLineAsync();
        while (line != null)
        {
            result.Append(line);
            result.Append('\n');
            line = await reader.ReadLineAsync();
        }
        // to remove the trailing '\n'
        result.Remove(result.ToString().LastIndexOf('\n'), 1);
        reader.Close();
        response.Close();
        return result.ToString().Split('\n');
    }

    /// <summary>
    /// 从ftp服务器上获得文件列表
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public async Task<string[]> GetFileListAsync(string path)
        => await GetFileListAsync(FtpHost + "/" + path, WebRequestMethods.Ftp.ListDirectory);

    /// <summary>
    /// 从ftp服务器上获得文件列表
    /// </summary>
    /// <returns></returns>
    public async Task<string[]> GetFileList()
        => await GetFileListAsync(FtpHost + "/", WebRequestMethods.Ftp.ListDirectory);

    /// <summary>
    /// 从ftp服务器上载文件的功能
    /// </summary>
    /// <param name="ftpFilePath"></param>
    /// <param name="localFilePath"></param>
    /// <returns></returns>
    public async Task UploadAsync(string ftpFilePath, string localFilePath)
    {
        var fileInf = new FileInfo(localFilePath);
        var uri = FtpHost + "/" + ftpFilePath;
        Connect(uri);//连接         
        // 默认为true，连接不会被关闭
        // 在一个命令之后被执行
        _reqFTP!.KeepAlive = false;
        // 指定执行什么命令
        _reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
        // 上传文件时通知服务器文件的大小
        _reqFTP.ContentLength = fileInf.Length;
        // 缓冲大小设置为kb
        var buffLength = 2048;
        var buff = new byte[buffLength];
        int contentLen;
        // 打开一个文件流(System.IO.FileStream) 去读上传的文件
        var fs = fileInf.OpenRead();
        try
        {
            // 把上传的文件写入流
            var strm = _reqFTP.GetRequestStream();
            // 每次读文件流的kb
            contentLen = await fs.ReadAsync(buff, 0, buffLength);
            // 流内容没有结束
            while (contentLen != 0)
            {
                // 把内容从file stream 写入upload stream
                strm.Write(buff, 0, contentLen);
                contentLen = await fs.ReadAsync(buff, 0, buffLength);
            }
            // 关闭两个流
            strm.Close();
        }
        finally
        {
            fs.Close();
        }
    }

    /// <summary>
    /// 从ftp服务器下载文件
    /// </summary>
    /// <param name="localFilePath"></param>
    /// <param name="ftpFilePath"></param>
    /// <returns></returns>
    public async Task DownloadAsync(string localFilePath, string ftpFilePath)
    {
        var url = FtpHost + "/" + ftpFilePath;
        Connect(url);//连接 
        _reqFTP!.Method = WebRequestMethods.Ftp.DownloadFile;
        var response = (FtpWebResponse)_reqFTP.GetResponse();
        var ftpStream = response.GetResponseStream();
        var bufferSize = 2048;
        int readCount;
        var buffer = new byte[bufferSize];
        readCount = await ftpStream.ReadAsync(buffer, 0, bufferSize);

        var outputStream = new FileStream(localFilePath, FileMode.Create);
        while (readCount > 0)
        {
            await outputStream.WriteAsync(buffer, 0, readCount);
            readCount = await ftpStream.ReadAsync(buffer, 0, bufferSize);
        }
        ftpStream.Close();
        outputStream.Close();
        response.Close();
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="fileName"></param>
    public void DeleteFileName(string fileName)
    {
        var fileInf = new FileInfo(fileName);
        var uri = FtpHost + "/" + fileInf.Name;
        Connect(uri);//连接         
        // 默认为true，连接不会被关闭
        // 在一个命令之后被执行
        _reqFTP!.KeepAlive = false;
        // 指定执行什么命令
        _reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
        var response = (FtpWebResponse)_reqFTP.GetResponse();
        response.Close();
    }

    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="dirName"></param>
    public void MakeDir(string dirName)
    {
        var uri = FtpHost + "/" + dirName;
        Connect(uri);//连接      
        _reqFTP!.Method = WebRequestMethods.Ftp.MakeDirectory;
        var response = (FtpWebResponse)_reqFTP.GetResponse();
        response.Close();
    }

    /// <summary>
    /// 删除目录
    /// </summary>
    /// <param name="dirName"></param>
    public void delDir(string dirName)
    {
        var uri = FtpHost + "/" + dirName;
        Connect(uri);//连接      
        _reqFTP!.Method = WebRequestMethods.Ftp.RemoveDirectory;
        var response = (FtpWebResponse)_reqFTP.GetResponse();
        response.Close();
    }

    /// <summary>
    /// 获得文件大小
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public long GetFileSize(string filename)
    {
        long fileSize = 0;
        var fileInf = new FileInfo(filename);
        var uri = FtpHost + "/" + fileInf.Name;
        Connect(uri);//连接      
        _reqFTP!.Method = WebRequestMethods.Ftp.GetFileSize;
        var response = (FtpWebResponse)_reqFTP.GetResponse();
        fileSize = response.ContentLength;
        response.Close();
        return fileSize;
    }

    /// <summary>
    /// 文件改名
    /// </summary>
    /// <param name="currentFilename"></param>
    /// <param name="newFilename"></param>
    public void Rename(string currentFilename, string newFilename)
    {
        var fileInf = new FileInfo(currentFilename);
        var uri = FtpHost + "/" + fileInf.Name;
        Connect(uri);//连接
        _reqFTP!.Method = WebRequestMethods.Ftp.Rename;
        _reqFTP.RenameTo = newFilename;
        var response = (FtpWebResponse)_reqFTP.GetResponse();
        //var ftpStream = response.GetResponseStream();
        //ftpStream.Close();
        response.Close();
    }

    /// <summary>
    /// 获得文件明细
    /// </summary>
    /// <returns></returns>
    public async Task<string[]> GetFilesDetailListAsync()
        => await GetFileListAsync(FtpHost + "/", WebRequestMethods.Ftp.ListDirectoryDetails);

    /// <summary>
    /// 获得文件明细
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public async Task<string[]> GetFilesDetailListAsync(string path)
        => await GetFileListAsync(FtpHost + "/" + path, WebRequestMethods.Ftp.ListDirectoryDetails);
}