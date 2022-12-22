namespace Dry.Core.Utilities;

/// <summary>
/// 日志帮助类
/// </summary>
public static class LogHelper
{
    /// <summary>
    /// 记录异常日志
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="e"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    private static async Task WriteExceptionAsync(StreamWriter stream, Exception e, int level)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < level; i++)
        {
            sb.Append('=');
        }
        await stream.WriteLineAsync($"{sb}Message:{e.Message}");
        await stream.WriteLineAsync(string.Empty);
        await stream.WriteLineAsync($"{sb}Source:{e.Source}");
        await stream.WriteLineAsync(string.Empty);
        await stream.WriteLineAsync($"{sb}StackTrace:{e.StackTrace}");
        await stream.WriteLineAsync(string.Empty);
        await stream.WriteLineAsync($"{sb}TargetSite:{e.TargetSite}");
        await stream.WriteLineAsync(string.Empty);
        if (e.InnerException is not null)
        {
            await WriteExceptionAsync(stream, e.InnerException, level + 1);
        }
    }

    /// <summary>
    /// 记录异常
    /// </summary>
    /// <param name="className">类名</param>
    /// <param name="methodName">方法名</param>
    /// <param name="e">异常</param>
    /// <param name="otherInfo">其他信息</param>
    /// <returns></returns>
    public static async Task ExceptionAsync(string className, string methodName, Exception e, params string[] otherInfo)
    {
        if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(methodName))
        {
            return;
        }
        var relativePath = $@"Log\Exception\{DateTime.Today:yyyyMM}\{DateTime.Now:yyyyMMddHH}\{className}_{methodName}";
        await ExceptionToPathAsync(relativePath, e, otherInfo);
    }

    /// <summary>
    /// 记录异常
    /// </summary>
    /// <param name="e">异常</param>
    /// <param name="otherInfo">其他信息</param>
    /// <returns></returns>
    public static async Task ExceptionAsync(Exception e, params string[] otherInfo)
    {
        var relativePath = $@"Log\Exception\{DateTime.Today:yyyyMM}\{DateTime.Now:yyyyMMddHH}";
        await ExceptionToPathAsync(relativePath, e, otherInfo);
    }

    /// <summary>
    /// 记录异常到指定路径
    /// </summary>
    /// <param name="relativePath">应用目录下的相对路径</param>
    /// <param name="e">异常</param>
    /// <param name="otherInfo">其他信息</param>
    /// <returns></returns>
    public static async Task ExceptionToPathAsync(string relativePath, Exception e, params string[] otherInfo)
    {
        try
        {
            if (string.IsNullOrEmpty(relativePath) || e is null)
            {
                return;
            }
            var filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\{relativePath}.txt";
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            var file = new FileInfo(filePath);
            StreamWriter stream;
            if (!file.Exists)
            {
                stream = file.CreateText();
            }
            else
            {
                stream = file.AppendText();
            }
            await stream.WriteLineAsync($"LogTime:{DateTime.Now}");
            await stream.WriteLineAsync(string.Empty);
            if (otherInfo is not null)
            {
                foreach (var item in otherInfo)
                {
                    await stream.WriteLineAsync(item);
                    await stream.WriteLineAsync("");
                }
            }
            await WriteExceptionAsync(stream, e, 0);
            await stream.WriteLineAsync("------------------------------------------------------------------------------------");
            stream.Close();
        }
        catch { }
    }

    /// <summary>
    /// 记录日常日志
    /// </summary>
    /// <param name="data">日志内容</param>
    /// <returns></returns>
    public static async Task ActionAsync(params string[] data)
    {
        var relativePath = $@"Log\Action\{DateTime.Today:yyyyMM}\{DateTime.Now:yyyyMMddHH}";
        await ActionToPathAsync(relativePath, data);
    }

    /// <summary>
    /// 记录日常日志到指定路径
    /// </summary>
    /// <param name="relativePath">应用目录下的相对路径</param>
    /// <param name="data">日志内容</param>
    /// <returns></returns>
    public static async Task ActionToPathAsync(string relativePath, params string[] data)
    {
        try
        {
            if (string.IsNullOrEmpty(relativePath) || (data?.Length).GetValueOrDefault() == 0)
            {
                return;
            }
            var filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\{relativePath}.txt";
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            var file = new FileInfo(filePath);
            StreamWriter stream;
            if (!file.Exists)
            {
                stream = file.CreateText();
            }
            else
            {
                stream = file.AppendText();
            }
            await stream.WriteLineAsync($"LogTime:{DateTime.Now}");
            await stream.WriteLineAsync(string.Empty);
            foreach (var item in data)
            {
                await stream.WriteLineAsync(item);
                await stream.WriteLineAsync(string.Empty);
            }
            await stream.WriteLineAsync("------------------------------------------------------------------------------------");
            stream.Close();
        }
        catch { }
    }
}