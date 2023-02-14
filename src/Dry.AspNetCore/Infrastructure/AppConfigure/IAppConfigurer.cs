namespace Dry.AspNetCore.Infrastructure.AppConfigure;

/// <summary>
/// 应用配置器接口
/// </summary>
public interface IAppConfigurer : IHasOrder
{
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    Task ConfigureAsync(WebApplication app);
}