using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Api
{
    /// <summary>
    /// 查询应用控制器
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class ApplicationQueryController<TResult, TQuery> :
        DryController,
        IApplicationQueryService<TResult, TQuery>
        where TResult : IResultDto
        where TQuery : IQueryDto
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Any")]
        public virtual async Task<bool> AnyAsync([FromQuery] TQuery queryDto)
        {
            return await Service<IApplicationQueryService<TResult, TQuery>>().AnyAsync(queryDto);
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Count")]
        public virtual async Task<int> CountAsync([FromQuery] TQuery queryDto)
        {
            return await Service<IApplicationQueryService<TResult, TQuery>>().CountAsync(queryDto);
        }

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("First")]
        public virtual async Task<TResult> FirstAsync([FromQuery] TQuery queryDto)
        {
            return await Service<IApplicationQueryService<TResult, TQuery>>().FirstAsync(queryDto);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<TResult[]> ArrayAsync([FromQuery] TQuery queryDto)
        {
            return await Service<IApplicationQueryService<TResult, TQuery>>().ArrayAsync(queryDto);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Paged")]
        public virtual async Task<PagedResultDto<TResult>> ArrayAsync([FromQuery] PagedQueryDto<TQuery> queryDto)
        {
            return await Service<IApplicationQueryService<TResult, TQuery>>().ArrayAsync(queryDto);
        }
    }

    /// <summary>
    /// 查增应用控制器
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationQueryController<TResult, TQuery, TCreate> :
        ApplicationQueryController<TResult, TQuery>,
        IApplicationQueryService<TResult, TQuery, TCreate>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<TResult> CreateAsync([FromBody] TCreate createDto)
        {
            return await Service<IApplicationQueryService<TResult, TQuery, TCreate>>().CreateAsync(createDto);
        }
    }

    /// <summary>
    /// 查增删应用控制器
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryController<TResult, TQuery, TCreate, TKey> :
        ApplicationQueryController<TResult, TQuery, TCreate>,
        IApplicationQueryService<TResult, TQuery, TCreate, TKey>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<TResult> FindAsync(TKey id)
        {
            return await Service<IApplicationQueryService<TResult, TQuery, TCreate, TKey>>().FindAsync(id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<TResult> DeleteAsync(TKey id)
        {
            return await Service<IApplicationQueryService<TResult, TQuery, TCreate, TKey>>().DeleteAsync(id);
        }
    }

    /// <summary>
    /// 查增删改应用控制器
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryController<TResult, TQuery, TCreate, TEdit, TKey> :
        ApplicationQueryController<TResult, TQuery, TCreate, TKey>,
        IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<TResult> EditAsync(TKey id, [FromBody] TEdit editDto)
        {
            return await Service<IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey>>().EditAsync(id, editDto);
        }
    }
}