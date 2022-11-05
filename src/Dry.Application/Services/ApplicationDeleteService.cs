using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
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
    /// 基础查、删应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationDeleteService<TBoundedContext, TEntity, TResult, TKey> :
        ApplicationService<TEntity, TResult, TKey>,
        IApplicationDeleteService<TResult, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        protected readonly IUnitOfWork<TBoundedContext> _unitOfWork;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationDeleteService(IServiceProvider serviceProvider) : base(serviceProvider)
            => _unitOfWork = serviceProvider.GetService<IUnitOfWork<TBoundedContext>>();

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
            await DeletedAsync(entity);
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

    /// <summary>
    /// 条件查、删应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryDeleteService<TBoundedContext, TEntity, TResult, TQuery, TKey> :
        ApplicationQueryService<TEntity, TResult, TQuery, TKey>,
        IApplicationQueryDeleteService<TResult, TQuery, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        protected readonly IUnitOfWork<TBoundedContext> _unitOfWork;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationQueryDeleteService(IServiceProvider serviceProvider) : base(serviceProvider)
            => _unitOfWork = serviceProvider.GetService<IUnitOfWork<TBoundedContext>>();

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
            await DeletedAsync(entity);
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