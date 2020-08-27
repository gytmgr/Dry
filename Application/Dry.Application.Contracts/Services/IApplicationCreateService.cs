using Dry.Application.Contracts.Dtos;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 新建应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public interface IApplicationCreateService<TResult, TCreate>
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<TResult> CreateAsync([NotNull] TCreate createDto);
    }
}