using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace Dry.Serilog.Extensions
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class HostBuilderExtension
    {
        /// <summary>
        /// 日志配置
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="writeFileMinimumLevel">写文件的最小等级</param>
        /// <param name="rollingInterval">写文件的滚动频率</param>
        /// <param name="filePathConfigName">日志路径在AppSettings的配置名称</param>
        /// <returns></returns>
        public static IHostBuilder UseDrySerilog(this IHostBuilder hostBuilder, LogEventLevel writeFileMinimumLevel = LogEventLevel.Error, RollingInterval rollingInterval = RollingInterval.Day, string filePathConfigName = "LogFilePath")
        {
            return hostBuilder.UseSerilog((hostBuilderConfig, loggerConfig) =>
            {
                var filePath = hostBuilderConfig.Configuration[$"AppSettings:{filePathConfigName}"];
                if (string.IsNullOrEmpty(filePath))
                {
                    filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Logs\log.txt";
                }
                loggerConfig
                .WriteTo.Async(x => x.Debug())
                .WriteTo.Async(x => x.Console())
                .WriteTo.Async(x => x.File(filePath, writeFileMinimumLevel, rollingInterval: rollingInterval));
            });
        }
    }
}