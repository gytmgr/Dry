using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Api
{
    /// <summary>
    /// 应用控制器
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ApplicationController<TResult> :
        DryController,
        IApplicationService<TResult>
        where TResult : IResultDto
    {
        private IApplicationService<TResult> _appService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="appService"></param>
        public ApplicationController(IApplicationService<TResult> appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        public virtual async Task<int> CountAsync()
        {
            return await _appService.CountAsync();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<TResult[]> ArrayAsync()
        {
            return await _appService.ArrayAsync();
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Paged")]
        public virtual async Task<PagedResultDto<TResult>> ArrayAsync([FromQuery] PagedQueryDto queryDto)
        {
            return await _appService.ArrayAsync(queryDto);
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
        private IApplicationService<TResult, TCreate> _appService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="appService"></param>
        public ApplicationController(IApplicationService<TResult, TCreate> appService) : base(appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<TResult> CreateAsync([FromBody] TCreate createDto)
        {
            return await _appService.CreateAsync(createDto);
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
        private IApplicationService<TResult, TCreate, TKey> _appService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="appService"></param>
        public ApplicationController(IApplicationService<TResult, TCreate, TKey> appService) : base(appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<TResult> FindAsync(TKey id)
        {
            return await _appService.FindAsync(id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<TResult> DeleteAsync(TKey id)
        {
            return await _appService.DeleteAsync(id);
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
        private IApplicationService<TResult, TCreate, TEdit, TKey> _appService;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="appService"></param>
        public ApplicationController(IApplicationService<TResult, TCreate, TEdit, TKey> appService) : base(appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<TResult> EditAsync(TKey id, [FromBody] TEdit editDto)
        {
            return await _appService.EditAsync(id, editDto);
        }
    }
}