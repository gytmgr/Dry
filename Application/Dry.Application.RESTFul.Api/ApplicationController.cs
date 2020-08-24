using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Api
{
    /// <summary>
    /// 应用控制器
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ApplicationController<TResult> :
        ControllerBase,
        IApplicationService<TResult>
        where TResult : IResultDto
    {
        /// <summary>
        /// 应用服务
        /// </summary>
        protected readonly IApplicationService<TResult> _applicationService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationService"></param>
        public ApplicationController(IApplicationService<TResult> applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllCount")]
        public async Task<int> CountAsync()
        {
            return await _applicationService.CountAsync();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet("All")]
        public async Task<TResult[]> ArrayAsync()
        {
            return await _applicationService.ArrayAsync();
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("AllPaged")]
        public async Task<PagedResultDto<TResult>> PagedArrayAsync([FromQuery] PagedQueryDto queryDto)
        {
            return await _applicationService.PagedArrayAsync(queryDto);
        }
    }

    /// <summary>
    /// 新增应用控制器
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationController<TResult, TCreate> :
        ApplicationController<TResult>,
        IApplicationService<TResult, TCreate>
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 应用服务
        /// </summary>
        protected readonly IApplicationService<TResult, TCreate> _applicationCreateService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationCreateService"></param>
        public ApplicationController(IApplicationService<TResult, TCreate> applicationCreateService) : base(applicationCreateService)
        {
            _applicationCreateService = applicationCreateService;
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TResult> CreateAsync([FromBody] TCreate createDto)
        {
            return await _applicationCreateService.CreateAsync(createDto);
        }
    }

    /// <summary>
    /// 增删应用控制器
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationController<TResult, TCreate, TKey> :
        ApplicationController<TResult, TCreate>,
        IApplicationService<TResult, TCreate, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 应用服务
        /// </summary>
        protected readonly IApplicationService<TResult, TCreate, TKey> _applicationDeleteService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationDeleteService"></param>
        public ApplicationController(IApplicationService<TResult, TCreate, TKey> applicationDeleteService) : base(applicationDeleteService)
        {
            _applicationDeleteService = applicationDeleteService;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<TResult> FindAsync(TKey id)
        {
            return await _applicationDeleteService.FindAsync(id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<TResult> DeleteAsync(TKey id)
        {
            return await _applicationDeleteService.DeleteAsync(id);
        }
    }

    /// <summary>
    /// 增删改应用控制器
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationController<TResult, TCreate, TEdit, TKey> :
        ApplicationController<TResult, TCreate, TKey>,
        IApplicationService<TResult, TCreate, TEdit, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 应用服务
        /// </summary>
        protected readonly IApplicationService<TResult, TCreate, TEdit, TKey> _applicationEditService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="applicationEditService"></param>
        public ApplicationController(IApplicationService<TResult, TCreate, TEdit, TKey> applicationEditService) : base(applicationEditService)
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
        public async Task<TResult> EditAsync(TKey id, [FromBody] TEdit editDto)
        {
            return await _applicationEditService.EditAsync(id, editDto);
        }
    }
}