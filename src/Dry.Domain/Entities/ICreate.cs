using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Domain.Entities
{
    /// <summary>
    /// 实体创建接口
    /// </summary>
    public interface ICreate
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual Task CreateAsync([NotNull] IServiceProvider serviceProvider)
        {
            if (this is IHasAddTime addTimeEntity)
            {
                addTimeEntity.AddTime = DateTime.Now;
            }
            return Task.CompletedTask;
        }
    }
}