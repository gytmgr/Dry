using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using Dry.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Api
{
    /// <summary>
    /// 应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public abstract class ApplicationController<TService> : DryController
    {
        /// <summary>
        /// 应用服务
        /// </summary>
        protected virtual TService AppService => Service<TService>();
    }

    /// <summary>
    /// 应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ApplicationController<TService, TResult> :
        ApplicationController<TService>,
        IApplicationService<TResult>
        where TResult : IResultDto
        where TService : IApplicationService<TResult>
    {
        /// <summary>
        /// 数量查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        public virtual async Task<int> CountAsync()
        {
            return await AppService.CountAsync();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<TResult[]> ArrayAsync()
        {
            return await AppService.ArrayAsync();
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Paged")]
        public virtual async Task<PagedResult<TResult>> ArrayAsync([FromQuery] PagedQuery queryDto)
        {
            return await AppService.ArrayAsync(queryDto);
        }
    }

    /// <summary>
    /// 新增应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationController<TService, TResult, TCreate> :
        ApplicationController<TService, TResult>,
        IApplicationService<TResult, TCreate>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TService : IApplicationService<TResult, TCreate>
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
    /// 增删应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationController<TService, TResult, TCreate, TKey> :
        ApplicationController<TService, TResult, TCreate>,
        IApplicationService<TResult, TCreate, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TService : IApplicationService<TResult, TCreate, TKey>
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
    /// 增删改应用控制器
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationController<TService, TResult, TCreate, TEdit, TKey> :
        ApplicationController<TService, TResult, TCreate, TKey>,
        IApplicationService<TResult, TCreate, TEdit, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
        where TService : IApplicationService<TResult, TCreate, TEdit, TKey>
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