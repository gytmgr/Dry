using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using Dry.Core.Utilities;
using Dry.Domain;
using Dry.Domain.Entities;
using Dry.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dry.Application.Services
{
    /// <summary>
    /// 查询应用服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class ApplicationQueryService<TEntity, TResult, TQuery> :
        ApplicationService<TEntity>,
        IApplicationQueryService<TResult, TQuery>
        where TEntity : IAggregateRoot, IBoundedContext
        where TResult : IResultDto
        where TQuery : IQueryDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationQueryService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 获取属性加载表达式
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, dynamic>>[] GetPropertyLoads(TQuery queryDto) => null;

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>>[] GetPredicates(TQuery queryDto) => null;

        /// <summary>
        /// 获取排序表达式
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        protected virtual (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] GetOrderBys(TQuery queryDto) => null;

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(TQuery queryDto)
        {
            var predicates = GetPredicates(queryDto);
            return await _repository.AnyAsync(predicates);
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(TQuery queryDto)
        {
            var predicates = GetPredicates(queryDto);
            return await _repository.CountAsync(predicates);
        }

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync(TQuery queryDto)
        {
            var propertyLoads = GetPropertyLoads(queryDto);
            var predicates = GetPredicates(queryDto);
            var orderBys = GetOrderBys(queryDto);
            var entity = await _repository.FirstAsync(predicates, propertyLoads, orderBys);
            if (entity != null)
            {
                return _mapper.Map<TResult>(entity);
            }
            return default;
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ArrayAsync(TQuery queryDto)
        {
            var propertyLoads = GetPropertyLoads(queryDto);
            var predicates = GetPredicates(queryDto);
            var orderBys = GetOrderBys(queryDto);
            var entities = await _repository.ToArrayAsync(predicates, propertyLoads, orderBys);
            return _mapper.Map<TResult[]>(entities);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<PagedResult<TResult>> ArrayAsync([NotNull] PagedQuery<TQuery> queryDto)
        {
            var propertyLoads = GetPropertyLoads(queryDto.Param);
            var predicates = GetPredicates(queryDto.Param);
            var orderBys = GetOrderBys(queryDto.Param);
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
    /// 查增应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationQueryService<TBoundedContext, TEntity, TResult, TQuery, TCreate> :
        ApplicationQueryService<TEntity, TResult, TQuery>,
        IApplicationQueryService<TResult, TQuery, TCreate>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot, TBoundedContext
        where TResult : IResultDto
        where TQuery : IQueryDto
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
        public ApplicationQueryService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _unitOfWork = serviceProvider.GetService(typeof(IUnitOfWork<TBoundedContext>)) as IUnitOfWork<TBoundedContext>;
        }

        /// <summary>
        /// 配置实体新建数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task SetCreateEntityAsync(TEntity entity, TCreate createDto)
        {
            if (entity is ICreate create)
            {
                await create.CreateAsync(_serviceProvider);
            }
            else
            {
                if (entity is IHasAddTime addTimeEntity)
                {
                    addTimeEntity.AddTime = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> CreateAsync([NotNull] TCreate createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
            await SetCreateEntityAsync(entity, createDto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TResult>(entity);
        }
    }

    /// <summary>
    /// 查增删应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryService<TBoundedContext, TEntity, TResult, TQuery, TCreate, TKey> :
        ApplicationQueryService<TBoundedContext, TEntity, TResult, TQuery, TCreate>,
        IApplicationQueryService<TResult, TQuery, TCreate, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationQueryService(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        protected override Expression<Func<TEntity, bool>>[] GetPredicates(TQuery queryDto)
        {
            var predicates = base.GetPredicates(queryDto)?.ToList() ?? new List<Expression<Func<TEntity, bool>>>();
            if (queryDto is not null)
            {
                if (queryDto.Id is not null && !queryDto.Id.Equals(default(TKey)))
                {
                    predicates.Add(x => x.Id.Equals(queryDto.Id));
                }
                if (queryDto.IdNotEqual is not null && !queryDto.IdNotEqual.Equals(default(TKey)))
                {
                    predicates.Add(x => !x.Id.Equals(queryDto.IdNotEqual));
                }
                if (queryDto.Ids is not null)
                {
                    predicates.Add(x => queryDto.Ids.Contains(x.Id));
                }
                if (queryDto.IdsNotEqual is not null)
                {
                    predicates.Add(x => !queryDto.IdsNotEqual.Contains(x.Id));
                }
            }
            return predicates.ToArray();
        }

        /// <summary>
        /// 获取排序表达式
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        protected override (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] GetOrderBys(TQuery queryDto)
        {
            return new (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] { (true, x => x.Id) };
        }

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
                throw new NullDataBizException();
            }
            if (entity is IDelete delete)
            {
                await delete.DeleteAsync(_serviceProvider);
            }
            await _repository.RemoveAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TResult>(entity);
        }
    }

    /// <summary>
    /// 查增删改应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryService<TBoundedContext, TEntity, TResult, TQuery, TCreate, TEdit, TKey> :
        ApplicationQueryService<TBoundedContext, TEntity, TResult, TQuery, TCreate, TKey>,
        IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationQueryService(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

        /// <summary>
        /// 配置实体编辑数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        protected virtual async Task SetEditEntityAsync(TEntity entity, TEdit editDto)
        {
            if (entity is IEdit edit)
            {
                await edit.EditAsync(_serviceProvider);
            }
            else
            {
                if (entity is IHasUpdateTime updateTimeEntity)
                {
                    updateTimeEntity.UpdateTime = DateTime.Now;
                }
            }
        }

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
                throw new NullDataBizException();
            }
            _mapper.Map(editDto, entity);
            await SetEditEntityAsync(entity, editDto);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TResult>(entity);
        }
    }
}