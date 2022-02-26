using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Domain;
using Dry.Domain.Entities;
using Dry.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Services
{
    /// <summary>
    /// 基础查、增应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationCreateService<TBoundedContext, TEntity, TResult, TCreate> :
        ApplicationService<TEntity, TResult>,
        IApplicationCreateService<TResult, TCreate>
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
        public ApplicationCreateService(IServiceProvider serviceProvider) : base(serviceProvider)
            => _unitOfWork = serviceProvider.GetService<IUnitOfWork<TBoundedContext>>();

        /// <summary>
        /// 配置实体新建数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task SetCreateEntityAsync(TEntity entity, TCreate createDto)
        {
            if (entity is ICreate createEntity)
            {
                await createEntity.CreateAsync(_serviceProvider);
            }
            if (entity is IHasAddTime hasAddTimeEntity && hasAddTimeEntity.AddTime == default)
            {
                hasAddTimeEntity.AddTime = DateTime.Now;
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
            await CreatedAsync(entity, createDto);
            return _mapper.Map<TResult>(entity);
        }

        /// <summary>
        /// 新建完成处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task CreatedAsync(TEntity entity, TCreate createDto)
        {
            if (entity is ICreate createEntity && await createEntity.CreatedAsync(_serviceProvider))
            {
                await _unitOfWork.CompleteAsync();
            }
        }
    }

    /// <summary>
    /// 基础查、增应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationCreateService<TBoundedContext, TEntity, TResult, TCreate, TKey> :
        ApplicationCreateService<TBoundedContext, TEntity, TResult, TCreate>,
        IApplicationCreateService<TResult, TCreate, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationCreateService(IServiceProvider serviceProvider) : base(serviceProvider)
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
    /// 条件查、增应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationQueryCreateService<TBoundedContext, TEntity, TResult, TQuery, TCreate> :
        ApplicationQueryService<TEntity, TResult, TQuery>,
        IApplicationQueryCreateService<TResult, TQuery, TCreate>
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
        public ApplicationQueryCreateService(IServiceProvider serviceProvider) : base(serviceProvider)
            => _unitOfWork = serviceProvider.GetService<IUnitOfWork<TBoundedContext>>();

        /// <summary>
        /// 配置实体新建数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task SetCreateEntityAsync(TEntity entity, TCreate createDto)
        {
            if (entity is ICreate createEntity)
            {
                await createEntity.CreateAsync(_serviceProvider);
            }
            if (entity is IHasAddTime hasAddTimeEntity && hasAddTimeEntity.AddTime == default)
            {
                hasAddTimeEntity.AddTime = DateTime.Now;
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
            await CreatedAsync(entity, createDto);
            return _mapper.Map<TResult>(entity);
        }

        /// <summary>
        /// 新建完成处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task CreatedAsync(TEntity entity, TCreate createDto)
        {
            if (entity is ICreate createEntity && await createEntity.CreatedAsync(_serviceProvider))
            {
                await _unitOfWork.CompleteAsync();
            }
        }
    }

    /// <summary>
    /// 条件查、增应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryCreateService<TBoundedContext, TEntity, TResult, TQuery, TCreate, TKey> :
        ApplicationQueryService<TEntity, TResult, TQuery, TKey>,
        IApplicationQueryCreateService<TResult, TQuery, TCreate, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
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
        public ApplicationQueryCreateService(IServiceProvider serviceProvider) : base(serviceProvider)
            => _unitOfWork = serviceProvider.GetService<IUnitOfWork<TBoundedContext>>();

        /// <summary>
        /// 配置实体新建数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task SetCreateEntityAsync(TEntity entity, TCreate createDto)
        {
            if (entity is ICreate createEntity)
            {
                await createEntity.CreateAsync(_serviceProvider);
            }
            if (entity is IHasAddTime hasAddTimeEntity && hasAddTimeEntity.AddTime == default)
            {
                hasAddTimeEntity.AddTime = DateTime.Now;
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
            await CreatedAsync(entity, createDto);
            return _mapper.Map<TResult>(entity);
        }

        /// <summary>
        /// 新建完成处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task CreatedAsync(TEntity entity, TCreate createDto)
        {
            if (entity is ICreate createEntity && await createEntity.CreatedAsync(_serviceProvider))
            {
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}