namespace Dry.Domain.Entities;

/// <summary>
/// 实体删除接口
/// </summary>
public interface IDelete
{
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public Task DeleteAsync(IServiceProvider serviceProvider)
    {
        if (this is IHasAddTime addTimeEntity)
        {
            if (addTimeEntity.AddTime == DateTime.MinValue)
            {
                throw new BizException("内置数据，不能删除");
            }
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 删除完成处理
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public Task<bool> DeletedAsync(IServiceProvider serviceProvider)
        => Task.FromResult(false);
}