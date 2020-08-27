﻿using AutoMapper;
using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
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
    /// 查询应用服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class ApplicationQueryService<TEntity, TResult, TQuery> :
        IApplicationQueryService<TResult, TQuery>
        where TEntity : IAggregateRoot, IBoundedContext
        where TResult : IResultDto
        where TQuery : IQueryDto
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
        public ApplicationQueryService(IMapper mapper, IRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 根据查询对象获取linq表达式
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        protected abstract IQueryable<TEntity> GetQueryable([NotNull] IQueryable<TEntity> queryable, TQuery queryDto);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(TQuery queryDto)
        {
            var queryable = _repository.GetQueryable();
            queryable = GetQueryable(queryable, queryDto);
            return await _repository.AnyAsync(queryable);
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(TQuery queryDto)
        {
            var queryable = _repository.GetQueryable();
            queryable = GetQueryable(queryable, queryDto);
            return await _repository.CountAsync(queryable);
        }

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync(TQuery queryDto)
        {
            var queryable = _repository.GetQueryable();
            queryable = GetQueryable(queryable, queryDto);
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
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ArrayAsync(TQuery queryDto)
        {
            var queryable = _repository.GetQueryable();
            queryable = GetQueryable(queryable, queryDto);
            var entities = await _repository.ToArrayAsync(queryable);
            return _mapper.Map<TResult[]>(entities);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<PagedResultDto<TResult>> ArrayAsync([NotNull] PagedQueryDto<TQuery> queryDto)
        {
            var queryable = _repository.GetQueryable();
            queryable = GetQueryable(queryable, queryDto.Param);
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
    /// 查增应用服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationQueryService<TEntity, TResult, TQuery, TCreate> :
        ApplicationQueryService<TEntity, TResult, TQuery>,
        IApplicationQueryService<TResult, TQuery, TCreate>
        where TEntity : IAggregateRoot, IBoundedContext
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ApplicationQueryService(IMapper mapper, IRepository<TEntity> repository) : base(mapper, repository)
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
    /// 查增删应用服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryService<TEntity, TResult, TQuery, TCreate, TKey> :
        ApplicationQueryService<TEntity, TResult, TQuery, TCreate>,
        IApplicationQueryService<TResult, TQuery, TCreate, TKey>
        where TEntity : IAggregateRoot<TKey>, IBoundedContext
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ApplicationQueryService(IMapper mapper, IRepository<TEntity> repository) : base(mapper, repository)
        { }

        /// <summary>
        /// 根据查询对象获取linq表达式
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        protected override IQueryable<TEntity> GetQueryable([NotNull] IQueryable<TEntity> queryable, TQuery queryDto)
        {
            if (queryDto != null)
            {
                if (queryDto.Id != null && !queryDto.Id.Equals(default(TKey)))
                {
                    queryable = queryable.Where(x => x.Id.Equals(queryDto.Id));
                }
                if (queryDto.IdNotEqual != null && !queryDto.IdNotEqual.Equals(default(TKey)))
                {
                    queryable = queryable.Where(x => !x.Id.Equals(queryDto.IdNotEqual));
                }
                if (queryDto.Ids != null)
                {
                    queryable = queryable.Where(x => queryDto.Ids.Contains(x.Id));
                }
                if (queryDto.IdsNotEqual != null)
                {
                    queryable = queryable.Where(x => !queryDto.IdsNotEqual.Contains(x.Id));
                }
            }
            return queryable;
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
            if (entity != null)
            {
                await _repository.RemoveAsync(entity, true);
                return _mapper.Map<TResult>(entity);
            }
            return default;
        }
    }

    /// <summary>
    /// 查增删改应用服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryService<TEntity, TResult, TQuery, TCreate, TEdit, TKey> :
        ApplicationQueryService<TEntity, TResult, TQuery, TCreate, TKey>,
        IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey>
        where TEntity : IAggregateRoot<TKey>, IBoundedContext
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ApplicationQueryService(IMapper mapper, IRepository<TEntity> repository) : base(mapper, repository)
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