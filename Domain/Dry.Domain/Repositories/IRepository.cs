using Dry.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Domain.Repositories
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : IAggregateRoot, IBoundedContext
    {
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync([NotNull] params object[] keyValues);

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<TEntity[]> FindAllAsync();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entitiy"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task AddAsync([NotNull] TEntity entitiy, bool autoSave = false);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task AddAsync([NotNull] TEntity[] entities, bool autoSave = false);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entitiy"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task UpdateAsync([NotNull] TEntity entitiy, bool autoSave = false);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task UpdateAsync([NotNull] TEntity[] entities, bool autoSave = false);

        /// <summary>
        /// 主键删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] object keyValue, bool autoSave = false);

        /// <summary>
        /// 主键删除
        /// </summary>
        /// <param name="keyValues"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] object[] keyValues, bool autoSave = false);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entitiy"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] TEntity entitiy, bool autoSave = false);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task RemoveAsync([NotNull] TEntity[] entities, bool autoSave = false);
    }
}