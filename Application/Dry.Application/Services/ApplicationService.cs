using AutoMapper;
using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Domain;
using Dry.Domain.Entities;
using Dry.Domain.Repositories;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Dry.Application.Services
{
    /// <summary>
    /// 应用服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ApplicationService<TEntity, TResult> :
        IApplicationService<TResult>
        where TEntity : IAggregateRoot, IBoundedContext
        where TResult : IResultDto
    {
        /// <summary>
        /// 对象映射
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        /// 仓储
        /// </summary>
        protected readonly IRepository<TEntity> _repository;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ApplicationService(IMapper mapper, IRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TResult[]> ArrayAsync()
        {
            var entities = await _repository.ToArrayAsync();
            return _mapper.Map<TResult[]>(entities);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<PagedResultDto<TResult>> ArrayAsync([NotNull] PagedQueryDto queryDto)
        {
            var queryable = _repository.GetQueryable();
            var total = await _repository.CountAsync(queryable);
            var entities = await _repository.ToArrayAsync(queryable.Skip((queryDto.PageIndex - 1) * queryDto.PageSize).Take(queryDto.PageSize));
            return new PagedResultDto<TResult>
            {
                Total = total,
                Items = _mapper.Map<TResult[]>(entities)
            };
        }
    }

    /// <summary>
    /// 新增应用服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationService<TEntity, TResult, TCreate> :
        ApplicationService<TEntity, TResult>,
        IApplicationService<TResult, TCreate>
        where TEntity : IAggregateRoot, IBoundedContext
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ApplicationService(IMapper mapper, IRepository<TEntity> repository) : base(mapper, repository)
        { }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> CreateAsync([NotNull] TCreate createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
            await _repository.AddAsync(entity, true);
            return _mapper.Map<TResult>(entity);
        }
    }

    /// <summary>
    /// 增删应用服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationService<TEntity, TResult, TCreate, TKey> :
        ApplicationService<TEntity, TResult, TCreate>,
        IApplicationService<TResult, TCreate, TKey>
        where TEntity : IAggregateRoot<TKey>, IBoundedContext
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ApplicationService(IMapper mapper, IRepository<TEntity> repository) : base(mapper, repository)
        { }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FindAsync([NotNull] TKey id)
        {
            var entity = await _repository.FindAsync(id);
            return _mapper.Map<TResult>(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TResult> DeleteAsync([NotNull] TKey id)
        {
            var entity = await _repository.FindAsync(id);
            if (entity != null)
            {
                await _repository.RemoveAsync(entity, true);
                return _mapper.Map<TResult>(entity);
            }
            return default;
        }
    }

    /// <summary>
    /// 增删改应用服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationService<TEntity, TResult, TCreate, TEdit, TKey> :
        ApplicationService<TEntity, TResult, TCreate, TKey>,
        IApplicationService<TResult, TCreate, TEdit, TKey>
        where TEntity : IAggregateRoot<TKey>, IBoundedContext
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ApplicationService(IMapper mapper, IRepository<TEntity> repository) : base(mapper, repository)
        { }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> EditAsync([NotNull] TKey id, [NotNull] TEdit editDto)
        {
            var entity = await _repository.FindAsync(id);
            if (entity != null)
            {
                _mapper.Map(editDto, entity);
                await _repository.UpdateAsync(entity, true);
                return _mapper.Map<TResult>(entity);
            }
            return default;
        }
    }
}