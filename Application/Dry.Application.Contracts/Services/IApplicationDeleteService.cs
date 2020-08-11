using Dry.Application.Contracts.Dtos;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 应用服务删除接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IApplicationDeleteService<TResult> where TResult : IResultDto
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TResult> DeleteAsync([NotNull] object id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<TResult> DeleteAsync([NotNull] object[] ids);
    }
}