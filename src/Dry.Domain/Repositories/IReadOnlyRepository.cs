﻿using Dry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dry.Domain.Repositories
{
    /// <summary>
    /// 只读仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IReadOnlyRepository<TEntity> where TEntity : IEntity, IBoundedContext
    {
        /// <summary>
        /// 获取查询
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable();

        #region Bool

        /// <summary>
        /// 是否所有记录都满足条件
        /// </summary>
        /// <param name="allPredicate"></param>
        /// <param name="wherePredicates"></param>
        /// <returns></returns>
        Task<bool> AllAsync([NotNull] Expression<Func<TEntity, bool>> allPredicate, params Expression<Func<TEntity, bool>>[] wherePredicates);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(params Expression<Func<TEntity, bool>>[] predicates);

        #endregion

        #region Count

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<int> CountAsync(params Expression<Func<TEntity, bool>>[] predicates);

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<long> LongCountAsync(params Expression<Func<TEntity, bool>>[] predicates);

        #endregion

        #region QueryEntity

        /// <summary>
        /// 条件查询第一条并排序提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 排序条件查询第一条指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TResult> FirstAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 条件查询并排序提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 排序条件查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 排序条件查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, IEnumerable<TResult>>> selector, Expression<Func<TEntity, bool>> predicate = null, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        #endregion

        #region Sum

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<int?> SumAsync([NotNull] Expression<Func<TEntity, int?>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<long?> SumAsync([NotNull] Expression<Func<TEntity, long?>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<float?> SumAsync([NotNull] Expression<Func<TEntity, float?>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<double?> SumAsync([NotNull] Expression<Func<TEntity, double?>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<decimal?> SumAsync([NotNull] Expression<Func<TEntity, decimal?>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        #endregion

        #region Max

        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        #endregion

        #region Min

        /// <summary>
        /// 最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        #endregion

        #region Average

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, int?>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, long?>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<float?> AverageAsync([NotNull] Expression<Func<TEntity, float?>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, double?>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        Task<decimal?> AverageAsync([NotNull] Expression<Func<TEntity, decimal?>> selector, params Expression<Func<TEntity, bool>>[] predicates);

        #endregion
    }
}