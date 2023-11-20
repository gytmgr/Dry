using Dry.Core.Model;
using Dry.Dependency;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Dry.Console.Test.Demo;

public static class Dependency
{
    public static Task Run()
    {
        var sc = new ServiceCollection();
        sc.AddDependency(); ;
        var sp = sc.BuildServiceProvider();
        var ee = sp.GetService<IAppClient>();
        ee.App = new object();
        sp = sc.BuildServiceProvider();
        var ee1 = sp.GetService<IAppClient>();
        var e = ee == ee1;
        return Task.CompletedTask;
    }
}
/// <summary>
/// 应用客户端接口
/// </summary>
public interface IAppClient
{
    /// <summary>
    /// 应用编码
    /// </summary>
    string AppCode { get; }

    /// <summary>
    /// 应用
    /// </summary>
    object App { get; set; }
}

/// <summary>
/// 应用客户端基类
/// </summary>
/// <typeparam name="TClient"></typeparam>
public abstract class AppClient<TClient> : IAppClient where TClient : AppClient<TClient>
{
    /// <summary>
    /// 实例
    /// </summary>
    public static TClient Instance { get; private set; }

    /// <summary>
    /// 应用编码
    /// </summary>
    public abstract string AppCode { get; }

    /// <summary>
    /// 应用
    /// </summary>
    public virtual object App { get; set; }

    /// <summary>
    /// 构造体
    /// </summary>
    public AppClient()
        => Instance = (TClient)this;
}

public class AppHubServiceClient : AppClient<AppHubServiceClient>, ISingletonDependency<IAppClient>
{
    public override string AppCode => "gg";
}