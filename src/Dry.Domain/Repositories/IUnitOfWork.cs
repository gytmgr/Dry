using System.Threading.Tasks;

namespace Dry.Domain.Repositories
{
    /// <summary>
    /// 工作单元
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    public interface IUnitOfWork<TBoundedContext> where TBoundedContext : IBoundedContext
    {
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        Task<int> CompleteAsync();
    }
}