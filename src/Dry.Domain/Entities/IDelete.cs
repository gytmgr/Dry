using Dry.Core.Model;
using Dry.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Domain.Entities
{
    /// <summary>
    /// 实体删除接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDelete<TEntity> where TEntity : IEntity, IBoundedContext
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual async Task<Result<int>> DeleteAsync([NotNull] IServiceProvider serviceProvider)
        {
            if (this is TEntity entity)
            {
                await serviceProvider.GetService<IRepository<TEntity>>().RemoveAsync(entity);
                return Result<int>.Create(1);
            }
            else
            {
                return Result<int>.Create(0, "实体类型错误");
            }
        }
    }
}