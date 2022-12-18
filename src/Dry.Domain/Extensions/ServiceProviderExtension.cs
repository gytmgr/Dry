using Dry.Domain.Entities;
using Dry.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dry.Domain.Extensions
{
    /// <summary>
    /// 服务创建扩展
    /// </summary>
    public static class ServiceProviderExtension
    {
        /// <summary>
        /// 获取只读仓储服务
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IReadOnlyRepository<TEntity> GetReadOnlyRepository<TEntity>(this IServiceProvider serviceProvider) where TEntity : IEntity, IBoundedContext
            => serviceProvider.GetService<IReadOnlyRepository<TEntity>>();

        /// <summary>
        /// 获取仓储服务
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IRepository<TEntity> GetRepository<TEntity>(this IServiceProvider serviceProvider) where TEntity : IEntity, IBoundedContext
            => serviceProvider.GetService<IRepository<TEntity>>();
    }
}