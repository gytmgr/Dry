using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using Dry.Core.Utilities;
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
    /// 基础查、改应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationEditService<TBoundedContext, TEntity, TResult, TEdit, TKey> :
        ApplicationService<TEntity, TResult, TKey>,
        IApplicationEditService<TResult, TEdit, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        protected readonly IUnitOfWork<TBoundedContext> _unitOfWork;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationEditService(IServiceProvider serviceProvider) : base(serviceProvider)
            => _unitOfWork = serviceProvider.GetService<IUnitOfWork<TBoundedContext>>();

        /// <summary>
        /// 配置实体编辑数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        protected virtual async Task SetEditEntityAsync(TEntity entity, TEdit editDto)
        {
            if (entity is IEdit editEntity)
            {
                await editEntity.EditAsync(_serviceProvider);
            }
            if (entity is IHasUpdateTime hasUpdateTimeEntity)
            {
                var updateTimeExpression = LinqHelper.GetKeySelector<TEntity, DateTime?>(nameof(IHasUpdateTime.UpdateTime));
                if (!_repository.PropertyModified(entity, updateTimeExpression))
                {
                    hasUpdateTimeEntity.UpdateTime = DateTime.Now;
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

        /// <summary>
        /// 编辑完成处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task CreatedAsync(TEntity entity, TEdit createDto)
        {
            if (entity is IEdit editEntity && await editEntity.EditedAsync(_serviceProvider))
            {
                await _unitOfWork.CompleteAsync();
            }
        }
    }

    /// <summary>
    /// 条件查、改应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryEditService<TBoundedContext, TEntity, TResult, TQuery, TEdit, TKey> :
        ApplicationQueryService<TEntity, TResult, TQuery, TKey>,
        IApplicationQueryEditService<TResult, TQuery, TEdit, TKey>
        where TBoundedContext : IBoundedContext
        where TEntity : IAggregateRoot<TKey>, TBoundedContext
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TEdit : IEditDto
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        protected readonly IUnitOfWork<TBoundedContext> _unitOfWork;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationQueryEditService(IServiceProvider serviceProvider) : base(serviceProvider)
            => _unitOfWork = serviceProvider.GetService<IUnitOfWork<TBoundedContext>>();

        /// <summary>
        /// 配置实体编辑数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        protected virtual async Task SetEditEntityAsync(TEntity entity, TEdit editDto)
        {
            if (entity is IEdit editEntity)
            {
                await editEntity.EditAsync(_serviceProvider);
            }
            if (entity is IHasUpdateTime hasUpdateTimeEntity)
            {
                var updateTimeExpression = LinqHelper.GetKeySelector<TEntity, DateTime?>(nameof(IHasUpdateTime.UpdateTime));
                if (!_repository.PropertyModified(entity, updateTimeExpression))
                {
                    hasUpdateTimeEntity.UpdateTime = DateTime.Now;
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

        /// <summary>
        /// 编辑完成处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task CreatedAsync(TEntity entity, TEdit createDto)
        {
            if (entity is IEdit editEntity && await editEntity.EditedAsync(_serviceProvider))
            {
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}