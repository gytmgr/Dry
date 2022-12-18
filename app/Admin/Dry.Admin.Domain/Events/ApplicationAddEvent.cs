namespace Dry.Admin.Domain.Events;

public class ApplicationAddEvent : EventBase
{
    /// <summary>
    /// 添加应用
    /// </summary>
    public Application Application { get; set; }
}