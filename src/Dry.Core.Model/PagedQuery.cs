namespace Dry.Core.Model;

/// <summary>
/// 分页查询
/// </summary>
public class PagedQuery
{
    /// <summary>
    /// 当前页
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 每页条目数
    /// </summary>
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// 分页查询
/// </summary>
/// <typeparam name="TQuery"></typeparam>
public class PagedQuery<TQuery> : PagedQuery
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public TQuery? Param { get; set; }
}