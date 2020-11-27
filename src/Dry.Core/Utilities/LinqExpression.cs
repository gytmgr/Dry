using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dry.Core.Utilities
{
    /// <summary>
    /// Enumerable扩展
    /// </summary>
    public static class EnumerableExpression
    {
        /// <summary>
        /// 多字段排序
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> query, params (bool isAsc, Func<TSource, dynamic> keySelector)[] orderBys)
        {
            if (orderBys != null && orderBys.Length > 0)
            {
                var orderQuery = default(IOrderedEnumerable<TSource>);
                foreach (var (isAsc, keySelector) in orderBys)
                {
                    if (isAsc)
                    {
                        if (orderQuery == null)
                        {
                            orderQuery = query.OrderBy(keySelector);
                        }
                        else
                        {
                            orderQuery = orderQuery.ThenBy(keySelector);
                        }
                    }
                    else
                    {
                        if (orderQuery == null)
                        {
                            orderQuery = query.OrderByDescending(keySelector);
                        }
                        else
                        {
                            orderQuery = orderQuery.ThenByDescending(keySelector);
                        }
                    }
                }
                query = orderQuery;
            }
            return query;
        }

        /// <summary>
        /// 使用比较器多字段排序
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> query, params (bool isAsc, Func<TSource, dynamic> keySelector, IComparer<dynamic> comparer)[] orderBys)
        {
            if (orderBys != null && orderBys.Length > 0)
            {
                var orderQuery = default(IOrderedEnumerable<TSource>);
                foreach (var (isAsc, keySelector, comparer) in orderBys)
                {
                    if (isAsc)
                    {
                        if (orderQuery == null)
                        {
                            orderQuery = query.OrderBy(keySelector, comparer);
                        }
                        else
                        {
                            orderQuery = orderQuery.ThenBy(keySelector, comparer);
                        }
                    }
                    else
                    {
                        if (orderQuery == null)
                        {
                            orderQuery = query.OrderByDescending(keySelector, comparer);
                        }
                        else
                        {
                            orderQuery = orderQuery.ThenByDescending(keySelector, comparer);
                        }
                    }
                }
                query = orderQuery;
            }
            return query;
        }
    }

    /// <summary>
    /// Queryable扩展
    /// </summary>
    public static class QueryableExpression
    {
        /// <summary>
        /// 多字段排序
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, params (bool isAsc, Expression<Func<TSource, dynamic>> keySelector)[] orderBys)
        {
            if (orderBys != null && orderBys.Length > 0)
            {
                var orderQuery = default(IOrderedQueryable<TSource>);
                foreach (var (isAsc, keySelector) in orderBys)
                {
                    if (isAsc)
                    {
                        if (orderQuery == null)
                        {
                            orderQuery = query.OrderBy(keySelector);
                        }
                        else
                        {
                            orderQuery = orderQuery.ThenBy(keySelector);
                        }
                    }
                    else
                    {
                        if (orderQuery == null)
                        {
                            orderQuery = query.OrderByDescending(keySelector);
                        }
                        else
                        {
                            orderQuery = orderQuery.ThenByDescending(keySelector);
                        }
                    }
                }
                query = orderQuery;
            }
            return query;
        }

        /// <summary>
        /// 使用比较器多字段排序
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, params (bool isAsc, Expression<Func<TSource, dynamic>> keySelector, IComparer<dynamic> comparer)[] orderBys)
        {
            if (orderBys != null && orderBys.Length > 0)
            {
                var orderQuery = default(IOrderedQueryable<TSource>);
                foreach (var (isAsc, keySelector, comparer) in orderBys)
                {
                    if (isAsc)
                    {
                        if (orderQuery == null)
                        {
                            orderQuery = query.OrderBy(keySelector, comparer);
                        }
                        else
                        {
                            orderQuery = orderQuery.ThenBy(keySelector, comparer);
                        }
                    }
                    else
                    {
                        if (orderQuery == null)
                        {
                            orderQuery = query.OrderByDescending(keySelector, comparer);
                        }
                        else
                        {
                            orderQuery = orderQuery.ThenByDescending(keySelector, comparer);
                        }
                    }
                }
                query = orderQuery;
            }
            return query;
        }
    }

    /// <summary>
    /// linq帮助类
    /// </summary>
    public static class LinqHelper
    {
        /// <summary>
        /// 获取根据字段名获取Lambda表达式
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static Expression<Func<TSource, dynamic>> GetKeySelector<TSource>(string keyName)
        {
            var type = typeof(TSource);
            var param = Expression.Parameter(type);
            var propertyNames = keyName.Split(".");
            Expression propertyAccess = param;
            foreach (var propertyName in propertyNames)
            {
                var property = type.GetProperty(propertyName);
                if (property == null)
                {
                    return null;
                }
                type = property.PropertyType;
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            }
            return Expression.Lambda<Func<TSource, dynamic>>(propertyAccess, param);
        }
    }
}