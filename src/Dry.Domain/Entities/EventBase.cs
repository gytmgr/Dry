namespace Dry.Domain.Entities;

/// <summary>
/// 领域事件基类
/// </summary>
public abstract class EventBase : INotification
{
    /// <summary>
    /// 提交前执行
    /// </summary>
    public virtual bool PreExecute { get; set; }
}