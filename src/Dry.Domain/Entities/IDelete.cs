using Dry.Core.Model;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Domain.Entities
{
    /// <summary>
    /// 实体删除接口
    /// </summary>
    public interface IDelete
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync([NotNull] IServiceProvider serviceProvider)
        {
            if (this is IHasAddTime addTimeEntity)
            {
                if (addTimeEntity.AddTime == DateTime.MinValue)
                {
                    throw new BizException("内置数据，不能删除");
                }
            }
            return Task.CompletedTask;
        }
    }
}