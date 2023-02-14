namespace Dry.AspNetCore.Infrastructure.AppBuilderConfigure;

/// <summary>
/// 服务配置器接口
/// </summary>
public interface IServiceConfigurer : IHasOrder
{
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    Task ConfigureAsync(IServiceCollection services);
}