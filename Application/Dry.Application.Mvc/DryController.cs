using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dry.Application.Mvc
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class DryController<TResult> : ControllerBase where TResult : IResultDto
    {
        /// <summary>
        /// 应用服务接口
        /// </summary>
        protected readonly IApplicationService<TResult> _applicationService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationService"></param>
        public DryController(IApplicationService<TResult> applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        public virtual async Task<Result<int, int>> CountGetAsync()
        {
            var data = await _applicationService.CountAsync();
            return Result<int, int>.Create(1, data);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<Result<int, TResult[]>> GetAsync()
        {
            var data = await _applicationService.ArrayAsync();
            return Result<int, TResult[]>.Create(1, data);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Paged")]
        public virtual async Task<Result<int, PagedResultDto<TResult>>> PagedGetAsync([FromQuery] PagedQueryDto queryDto)
        {
            var data = await _applicationService.ArrayAsync(queryDto);
            return Result<int, PagedResultDto<TResult>>.Create(1, data);
        }
    }

    /// <summary>
    /// 新增控制器基类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class DryController<TResult, TCreate> : DryController<TResult>
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 应用服务接口
        /// </summary>
        protected readonly IApplicationService<TResult, TCreate> _applicationCreateService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationCreateService"></param>
        public DryController(IApplicationService<TResult, TCreate> applicationCreateService) : base(applicationCreateService)
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
    /// 增删控制器基类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class DryController<TResult, TCreate, TKey> : DryController<TResult, TCreate>
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 应用服务接口
        /// </summary>
        protected readonly IApplicationService<TResult, TCreate, TKey> _applicationDeleteService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationDeleteService"></param>
        public DryController(IApplicationService<TResult, TCreate, TKey> applicationDeleteService) : base(applicationDeleteService)
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
    /// 增删改控制器基类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class DryController<TResult, TCreate, TEdit, TKey> : DryController<TResult, TCreate, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 应用服务接口
        /// </summary>
        protected readonly IApplicationService<TResult, TCreate, TEdit, TKey> _applicationEditService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationEditService"></param>
        public DryController(IApplicationService<TResult, TCreate, TEdit, TKey> applicationEditService) : base(applicationEditService)
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