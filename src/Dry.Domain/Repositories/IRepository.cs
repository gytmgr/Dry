using Dry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dry.Domain.Repositories
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity, IBoundedContext
    {
        /// <summary>
        /// 获取linq查询表达式
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable();

        /// <summary>
        /// 提前加载
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        IQueryable<TEntity> Include([NotNull] IQueryable<TEntity> queryable, [NotNull] params Expression<Func<TEntity, dynamic>>[] paths);

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync([NotNull] params object[] keyValues);

        #region Add

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entitiy"></param>
        /// <returns></returns>
        Task AddAsync([NotNull] TEntity entitiy);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddAsync([NotNull] TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entitiy"></param>
        /// <returns></returns>
        Task UpdateAsync([NotNull] TEntity entitiy);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateAsync([NotNull] TEntity[] entities);

        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="set"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task UpdateAsync([NotNull] Action<TEntity> set, Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region Remove

        /// <summary>
        /// 主键删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] object keyValue);

        /// <summary>
        /// 主键删除
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] object[] keyValues);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entitiy"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] TEntity entitiy);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] TEntity[] entities);

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region All

        /// <summary>
        /// 是否所有记录都满足条件
        /// </summary>
        /// <param name="allPredicate"></param>
        /// <param name="wherePredicate"></param>
        /// <returns></returns>
        Task<bool> AllAsync([NotNull] Expression<Func<TEntity, bool>> allPredicate, Expression<Func<TEntity, bool>> wherePredicate = null);

        #endregion

        #region Any

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Any
        /// </summary>
        /// <param name="querable"></param>
        /// <returns></returns>
        Task<bool> AnyAsync([NotNull] IQueryable<TEntity> querable);

        #endregion

        #region Count

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="querable"></param>
        /// <returns></returns>
        Task<int> CountAsync([NotNull] IQueryable<TEntity> querable);

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="querable"></param>
        /// <returns></returns>
        Task<int> LongCountAsync([NotNull] IQueryable<TEntity> querable);

        #endregion

        #region First

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 排序查询第一条
        /// </summary>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync([NotNull] params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 条件查询第一条并排序
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync([NotNull] Expression<Func<TEntity, bool>> predicate, [NotNull] params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 条件查询第一条并提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync([NotNull] Expression<Func<TEntity, bool>> predicate, [NotNull] params Expression<Func<TEntity, dynamic>>[] paths);

        /// <summary>
        /// 条件查询第一条并排序提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync([NotNull] Expression<Func<TEntity, bool>> predicate, [NotNull] Expression<Func<TEntity, dynamic>>[] paths, [NotNull] params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 排序查询第一条指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TResult> FirstAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 排序条件查询第一条指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TResult> FirstAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, [NotNull] Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="querable"></param>
        /// <returns></returns>
        Task<TResult> FirstAsync<TResult>([NotNull] IQueryable<TResult> querable);

        #endregion

        #region ToArray

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 排序查询
        /// </summary>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TEntity[]> ToArrayAsync([NotNull] params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 条件查询并排序
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TEntity[]> ToArrayAsync([NotNull] Expression<Func<TEntity, bool>> predicate, [NotNull] params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 条件查询并提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        Task<TEntity[]> ToArrayAsync([NotNull] Expression<Func<TEntity, bool>> predicate, [NotNull] params Expression<Func<TEntity, dynamic>>[] paths);

        /// <summary>
        /// 条件查询并排序提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TEntity[]> ToArrayAsync([NotNull] Expression<Func<TEntity, bool>> predicate, [NotNull] Expression<Func<TEntity, dynamic>>[] paths, [NotNull] params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 排序查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 排序查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, IEnumerable<TResult>>> selector, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 排序条件查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, [NotNull] Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 排序条件查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, IEnumerable<TResult>>> selector, [NotNull] Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="querable"></param>
        /// <returns></returns>
        Task<TResult[]> ToArrayAsync<TResult>([NotNull] IQueryable<TResult> querable);

        #endregion

        #region Sum

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int?> SumAsync([NotNull] Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long?> SumAsync([NotNull] Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<float?> SumAsync([NotNull] Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<double?> SumAsync([NotNull] Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<decimal?> SumAsync([NotNull] Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region Max

        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region Min

        /// <summary>
        /// 最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region Average

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<float?> AverageAsync([NotNull] Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<decimal?> AverageAsync([NotNull] Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate = null);

        #endregion
    }
}