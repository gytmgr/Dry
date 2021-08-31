using System;
using System.IO;
using System.Text;

namespace Dry.Core.Utilities
{
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
        private static void WriteException(StreamWriter stream, Exception e, int level)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < level; i++)
            {
                sb.Append('=');
            }
            stream.WriteLine("{0}Message:{1}", sb.ToString(), e.Message);
            stream.WriteLine("");
            stream.WriteLine("{0}Source:{1}", sb.ToString(), e.Source);
            stream.WriteLine("");
            stream.WriteLine("{0}StackTrace:{1}", sb.ToString(), e.StackTrace);
            stream.WriteLine("");
            stream.WriteLine("{0}TargetSite:{1}", sb.ToString(), e.TargetSite);
            stream.WriteLine("");
            if (e.InnerException != null)
            {
                WriteException(stream, e.InnerException, level + 1);
            }
        }

        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="e">异常</param>
        /// <param name="otherInfo">其他信息</param>
        public static void Exception(string className, string methodName, Exception e, params string[] otherInfo)
        {
            if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(methodName))
            {
                return;
            }
            var relativePath = $@"Log\Exception\{DateTime.Today:yyyyMM}\{DateTime.Now:yyyyMMddHH}\{className}_{methodName}";
            ExceptionToPath(relativePath, e, otherInfo);
        }

        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="e">异常</param>
        /// <param name="otherInfo">其他信息</param>
        public static void Exception(Exception e, params string[] otherInfo)
        {
            var relativePath = $@"Log\Exception\{DateTime.Today:yyyyMM}\{DateTime.Now:yyyyMMddHH}";
            ExceptionToPath(relativePath, e, otherInfo);
        }

        /// <summary>
        /// 记录异常到指定路径
        /// </summary>
        /// <param name="relativePath">应用目录下的相对路径</param>
        /// <param name="e">异常</param>
        /// <param name="otherInfo">其他信息</param>
        public static void ExceptionToPath(string relativePath, Exception e, params string[] otherInfo)
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
                stream.WriteLine("LogTime:{0}", DateTime.Now.ToString());
                stream.WriteLine("");
                if (otherInfo is not null)
                {
                    foreach (var item in otherInfo)
                    {
                        stream.WriteLine(item);
                        stream.WriteLine("");
                    }
                }
                WriteException(stream, e, 0);
                stream.WriteLine("------------------------------------------------------------------------------------");
                stream.Close();
            }
            catch { }
        }

        /// <summary>
        /// 记录日常日志
        /// </summary>
        /// <param name="data">日志内容</param>
        public static void Action(params string[] data)
        {
            var relativePath = $@"Log\Action\{DateTime.Today:yyyyMM}\{DateTime.Now:yyyyMMddHH}";
            ActionToPath(relativePath, data);
        }

        /// <summary>
        /// 记录日常日志到指定路径
        /// </summary>
        /// <param name="relativePath">应用目录下的相对路径</param>
        /// <param name="data">日志内容</param>
        public static void ActionToPath(string relativePath, params string[] data)
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
                stream.WriteLine("LogTime:{0}", DateTime.Now.ToString());
                stream.WriteLine("");
                foreach (var item in data)
                {
                    stream.WriteLine(item);
                    stream.WriteLine("");
                }
                stream.WriteLine("------------------------------------------------------------------------------------");
                stream.Close();
            }
            catch { }
        }
    }
}