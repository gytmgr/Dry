using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dry.Application.Mvc
{
    /// <summary>
    /// 查询控制器基类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class DryQueryController<TResult, TQuery> : ControllerBase
        where TResult : IResultDto
        where TQuery : IQueryDto
    {
        /// <summary>
        /// 应用服务接口
        /// </summary>
        protected readonly IApplicationQueryService<TResult, TQuery> _applicationService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationService"></param>
        public DryQueryController(IApplicationQueryService<TResult, TQuery> applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Any")]
        public virtual async Task<Result<int, bool>> AnyGetAsync([FromQuery] TQuery queryDto)
        {
            var data = await _applicationService.AnyAsync(queryDto);
            return Result<int, bool>.Create(1, data);
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Count")]
        public virtual async Task<Result<int, int>> CountGetAsync([FromQuery] TQuery queryDto)
        {
            var data = await _applicationService.CountAsync(queryDto);
            return Result<int, int>.Create(1, data);
        }

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("First")]
        public virtual async Task<Result<int, TResult>> FirstGetAsync([FromQuery] TQuery queryDto)
        {
            var data = await _applicationService.FirstAsync(queryDto);
            return Result<int, TResult>.Create(1, data);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<Result<int, TResult[]>> GetAsync([FromQuery] TQuery queryDto)
        {
            var data = await _applicationService.ArrayAsync(queryDto);
            return Result<int, TResult[]>.Create(1, data);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Paged")]
        public virtual async Task<Result<int, PagedResultDto<TResult>>> PagedGetAsync([FromQuery] PagedQueryDto<TQuery> queryDto)
        {
            var data = await _applicationService.ArrayAsync(queryDto);
            return Result<int, PagedResultDto<TResult>>.Create(1, data);
        }
    }

    /// <summary>
    /// 查增控制器基类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class DryQueryController<TResult, TQuery, TCreate> : DryQueryController<TResult, TQuery>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 应用服务接口
        /// </summary>
        protected readonly IApplicationQueryService<TResult, TQuery, TCreate> _applicationCreateService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationCreateService"></param>
        public DryQueryController(IApplicationQueryService<TResult, TQuery, TCreate> applicationCreateService) : base(applicationCreateService)
        {
            _applicationCreateService = applicationCreateService;
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<Result<int, TResult>> PostAsync([FromBody] TCreate createDto)
        {
            var data = await _applicationCreateService.CreateAsync(createDto);
            return Result<int, TResult>.Create(1, data);
        }
    }

    /// <summary>
    /// 查增删控制器基类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class DryQueryController<TResult, TQuery, TCreate, TKey> : DryQueryController<TResult, TQuery, TCreate>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 应用服务接口
        /// </summary>
        protected readonly IApplicationQueryService<TResult, TQuery, TCreate, TKey> _applicationDeleteService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationDeleteService"></param>
        public DryQueryController(IApplicationQueryService<TResult, TQuery, TCreate, TKey> applicationDeleteService) : base(applicationDeleteService)
        {
            _applicationDeleteService = applicationDeleteService;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<Result<int, TResult>> GetAsync(TKey id)
        {
            var data = await _applicationDeleteService.FindAsync(id);
            return Result<int, TResult>.Create(1, data);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<Result<int, TResult>> DeleteAsync(TKey id)
        {
            var data = await _applicationDeleteService.DeleteAsync(id);
            return Result<int, TResult>.Create(1, data);
        }
    }

    /// <summary>
    /// 查增删改控制器基类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class DryQueryController<TResult, TQuery, TCreate, TEdit, TKey> : DryQueryController<TResult, TQuery, TCreate, TKey>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 应用服务接口
        /// </summary>
        protected readonly IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey> _applicationEditService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationEditService"></param>
        public DryQueryController(IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey> applicationEditService) : base(applicationEditService)
        {
            _applicationEditService = applicationEditService;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<Result<int, TResult>> PutAsync(TKey id, [FromBody] TEdit editDto)
        {
            var data = await _applicationEditService.EditAsync(id, editDto);
            return Result<int, TResult>.Create(1, data);
        }
    }
}