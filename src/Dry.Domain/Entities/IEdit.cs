using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Domain.Entities
{
    /// <summary>
    /// 实体编辑接口
    /// </summary>
    public interface IEdit
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual Task EditAsync([NotNull] IServiceProvider serviceProvider)
        {
            if (this is IHasUpdateTime updateTimeEntity)
            {
                updateTimeEntity.UpdateTime = DateTime.Now;
            }
            return Task.CompletedTask;
        }
    }
}