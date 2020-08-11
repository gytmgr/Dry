using Dry.Application.Contracts.Dtos;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 应用服务编辑接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    public interface IApplicationEditService<TResult, TEdit> where TResult : IResultDto where TEdit : IEditDto
    {

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        Task<TResult> EditAsync([NotNull] object id, [NotNull] TEdit editDto);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        Task<TResult> EditAsync([NotNull] object[] ids, [NotNull] TEdit editDto);
    }
}