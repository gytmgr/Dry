using Dry.Application.Contracts.Dtos;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 删除应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationDeleteService<TResult, TKey> where TResult : IResultDto
    {
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TResult> FindAsync([NotNull] TKey id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TResult> DeleteAsync([NotNull] TKey id);
    }
}