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
        Task DeleteAsync([NotNull] IServiceProvider serviceProvider);
    }
}