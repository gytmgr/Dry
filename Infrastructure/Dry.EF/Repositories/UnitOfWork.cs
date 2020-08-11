using Dry.Domain;
using Dry.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Dry.EF.Repositories
{
    /// <summary>
    /// 工作单元
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    public class UnitOfWork<TBoundedContext> : IUnitOfWork<TBoundedContext> where TBoundedContext : IBoundedContext
    {
        /// <summary>
        /// ef上下文
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(TBoundedContext context)
        {
            _context = context as DbContext;
        }

        /// <summary>
        /// 异步提交
        /// </summary>
        /// <returns></returns>
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}