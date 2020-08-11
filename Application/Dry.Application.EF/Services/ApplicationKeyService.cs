using AutoMapper;
using Dry.Application.Contracts.Dtos;
using Dry.Domain;
using Dry.Domain.Entities;
using Dry.EF.Repositories;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Dry.Application.EF.Services
{
    /// <summary>
    /// 单一主键应用服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationKeyService<TEntity, TResult, TQuery, TCreate, TEdit, TKey> :
        ApplicationService<TEntity, TResult, TQuery, TCreate, TEdit>
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
        public ApplicationKeyService(IMapper mapper, IEFRepository<TEntity> repository) : base(mapper, repository)
        {
        }

        /// <summary>
        /// 根据查询对象获取linq表达式
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        protected override IQueryable<TEntity> GetQueryable([NotNull] IQueryable<TEntity> queryable, TQuery query)
        {
            if (query != null)
            {
                if (query.Id != null && !query.Id.Equals(default(TKey)))
                {
                    queryable = queryable.Where(x => x.Id.Equals(query.Id));
                }
                if (query.IdNotEqual != null && !query.IdNotEqual.Equals(default(TKey)))
                {
                    queryable = queryable.Where(x => !x.Id.Equals(query.IdNotEqual));
                }
                if (query.Ids != null)
                {
                    queryable = queryable.Where(x => query.Ids.Contains(x.Id));
                }
                if (query.IdsNotEqual != null)
                {
                    queryable = queryable.Where(x => !query.IdsNotEqual.Contains(x.Id));
                }
            }
            return queryable;
        }
    }

    /// <summary>
    /// 应用服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationKeyService<TEntity, TResult, TQuery, TKey> :
        ApplicationKeyService<TEntity, TResult, TQuery, ICreateDto, IEditDto, TKey>
        where TEntity : IAggregateRoot<TKey>, IBoundedContext
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ApplicationKeyService(IMapper mapper, IEFRepository<TEntity> repository) : base(mapper, repository)
        {
        }
    }
}