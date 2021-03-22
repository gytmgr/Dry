using System;

namespace Dry.Domain.Entities
{
    /// <summary>
    /// 有新增时间实体
    /// </summary>
    public interface IHasAddTime
    {
        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}