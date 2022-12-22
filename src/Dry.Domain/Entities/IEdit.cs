namespace Dry.Domain.Entities;

/// <summary>
/// 实体编辑接口
/// </summary>
public interface IEdit
{
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public Task EditAsync([NotNull] IServiceProvider serviceProvider)
    {
        if (this is IHasUpdateTime updateTimeEntity)
        {
            updateTimeEntity.UpdateTime = DateTime.Now;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 编辑完成处理
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public Task<bool> EditedAsync([NotNull] IServiceProvider serviceProvider)
        => Task.FromResult(false);
}