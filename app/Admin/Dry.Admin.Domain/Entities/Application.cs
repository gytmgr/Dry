using Dry.Admin.Domain.ValueObjects;
using Dry.Domain.Entities;
using System;

namespace Dry.Admin.Domain.Entities
{
    /// <summary>
    /// 应用
    /// </summary>
    public class Application : IAggregateRoot<string>, IAdminContext, IAddTimeEntity
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
    }
}