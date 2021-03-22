using System;

namespace Dry.Domain.Entities
{
    /// <summary>
    /// 有更新时间实体
    /// </summary>
    public interface IHasUpdateTime
    {
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}