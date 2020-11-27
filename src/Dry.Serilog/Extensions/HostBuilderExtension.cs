using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

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
        /// <param name="filePathConfigName">日志路径在AppSettings的配置名称</param>
        /// <returns></returns>
        public static IHostBuilder UseDrySerilog(this IHostBuilder hostBuilder, string filePathConfigName = "LogFilePath")
        {
            return hostBuilder.UseSerilog((hostBuilderConfig, loggerConfig) =>
            {
                var filePath = hostBuilderConfig.Configuration[$"AppSettings:{filePathConfigName}"];
                if (string.IsNullOrEmpty(filePath))
                {
                    filePath = "Logs/log.txt";
                }
                loggerConfig
                .WriteTo.Async(x => x.Debug())
                .WriteTo.Async(x => x.Console())
                .WriteTo.Async(x => x.File(filePath, LogEventLevel.Warning, rollingInterval: RollingInterval.Day));
            });
        }
    }
}