namespace Dry.Domain.Entities;

/// <summary>
/// 领域事件集合
/// </summary>
public interface IEvents
{
    /// <summary>
    /// 获取事件
    /// </summary>
    /// <returns></returns>
    IEnumerable<EventBase> GetEvent();

    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="eventItem"></param>
    void AddEvent(EventBase eventItem);

    /// <summary>
    /// 添加事件（检查重复）
    /// </summary>
    /// <param name="eventItem"></param>
    void AddEventIfAbsent(EventBase eventItem);

    /// <summary>
    /// 清除事件
    /// </summary>
    void ClearEvent();
}