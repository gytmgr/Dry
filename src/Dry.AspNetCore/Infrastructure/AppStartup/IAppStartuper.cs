namespace Dry.AspNetCore.Infrastructure.AppStartup;

/// <summary>
/// 应用启动器接口
/// </summary>
public interface IAppStartuper : IHasOrder, IDependency<IAppStartuper>
{
    /// <summary>
    /// 启动
    /// </summary>
    /// <returns></returns>
    Task StartupAsync();
}