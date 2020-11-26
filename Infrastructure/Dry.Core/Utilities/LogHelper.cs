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
                sb.Append("=");
            }
            stream.WriteLine("{0}Message:{1}", sb.ToString(), e.Message);
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
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="e"></param>
        /// <param name="otherInfo"></param>
        public static void Exception(string className, string methodName, Exception e, params string[] otherInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(methodName))
                {
                    return;
                }
                var filePath = string.Format(@"{0}\Log\Exception\{1}\{2}\{3}.txt", AppDomain.CurrentDomain.BaseDirectory, DateTime.Today.ToString("yyyyMM"), DateTime.Today.ToString("yyyyMMdd"), className);
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
                stream.WriteLine("MethodName:{0}", methodName);
                stream.WriteLine("");
                stream.WriteLine("LogTime:{0}", DateTime.Now.ToString());
                stream.WriteLine("");
                if (otherInfo != null)
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
        /// <param name="paramList"></param>
        public static void Action(params string[] paramList)
        {
            try
            {
                if (paramList == null || paramList.Length == 0)
                {
                    return;
                }
                var filePath = string.Format(@"{0}\Log\Action\{1}\{2}.txt", AppDomain.CurrentDomain.BaseDirectory, DateTime.Today.ToString("yyyyMM"), DateTime.Today.ToString("yyyyMMdd"));
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
                foreach (var item in paramList)
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