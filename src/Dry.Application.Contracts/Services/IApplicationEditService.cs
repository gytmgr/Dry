using Dry.Application.Contracts.Dtos;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 编辑应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationEditService<TResult, TEdit, TKey>
        where TResult : IResultDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        Task<TResult> EditAsync([NotNull] TKey id, [NotNull] TEdit editDto);
    }
}