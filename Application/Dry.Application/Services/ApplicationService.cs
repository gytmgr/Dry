using AutoMapper;
using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using Dry.Domain;
using Dry.Domain.Entities;
using Dry.Domain.Repositories;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Dry.Application.Services
{
    /// <summary>
    /// 应用服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ApplicationService<TEntity> where TEntity : IAggregateRoot, IBoundedContext
    {
        /// <summary>
        /// 服务生成器
        /// </summary>
        protected readonly IServiceProvider _serviceProvider;

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
        /// <param name="serviceProvider"></param>
        public ApplicationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _mapper = serviceProvider.GetService(typeof(IMapper)) as IMapper;
            _repository = serviceProvider.GetService(typeof(IRepository<TEntity>)) as IRepository<TEntity>;
        }

        /// <summary>
        /// 获取仓储
        /// </summary>
        /// <typeparam name="TOtherEntity"></typeparam>
        /// <returns></returns>
        protected IRepository<TOtherEntity> Repository<TOtherEntity>() where TOtherEntity : IEntity, IBoundedContext
            => _serviceProvider.GetService(typeof(IRepository<TOtherEntity>)) as IRepository<TOtherEntity>;
    }

    /// <summary>
    /// 应用服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ApplicationService<TEntity, TResult> :
        ApplicationService<TEntity>,
        IApplicationService<TResult>
        where TEntity : IAggregateRoot, IBoundedContext
        where TResult : IResultDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
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
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationService<TBoundedContext, TEntity, TResult, TCreate> :
        ApplicationService<TEntity, TResult>,
        IApplicationService<TResult, TCreate>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot, TBoundedContext
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        protected readonly IUnitOfWork<TBoundedContext> _unitOfWork;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _unitOfWork = serviceProvider.GetService(typeof(IUnitOfWork<TBoundedContext>)) as IUnitOfWork<TBoundedContext>;
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> CreateAsync([NotNull] TCreate createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TResult>(entity);
        }
    }

    /// <summary>
    /// 增删应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationService<TBoundedContext, TEntity, TResult, TCreate, TKey> :
        ApplicationService<TBoundedContext, TEntity, TResult, TCreate>,
        IApplicationService<TResult, TCreate, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
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
            if (entity == null)
            {
                throw new BizException("数据不存在");
            }
            await _repository.RemoveAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TResult>(entity);
        }
    }

    /// <summary>
    /// 增删改应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationService<TBoundedContext, TEntity, TResult, TCreate, TEdit, TKey> :
        ApplicationService<TBoundedContext, TEntity, TResult, TCreate, TKey>,
        IApplicationService<TResult, TCreate, TEdit, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
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
            if (entity == null)
            {
                throw new BizException("数据不存在");
            }
            _mapper.Map(editDto, entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TResult>(entity);
        }
    }
}