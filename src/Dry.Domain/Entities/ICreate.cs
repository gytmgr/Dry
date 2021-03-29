using Dry.Core.Model;
using Dry.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Domain.Entities
{
    /// <summary>
    /// 实体创建接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICreate<TEntity> where TEntity : IEntity, IBoundedContext
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual async Task<Result<int>> CreateAsync([NotNull] IServiceProvider serviceProvider)
        {
            if (this is IHasAddTime addTimeEntity)
            {
                addTimeEntity.AddTime = DateTime.Now;
            }
            if (this is TEntity entity)
            {
                await serviceProvider.GetService<IRepository<TEntity>>().AddAsync(entity);
                return Result<int>.Create(1);
            }
            else
            {
                return Result<int>.Create(0, "实体类型错误");
            }
        }
    }
}