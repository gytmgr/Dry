using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using Dry.Domain;
using Dry.Domain.Entities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Services
{
    /// <summary>
    /// 基础查、增、删应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationCreateDeleteService<TBoundedContext, TEntity, TResult, TCreate, TKey> :
        ApplicationCreateService<TBoundedContext, TEntity, TResult, TCreate, TKey>,
        IApplicationCreateDeleteService<TResult, TCreate, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationCreateDeleteService(IServiceProvider serviceProvider) : base(serviceProvider)
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
    /// 条件查、增、删应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryCreateDeleteService<TBoundedContext, TEntity, TResult, TQuery, TCreate, TKey> :
        ApplicationQueryCreateService<TBoundedContext, TEntity, TResult, TQuery, TCreate, TKey>,
        IApplicationQueryCreateDeleteService<TResult, TQuery, TCreate, TKey>
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
        public ApplicationQueryCreateDeleteService(IServiceProvider serviceProvider) : base(serviceProvider)
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