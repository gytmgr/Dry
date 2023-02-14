namespace Dry.AspNetCore.Infrastructure.AppBuilderConfigure;

/// <summary>
/// 应用生成配置器接口
/// </summary>
public interface IAppBuilderConfigurer : IHasOrder
{
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    Task ConfigureAsync(WebApplicationBuilder builder);
}