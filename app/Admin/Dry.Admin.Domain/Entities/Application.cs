using Dry.Admin.Domain.ValueObjects;
using Dry.Core.Model;
using Dry.Domain.Entities;
using Dry.Domain.Repositories;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Admin.Domain.Entities
{
    /// <summary>
    /// 应用
    /// </summary>
    public class Application : IAggregateRoot<string>, IAdminContext, IHasAddTime, ICreate<Application>, IEdit<Application>, IDelete<Application>
    {
        /// <summary>
        /// 系统id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public ApplicationType Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public virtual async Task<Result<int>> CreateAsync([NotNull] IRepository<Application> repository)
        {
            if (await repository.AnyAsync(x => x.Id == Id))
            {
                return Result<int>.Create(-1, "编码已存在");
            }
            Secret = Guid.NewGuid().ToString().Replace("-", string.Empty);
            AddTime = DateTime.Now;
            await repository.AddAsync(this);
            return Result<int>.Create(1);
        }
    }
}