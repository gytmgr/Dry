namespace Dry.AspNetCore.Infrastructure.AppBuilderConfigure;

/// <summary>
/// Configuration读取器接口
/// </summary>
public interface IConfigurationReader : IDependency<IConfigurationReader>
{
    /// <summary>
    /// 读
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    Task ReadAsync(IConfiguration configuration);
}