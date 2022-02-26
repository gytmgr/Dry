using AutoMapper;
using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using Dry.Core.Utilities;
using Dry.Domain;
using Dry.Domain.Entities;
using Dry.Domain.Extensions;
using Dry.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
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
            _mapper = serviceProvider.GetService<IMapper>();
            _repository = serviceProvider.GetRepository<TEntity>();
        }

        /// <summary>
        /// 获取仓储
        /// </summary>
        /// <typeparam name="TOtherEntity"></typeparam>
        /// <returns></returns>
        protected IRepository<TOtherEntity> Repository<TOtherEntity>() where TOtherEntity : IEntity, IBoundedContext
            => _serviceProvider.GetRepository<TOtherEntity>();

        /// <summary>
        /// 获取属性加载表达式
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, dynamic>>[] GetPropertyLoads()
            => null;

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>>[] GetPredicates()
            => null;

        /// <summary>
        /// 获取排序表达式
        /// </summary>
        /// <returns></returns>
        protected virtual (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] GetOrderBys()
            => null;
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
        { }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync()
        {
            var predicates = GetPredicates();
            return await _repository.AnyAsync(predicates);
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync()
        {
            var predicates = GetPredicates();
            return await _repository.CountAsync(predicates);
        }

        /// <summary>
        /// 查询第一条
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync()
        {
            var propertyLoads = GetPropertyLoads();
            var predicates = GetPredicates();
            var orderBys = GetOrderBys();
            var entitiy = await _repository.FirstAsync(predicates, propertyLoads, orderBys);
            return _mapper.Map<TResult>(entitiy);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TResult[]> ArrayAsync()
        {
            var propertyLoads = GetPropertyLoads();
            var predicates = GetPredicates();
            var orderBys = GetOrderBys();
            var entities = await _repository.ToArrayAsync(predicates, propertyLoads, orderBys);
            return _mapper.Map<TResult[]>(entities);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<PagedResult<TResult>> ArrayAsync([NotNull] PagedQuery queryDto)
        {
            var propertyLoads = GetPropertyLoads();
            var predicates = GetPredicates();
            var orderBys = GetOrderBys();
            var total = await _repository.CountAsync(predicates);
            var entities = await _repository.ToArrayAsync(
                queryable => queryable.Where(predicates).OrderBy(orderBys).Skip((queryDto.PageIndex - 1) * queryDto.PageSize).Take(queryDto.PageSize),
                propertyLoads);
            return new PagedResult<TResult>
            {
                Total = total,
                Items = _mapper.Map<TResult[]>(entities)
            };
        }
    }

    /// <summary>
    /// 应用服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationService<TEntity, TResult, TKey> :
        ApplicationService<TEntity, TResult>,
        IApplicationService<TResult, TKey>
        where TEntity : IAggregateRoot<TKey>, IBoundedContext
        where TResult : IResultDto
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
    }

    /// <summary>
    /// 基础查、增、改、删应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationService<TBoundedContext, TEntity, TResult, TCreate, TEdit, TKey> :
        ApplicationCreateEditService<TBoundedContext, TEntity, TResult, TCreate, TEdit, TKey>,
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
        /// 配置实体删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual async Task SetDeleteEntityAsync(TEntity entity)
        {
            if (entity is IDelete deleteEntity)
            {
                await deleteEntity.DeleteAsync(_serviceProvider);
            }
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
                throw new NullDataBizException();
            }
            await SetDeleteEntityAsync(entity);
            await _repository.RemoveAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TResult>(entity);
        }

        /// <summary>
        /// 删除完成处理
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual async Task DeletedAsync(TEntity entity)
        {
            if (entity is IDelete deleteEntity && await deleteEntity.DeletedAsync(_serviceProvider))
            {
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}