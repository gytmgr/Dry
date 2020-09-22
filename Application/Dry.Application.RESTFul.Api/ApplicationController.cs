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
        /// <summary>
        /// 数量查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        public virtual async Task<int> CountAsync()
        {
            return await Service<IApplicationService<TResult>>().CountAsync();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<TResult[]> ArrayAsync()
        {
            return await Service<IApplicationService<TResult>>().ArrayAsync();
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpGet("Paged")]
        public virtual async Task<PagedResultDto<TResult>> ArrayAsync([FromQuery] PagedQueryDto queryDto)
        {
            return await Service<IApplicationService<TResult>>().ArrayAsync(queryDto);
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
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<TResult> CreateAsync([FromBody] TCreate createDto)
        {
            return await Service<IApplicationService<TResult, TCreate>>().CreateAsync(createDto);
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
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<TResult> FindAsync(TKey id)
        {
            return await Service<IApplicationService<TResult, TCreate, TKey>>().FindAsync(id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<TResult> DeleteAsync(TKey id)
        {
            return await Service<IApplicationService<TResult, TCreate, TKey>>().DeleteAsync(id);
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
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<TResult> EditAsync(TKey id, [FromBody] TEdit editDto)
        {
            return await Service<IApplicationService<TResult, TCreate, TEdit, TKey>>().EditAsync(id, editDto);
        }
    }
}