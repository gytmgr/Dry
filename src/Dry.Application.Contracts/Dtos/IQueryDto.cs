namespace Dry.Application.Contracts.Dtos;

/// <summary>
/// 查询dto
/// </summary>
public interface IQueryDto
{
}

/// <summary>
/// 查询dto
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class QueryDto<TKey> : IQueryDto
{
    /// <summary>
    /// 系统id
    /// </summary>
    public TKey? Id { get; set; }

    /// <summary>
    /// 系统id不等于
    /// </summary>
    public TKey? IdNotEqual { get; set; }

    /// <summary>
    /// 系统id
    /// </summary>
    public TKey[]? Ids { get; set; }

    /// <summary>
    /// 系统id不等于
    /// </summary>
    public TKey[]? IdsNotEqual { get; set; }
}