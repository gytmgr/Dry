namespace Dry.AspNetCore.Infrastructure.AppBuilderConfigure;

/// <summary>
/// 数据初始化器接口
/// </summary>
public interface IDataIniter : IHasOrder, IDependency<IDataIniter>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    Task InitAsync();
}