using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
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
        ApplicationController<TResult>,
        IApplicationQueryService<TResult, TQuery>
        where TResult : IResultDto
        where TQuery : IQueryDto
    {
        /// <summary>
        /// 应用服务
        /// </summary>
        protected readonly IApplicationQueryService<TResult, TQuery> _applicationQueryService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationQueryService"></param>
        public ApplicationQueryController(IApplicationQueryService<TResult, TQuery> applicationQueryService) : base(applicationQueryService)
        {
            _applicationQueryService = applicationQueryService;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Any")]
        public async Task<bool> AnyAsync([FromQuery] TQuery queryDto)
        {
            return await _applicationQueryService.AnyAsync(queryDto);
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Count")]
        public async Task<int> CountAsync([FromQuery] TQuery queryDto)
        {
            return await _applicationQueryService.CountAsync(queryDto);
        }

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("First")]
        public async Task<TResult> FirstAsync([FromQuery] TQuery queryDto)
        {
            return await _applicationQueryService.FirstAsync(queryDto);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TResult[]> ArrayAsync([FromQuery] TQuery queryDto)
        {
            return await _applicationQueryService.ArrayAsync(queryDto);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Paged")]
        public async Task<PagedResultDto<TResult>> PagedArrayAsync([FromQuery] PagedQueryDto<TQuery> queryDto)
        {
            return await _applicationQueryService.PagedArrayAsync(queryDto);
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
        /// 应用服务
        /// </summary>
        protected readonly IApplicationQueryService<TResult, TQuery, TCreate> _applicationQueryCreateService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationQueryCreateService"></param>
        public ApplicationQueryController(IApplicationQueryService<TResult, TQuery, TCreate> applicationQueryCreateService) : base(applicationQueryCreateService)
        {
            _applicationQueryCreateService = applicationQueryCreateService;
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TResult> CreateAsync([FromBody] TCreate createDto)
        {
            return await _applicationQueryCreateService.CreateAsync(createDto);
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
        /// 应用服务
        /// </summary>
        protected readonly IApplicationQueryService<TResult, TQuery, TCreate, TKey> _applicationQueryDeleteService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationQueryDeleteService"></param>
        public ApplicationQueryController(IApplicationQueryService<TResult, TQuery, TCreate, TKey> applicationQueryDeleteService) : base(applicationQueryDeleteService)
        {
            _applicationQueryDeleteService = applicationQueryDeleteService;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<TResult> FindAsync(TKey id)
        {
            return await _applicationQueryDeleteService.FindAsync(id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<TResult> DeleteAsync(TKey id)
        {
            return await _applicationQueryDeleteService.DeleteAsync(id);
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
        /// 应用服务
        /// </summary>
        protected readonly IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey> _applicationQueryEditService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationQueryEditService"></param>
        public ApplicationQueryController(IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey> applicationQueryEditService) : base(applicationQueryEditService)
        {
            _applicationQueryEditService = applicationQueryEditService;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<TResult> EditAsync(TKey id, [FromBody] TEdit editDto)
        {
            return await _applicationQueryEditService.EditAsync(id, editDto);
        }
    }
}