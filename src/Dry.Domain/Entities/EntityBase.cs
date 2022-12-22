namespace Dry.Domain.Entities;

/// <summary>
/// 实体基类
/// </summary>
public abstract class EntityBase : IEvents
{
    private readonly List<EventBase> _events = new();

    /// <summary>
    /// 获取事件
    /// </summary>
    /// <returns></returns>
    public IEnumerable<EventBase> GetEvent()
        => _events;

    /// <summary>
    /// 获取事件
    /// </summary>
    /// <param name="eventItem"></param>
    public void AddEvent(EventBase eventItem)
        => _events.Add(eventItem);

    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="eventItem"></param>
    public void AddEventIfAbsent(EventBase eventItem)
    {
        if (!_events.Contains(eventItem))
        {
            _events.Add(eventItem);
        }
    }

    /// <summary>
    /// 清除事件
    /// </summary>
    public void ClearEvent()
        => _events.Clear();
}