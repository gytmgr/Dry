using Dry.Core.Model;
using Dry.Domain.Repositories;
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
        /// <param name="repository"></param>
        /// <returns></returns>
        public virtual Task<Result<int>> EditAsync([NotNull] IRepository<TEntity> repository)
        {
            if (this is IHasUpdateTime updateTimeEntity)
            {
                updateTimeEntity.UpdateTime = DateTime.Now;
            }
            return Task.FromResult(Result<int>.Create(1));
        }
    }
}