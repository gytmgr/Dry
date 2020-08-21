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
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    public abstract class ApplicationService<TEntity, TResult, TQuery, TCreate, TEdit> :
        IApplicationService<TResult, TQuery, TCreate, TEdit>
        where TEntity : IAggregateRoot, IBoundedContext
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
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
        /// 根据查询对象获取linq表达式
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetQueryable([NotNull] IQueryable<TEntity> queryable, TQuery query)
        {
            return queryable;
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
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<PagedResultDto<TResult>> PagedArrayAsync(PagedQueryDto query)
        {
            var queryable = _repository.GetQueryable();
            var total = await _repository.CountAsync(queryable);
            var entities = await _repository.ToArrayAsync(queryable.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize));
            return new PagedResultDto<TResult>
            {
                Total = total,
                Items = _mapper.Map<TResult[]>(entities)
            };
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(TQuery query)
        {
            var queryable = _repository.GetQueryable();
            if (query != null)
            {
                queryable = GetQueryable(queryable, query);
            }
            return await _repository.AnyAsync(queryable);
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(TQuery query)
        {
            var queryable = _repository.GetQueryable();
            if (query != null)
            {
                queryable = GetQueryable(queryable, query);
            }
            return await _repository.CountAsync(queryable);
        }

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync(TQuery query)
        {
            var queryable = _repository.GetQueryable();
            if (query != null)
            {
                queryable = GetQueryable(queryable, query);
            }
            var entity = await _repository.FirstAsync(queryable);
            if (entity != null)
            {
                return _mapper.Map<TResult>(entity);
            }
            return default;
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ArrayAsync(TQuery query)
        {
            var queryable = _repository.GetQueryable();
            if (query != null)
            {
                queryable = GetQueryable(queryable, query);
            }
            var entities = await _repository.ToArrayAsync(queryable);
            return _mapper.Map<TResult[]>(entities);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<PagedResultDto<TResult>> PagedArrayAsync(PagedQueryDto<TQuery> query)
        {
            var queryable = _repository.GetQueryable();
            if (query.Param != null)
            {
                queryable = GetQueryable(queryable, query.Param);
            }
            var total = await _repository.CountAsync(queryable);
            var entities = await _repository.ToArrayAsync(queryable.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize));
            return new PagedResultDto<TResult>
            {
                Total = total,
                Items = _mapper.Map<TResult[]>(entities)
            };
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> CreateAsync(TCreate createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
            await _repository.AddAsync(entity, true);
            return _mapper.Map<TResult>(entity);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> EditAsync(object id, TEdit editDto)
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

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> EditAsync(object[] ids, TEdit editDto)
        {
            var entity = await _repository.FindAsync(ids);
            if (entity != null)
            {
                _mapper.Map(editDto, entity);
                await _repository.UpdateAsync(entity, true);
                return _mapper.Map<TResult>(entity);
            }
            return default;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TResult> DeleteAsync(object id)
        {
            var entity = await _repository.FindAsync(id);
            if (entity != null)
            {
                await _repository.RemoveAsync(entity, true);
                return _mapper.Map<TResult>(entity);
            }
            return default;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual async Task<TResult> DeleteAsync(object[] ids)
        {
            var entity = await _repository.FindAsync(ids);
            if (entity != null)
            {
                await _repository.RemoveAsync(entity, true);
                return _mapper.Map<TResult>(entity);
            }
            return default;
        }
    }

    /// <summary>
    /// 应用服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    public abstract class ApplicationService<TEntity, TResult, TCreate, TEdit> :
        ApplicationService<TEntity, TResult, IQueryDto, TCreate, TEdit>
        where TEntity : IAggregateRoot, IBoundedContext
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
        {
        }
    }

    /// <summary>
    /// 应用服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class ApplicationService<TEntity, TResult, TQuery> :
        ApplicationService<TEntity, TResult, TQuery, ICreateDto, IEditDto>
        where TEntity : IAggregateRoot, IBoundedContext
        where TResult : IResultDto
        where TQuery : IQueryDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ApplicationService(IMapper mapper, IRepository<TEntity> repository) : base(mapper, repository)
        {
        }
    }

    /// <summary>
    /// 应用服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ApplicationService<TEntity, TResult> :
        ApplicationService<TEntity, TResult, IQueryDto>
        where TEntity : IAggregateRoot, IBoundedContext
        where TResult : IResultDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ApplicationService(IMapper mapper, IRepository<TEntity> repository) : base(mapper, repository)
        {
        }
    }
}