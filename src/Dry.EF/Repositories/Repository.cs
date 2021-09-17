using Dry.Core.Utilities;
using Dry.Domain;
using Dry.Domain.Entities;
using Dry.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dry.EF.Repositories
{
    /// <summary>
    /// ef仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, IBoundedContext
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        protected readonly IServiceProvider _provider;

        /// <summary>
        /// ef上下文
        /// </summary>
        protected readonly DbContext _context;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="provider"></param>
        public Repository(IServiceProvider provider)
        {
            _provider = provider;
            var interfaces = typeof(TEntity).GetInterfaces();
            var boundedContextType = interfaces.First(x => x != typeof(IBoundedContext) && typeof(IBoundedContext).IsAssignableFrom(x));
            _context = (DbContext)_provider.GetService(boundedContextType);
        }

        #region Tracking

        /// <summary>
        /// 属性是否更改
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entitiy"></param>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public virtual bool PropertyModified<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression)
            => _context.Entry(entitiy).Property(propertyExpression).IsModified;

        /// <summary>
        /// 单数属性延迟加载
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entitiy"></param>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public virtual async Task SinglePropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression) where TProperty : class
            => await _context.Entry(entitiy).Reference(propertyExpression).LoadAsync();

        /// <summary>
        /// 单数属性延迟加载
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entitiy"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public virtual async Task<TProperty> SinglePropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression, [NotNull] params Expression<Func<TProperty, dynamic>>[] paths) where TProperty : class
        {
            var queryable = _context.Entry(entitiy).Reference(propertyExpression).Query();
            if (paths is not null)
            {
                foreach (var path in paths)
                {
                    queryable = queryable.Include(path);
                }
            }
            return await queryable.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 复数属性延迟加载
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entitiy"></param>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public virtual async Task ArrayPropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression) where TProperty : class
            => await _context.Entry(entitiy).Collection(propertyExpression).LoadAsync();

        /// <summary>
        /// 复数属性延迟加载
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entitiy"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public virtual async Task<TProperty[]> ArrayPropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression, [NotNull] params Expression<Func<TProperty, dynamic>>[] paths) where TProperty : class
        {
            var queryable = _context.Entry(entitiy).Collection(propertyExpression).Query();
            if (paths is not null)
            {
                foreach (var path in paths)
                {
                    queryable = queryable.Include(path);
                }
            }
            return await queryable.ToArrayAsync();
        }

        #endregion

        #region Add

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task AddAsync([NotNull] params TEntity[] entities)
            => await _context.AddRangeAsync(entities);

        #endregion

        #region Update

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync([NotNull] params TEntity[] entities)
        {
            _context.UpdateRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="set"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync([NotNull] Action<TEntity> set, params Expression<Func<TEntity, bool>>[] predicates)
        {
            var entities = await _context.Set<TEntity>().Where(predicates).ToArrayAsync();
            foreach (var entity in entities)
            {
                set(entity);
            }
        }

        #endregion

        #region Remove

        /// <summary>
        /// 主键删除
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync([NotNull] params object[] keyValues)
        {
            var entity = await _context.FindAsync<TEntity>(keyValues);
            if (entity is not null)
            {
                _context.Remove(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task RemoveAsync([NotNull] params TEntity[] entities)
        {
            _context.RemoveRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(params Expression<Func<TEntity, bool>>[] predicates)
        {
            var entities = await _context.Set<TEntity>().Where(predicates).ToArrayAsync();
            _context.RemoveRange(entities);
        }

        #endregion

        #region All

        /// <summary>
        /// 是否所有记录都满足条件
        /// </summary>
        /// <param name="allPredicate"></param>
        /// <param name="wherePredicates"></param>
        /// <returns></returns>
        public virtual async Task<bool> AllAsync([NotNull] Expression<Func<TEntity, bool>> allPredicate, params Expression<Func<TEntity, bool>>[] wherePredicates)
            => await _context.Set<TEntity>().Where(wherePredicates).AllAsync(allPredicate);

        #endregion

        #region Any

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).AnyAsync();

        #endregion

        #region Count

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).CountAsync();

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<long> LongCountAsync(params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).LongCountAsync();

        #endregion

        #region Find

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync([NotNull] params object[] keyValues)
            => await _context.FindAsync<TEntity>(keyValues);

        #endregion

        #region First

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(params Expression<Func<TEntity, bool>>[] predicates)
            => await FirstAsync(predicates, null);

        /// <summary>
        /// 条件查询第一条并提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] paths)
            => await FirstAsync(predicate, paths, null);

        /// <summary>
        /// 条件查询第一条并排序提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await FirstAsync(predicate.ToNullArray(), paths, orderBys);

        /// <summary>
        /// 条件查询第一条并排序提前加载导航属性
        /// </summary>
        /// <param name="predicates"></param>
        /// <param name="paths"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>>[] predicates, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
        {
            var queryable = _context.Set<TEntity>().AsQueryable();
            if (paths is not null)
            {
                foreach (var path in paths)
                {
                    queryable = queryable.Include(path);
                }
            }
            return await queryable.Where(predicates).OrderBy(orderBys).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 排序条件查询第一条指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await FirstAsync(selector, predicate.ToNullArray(), orderBys);

        /// <summary>
        /// 排序条件查询第一条指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>[] predicates, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await _context.Set<TEntity>().Where(predicates).OrderBy(orderBys).Select(selector).FirstOrDefaultAsync();

        /// <summary>
        /// 自定义查询第一条并提前加载导航属性
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync<TResult>([NotNull] Func<IQueryable<TEntity>, IQueryable<TResult>> func, params Expression<Func<TEntity, dynamic>>[] paths)
        {
            var queryable = _context.Set<TEntity>().AsQueryable();
            if (paths is not null)
            {
                foreach (var path in paths)
                {
                    queryable = queryable.Include(path);
                }
            }
            return await func(queryable).FirstOrDefaultAsync();
        }

        #endregion

        #region ToArray

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<TEntity[]> ToArrayAsync(params Expression<Func<TEntity, bool>>[] predicates)
            => await ToArrayAsync(predicates, null);

        /// <summary>
        /// 条件查询并提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public virtual async Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] paths)
            => await ToArrayAsync(predicate, paths, null);

        /// <summary>
        /// 条件查询并排序提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await ToArrayAsync(predicate.ToNullArray(), paths, orderBys);

        /// <summary>
        /// 条件查询并排序提前加载导航属性
        /// </summary>
        /// <param name="predicates"></param>
        /// <param name="paths"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>>[] predicates, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
        {
            var queryable = _context.Set<TEntity>().AsQueryable();
            if (paths is not null)
            {
                foreach (var path in paths)
                {
                    queryable = queryable.Include(path);
                }
            }
            return await queryable.Where(predicates).OrderBy(orderBys).ToArrayAsync();
        }

        /// <summary>
        /// 排序条件查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await ToArrayAsync(selector, predicate.ToNullArray(), orderBys);

        /// <summary>
        /// 排序条件查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>[] predicates, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await _context.Set<TEntity>().Where(predicates).OrderBy(orderBys).Select(selector).ToArrayAsync();

        /// <summary>
        /// 排序条件查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, IEnumerable<TResult>>> selector, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await ToArrayAsync(selector, predicate.ToNullArray(), orderBys);

        /// <summary>
        /// 排序条件查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, IEnumerable<TResult>>> selector, Expression<Func<TEntity, bool>>[] predicates, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await _context.Set<TEntity>().Where(predicates).OrderBy(orderBys).SelectMany(selector).ToArrayAsync();

        /// <summary>
        /// 自定义查询并提前加载导航属性
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ToArrayAsync<TResult>([NotNull] Func<IQueryable<TEntity>, IQueryable<TResult>> func, params Expression<Func<TEntity, dynamic>>[] paths)
        {
            var queryable = _context.Set<TEntity>().AsQueryable();
            if (paths is not null)
            {
                foreach (var path in paths)
                {
                    queryable = queryable.Include(path);
                }
            }
            return await func(queryable).ToArrayAsync();
        }

        #endregion

        #region Sum

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<int?> SumAsync([NotNull] Expression<Func<TEntity, int?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).SumAsync(selector);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<long?> SumAsync([NotNull] Expression<Func<TEntity, long?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).SumAsync(selector);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<float?> SumAsync([NotNull] Expression<Func<TEntity, float?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).SumAsync(selector);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<double?> SumAsync([NotNull] Expression<Func<TEntity, double?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).SumAsync(selector);

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<decimal?> SumAsync([NotNull] Expression<Func<TEntity, decimal?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).SumAsync(selector);

        #endregion

        #region Max

        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<TResult> MaxAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).MaxAsync(selector);

        #endregion

        #region Min

        /// <summary>
        /// 最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<TResult> MinAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).MinAsync(selector);

        #endregion

        #region Average

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, int?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).AverageAsync(selector);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, long?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).AverageAsync(selector);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<float?> AverageAsync([NotNull] Expression<Func<TEntity, float?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).AverageAsync(selector);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, double?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).AverageAsync(selector);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public virtual async Task<decimal?> AverageAsync([NotNull] Expression<Func<TEntity, decimal?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
            => await _context.Set<TEntity>().Where(predicates).AverageAsync(selector);

        #endregion
    }
}