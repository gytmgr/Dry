﻿using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using Dry.Core.Utilities;
using Dry.Domain;
using Dry.Domain.Entities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Services
{
    /// <summary>
    /// 基础查、增、改应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationCreateEditService<TBoundedContext, TEntity, TResult, TCreate, TEdit, TKey> :
        ApplicationCreateService<TBoundedContext, TEntity, TResult, TCreate, TKey>,
        IApplicationCreateEditService<TResult, TCreate, TEdit, TKey>
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
        public ApplicationCreateEditService(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

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
            await EditedAsync(entity, editDto);
            return _mapper.Map<TResult>(entity);
        }

        /// <summary>
        /// 编辑完成处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task EditedAsync(TEntity entity, TEdit createDto)
        {
            if (entity is IEdit editEntity && await editEntity.EditedAsync(_serviceProvider))
            {
                await _unitOfWork.CompleteAsync();
            }
        }
    }

    /// <summary>
    /// 条件查、增、改应用服务接口
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryCreateEditService<TBoundedContext, TEntity, TResult, TQuery, TCreate, TEdit, TKey> :
        ApplicationQueryCreateService<TBoundedContext, TEntity, TResult, TQuery, TCreate, TKey>,
        IApplicationQueryCreateEditService<TResult, TQuery, TCreate, TEdit, TKey>
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
        public ApplicationQueryCreateEditService(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

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
            await EditedAsync(entity, editDto);
            return _mapper.Map<TResult>(entity);
        }

        /// <summary>
        /// 编辑完成处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createDto"></param>
        /// <returns></returns>
        protected virtual async Task EditedAsync(TEntity entity, TEdit createDto)
        {
            if (entity is IEdit editEntity && await editEntity.EditedAsync(_serviceProvider))
            {
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}