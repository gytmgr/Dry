using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Api
{
    /// <summary>
    /// 查询应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class ApplicationQueryController<TService, TResult, TQuery> :
        ApplicationController<TService>,
        IApplicationQueryService<TResult, TQuery>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TService : IApplicationQueryService<TResult, TQuery>
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Any")]
        public virtual async Task<bool> AnyAsync([FromQuery] TQuery queryDto)
        {
            return await AppService.AnyAsync(queryDto);
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Count")]
        public virtual async Task<int> CountAsync([FromQuery] TQuery queryDto)
        {
            return await AppService.CountAsync(queryDto);
        }

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("First")]
        public virtual async Task<TResult> FirstAsync([FromQuery] TQuery queryDto)
        {
            return await AppService.FirstAsync(queryDto);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<TResult[]> ArrayAsync([FromQuery] TQuery queryDto)
        {
            return await AppService.ArrayAsync(queryDto);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Paged")]
        public virtual async Task<PagedResult<TResult>> ArrayAsync([FromQuery] PagedQuery<TQuery> queryDto)
        {
            return await AppService.ArrayAsync(queryDto);
        }
    }

    /// <summary>
    /// 查增应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationQueryController<TService, TResult, TQuery, TCreate> :
        ApplicationQueryController<TService, TResult, TQuery>,
        IApplicationQueryService<TResult, TQuery, TCreate>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
        where TService : IApplicationQueryService<TResult, TQuery, TCreate>
    {
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<TResult> CreateAsync([FromBody] TCreate createDto)
        {
            return await AppService.CreateAsync(createDto);
        }
    }

    /// <summary>
    /// 查增删应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryController<TService, TResult, TQuery, TCreate, TKey> :
        ApplicationQueryController<TService, TResult, TQuery, TCreate>,
        IApplicationQueryService<TResult, TQuery, TCreate, TKey>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
        where TService : IApplicationQueryService<TResult, TQuery, TCreate, TKey>
    {
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<TResult> FindAsync(TKey id)
        {
            return await AppService.FindAsync(id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<TResult> DeleteAsync(TKey id)
        {
            return await AppService.DeleteAsync(id);
        }
    }

    /// <summary>
    /// 查增删改应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryController<TService, TResult, TQuery, TCreate, TEdit, TKey> :
        ApplicationQueryController<TService, TResult, TQuery, TCreate, TKey>,
        IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
        where TService : IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey>
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
            return await AppService.EditAsync(id, editDto);
        }
    }
}