using Dry.Core.Model;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Domain.Entities
{
    /// <summary>
    /// 实体编辑接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEdit<TEntity> where TEntity : IEntity, IBoundedContext
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual Task<Result<int>> EditAsync([NotNull] IServiceProvider serviceProvider)
        {
            if (this is IHasUpdateTime updateTimeEntity)
            {
                updateTimeEntity.UpdateTime = DateTime.Now;
            }
            return Task.FromResult(Result<int>.Create(1));
        }
    }
}