namespace Dry.AspNetCore.Infrastructure.AppBuilderConfigure;

/// <summary>
/// 数据初始化器接口
/// </summary>
public interface IDataIniter : IHasOrder
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    Task InitAsync();
}