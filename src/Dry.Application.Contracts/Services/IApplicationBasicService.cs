namespace Dry.Application.Contracts.Services;

/// <summary>
/// 主键查询应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IFindService<TResult, TKey>
    where TResult : IResultDto
{
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TResult> FindAsync([NotNull] TKey id);
}

/// <summary>
/// 新建应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
public interface ICreateService<TResult, TCreate>
    where TResult : IResultDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    Task<TResult> CreateAsync([NotNull] TCreate createDto);
}

/// <summary>
/// 编辑应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IEditService<TResult, TEdit, TKey>
    where TResult : IResultDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="editDto"></param>
    /// <returns></returns>
    Task<TResult> EditAsync([NotNull] TKey id, [NotNull] TEdit editDto);
}

/// <summary>
/// 删除应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IDeleteService<TResult, TKey>
    where TResult : IResultDto
{
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TResult> DeleteAsync([NotNull] TKey id);
}