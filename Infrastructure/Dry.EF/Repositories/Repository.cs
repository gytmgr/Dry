using Dry.Core.Utilities;
using Dry.Domain;
using Dry.Domain.Entities;
using Dry.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// 获取linq查询表达式
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQueryable()
            => _context.Set<TEntity>();

        /// <summary>
        /// 提前加载
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Include(IQueryable<TEntity> queryable, params Expression<Func<TEntity, dynamic>>[] paths)
        {
            foreach (var path in paths)
            {
                queryable = queryable.Include(path);
            }
            return queryable;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
            => await _context.FindAsync<TEntity>(keyValues);

        #region Add

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entitiy"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity entitiy)
            => await _context.AddAsync(entitiy);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity[] entities)
            => await _context.AddRangeAsync(entities);

        #endregion

        #region Update

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entitiy"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync(TEntity entitiy)
        {
            _context.Update(entitiy);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync(TEntity[] entities)
        {
            _context.UpdateRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="set"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(Action<TEntity> set, Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _context.Set<TEntity>().Where(predicate).ToArrayAsync();
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
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(object keyValue)
        {
            var entity = await _context.FindAsync<TEntity>(keyValue);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        /// <summary>
        /// 主键删除
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(object[] keyValues)
        {
            var entity = await _context.FindAsync<TEntity>(keyValues);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entitiy"></param>
        /// <returns></returns>
        public virtual Task RemoveAsync(TEntity entitiy)
        {
            _context.Remove(entitiy);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task RemoveAsync(TEntity[] entities)
        {
            _context.RemoveRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _context.Set<TEntity>().Where(predicate).ToArrayAsync();
            _context.RemoveRange(entities);
        }

        #endregion

        #region All

        /// <summary>
        /// 是否所有记录都满足条件
        /// </summary>
        /// <param name="allPredicate"></param>
        /// <param name="wherePredicate"></param>
        /// <returns></returns>
        public virtual async Task<bool> AllAsync(Expression<Func<TEntity, bool>> allPredicate, Expression<Func<TEntity, bool>> wherePredicate)
        {
            var queryable = GetQueryable();
            if (wherePredicate != null)
            {
                queryable = queryable.Where(wherePredicate);
            }
            return await queryable.AllAsync(allPredicate);
        }

        #endregion

        #region Any

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                return await GetQueryable().AnyAsync();
            }
            else
            {
                return await GetQueryable().AnyAsync(predicate);
            }
        }

        /// <summary>
        /// Any
        /// </summary>
        /// <param name="querable"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(IQueryable<TEntity> querable)
            => await querable.AnyAsync();

        #endregion

        #region Count

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                return await _context.Set<TEntity>().CountAsync();
            }
            else
            {
                return await _context.Set<TEntity>().CountAsync(predicate);
            }
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="querable"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(IQueryable<TEntity> querable)
            => await querable.CountAsync();

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                return await _context.Set<TEntity>().LongCountAsync();
            }
            else
            {
                return await _context.Set<TEntity>().LongCountAsync(predicate);
            }
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="querable"></param>
        /// <returns></returns>
        public virtual async Task<int> LongCountAsync(IQueryable<TEntity> querable)
            => await querable.CountAsync();

        #endregion

        #region First

        /// <summary>
        /// 排序查询第一条
        /// </summary>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await GetQueryable().OrderBy(orderBys).FirstOrDefaultAsync();

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                return await GetQueryable().FirstOrDefaultAsync();
            }
            else
            {
                return await GetQueryable().FirstOrDefaultAsync(predicate);
            }
        }

        /// <summary>
        /// 条件查询第一条并排序
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await GetQueryable().Where(predicate).OrderBy(orderBys).FirstOrDefaultAsync();

        /// <summary>
        /// 条件查询第一条并提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] paths)
        {
            var queryable = GetQueryable();
            foreach (var path in paths)
            {
                queryable = queryable.Include(path);
            }
            return await queryable.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 条件查询第一条并排序提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
        {
            var queryable = GetQueryable();
            foreach (var path in paths)
            {
                queryable = queryable.Include(path);
            }
            return await queryable.OrderBy(orderBys).FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 排序查询第一条指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync<TResult>(Expression<Func<TEntity, TResult>> selector, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
        {
            var queryable = GetQueryable();
            if (orderBys != null)
            {
                queryable = queryable.OrderBy(orderBys);
            }
            return await queryable.Select(selector).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 排序条件查询第一条指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
        {
            var queryable = GetQueryable().Where(predicate);
            if (orderBys != null)
            {
                queryable = queryable.OrderBy(orderBys);
            }
            return await queryable.Select(selector).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="querable"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync<TResult>(IQueryable<TResult> querable)
            => await querable.FirstOrDefaultAsync();

        #endregion

        #region ToArray

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.ToArrayAsync();
        }

        /// <summary>
        /// 排序查询
        /// </summary>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TEntity[]> ToArrayAsync(params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await GetQueryable().OrderBy(orderBys).ToArrayAsync();

        /// <summary>
        /// 条件查询并排序
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
            => await GetQueryable().Where(predicate).OrderBy(orderBys).ToArrayAsync();

        /// <summary>
        /// 条件查询并提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public virtual async Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] paths)
        {
            var queryable = GetQueryable();
            foreach (var path in paths)
            {
                queryable = queryable.Include(path);
            }
            return await queryable.Where(predicate).ToArrayAsync();
        }

        /// <summary>
        /// 条件查询并排序提前加载导航属性
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
        {
            var queryable = GetQueryable();
            foreach (var path in paths)
            {
                queryable = queryable.Include(path);
            }
            return await queryable.Where(predicate).OrderBy(orderBys).ToArrayAsync();
        }

        /// <summary>
        /// 排序查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ToArrayAsync<TResult>(Expression<Func<TEntity, TResult>> selector, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
        {
            var queryable = GetQueryable();
            if (orderBys != null)
            {
                queryable = queryable.OrderBy(orderBys);
            }
            return await queryable.Select(selector).ToArrayAsync();
        }

        /// <summary>
        /// 排序查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ToArrayAsync<TResult>(Expression<Func<TEntity, IEnumerable<TResult>>> selector, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
        {
            var queryable = GetQueryable();
            if (orderBys != null)
            {
                queryable = queryable.OrderBy(orderBys);
            }
            return await queryable.SelectMany(selector).ToArrayAsync();
        }

        /// <summary>
        /// 排序条件查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ToArrayAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
        {
            var queryable = GetQueryable().Where(predicate);
            if (orderBys != null)
            {
                queryable = queryable.OrderBy(orderBys);
            }
            return await queryable.Select(selector).ToArrayAsync();
        }

        /// <summary>
        /// 排序条件查询指定字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ToArrayAsync<TResult>(Expression<Func<TEntity, IEnumerable<TResult>>> selector, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
        {
            var queryable = GetQueryable().Where(predicate);
            if (orderBys != null)
            {
                queryable = queryable.OrderBy(orderBys);
            }
            return await queryable.SelectMany(selector).ToArrayAsync();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="querable"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ToArrayAsync<TResult>(IQueryable<TResult> querable)
            => await querable.ToArrayAsync();

        #endregion

        #region Sum

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<int?> SumAsync(Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.SumAsync(selector);
        }

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<long?> SumAsync(Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.SumAsync(selector);
        }

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<float?> SumAsync(Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.SumAsync(selector);
        }

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<double?> SumAsync(Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.SumAsync(selector);
        }

        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<decimal?> SumAsync(Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.SumAsync(selector);
        }

        #endregion

        #region Max

        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.MaxAsync(selector);
        }

        #endregion

        #region Min

        /// <summary>
        /// 最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.MinAsync(selector);
        }

        #endregion

        #region Average

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<double?> AverageAsync(Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.AverageAsync(selector);
        }

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<double?> AverageAsync(Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.AverageAsync(selector);
        }

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<float?> AverageAsync(Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.AverageAsync(selector);
        }

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<double?> AverageAsync(Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.AverageAsync(selector);
        }

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<decimal?> AverageAsync(Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var queryable = GetQueryable();
            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }
            return await queryable.AverageAsync(selector);
        }

        #endregion
    }
}