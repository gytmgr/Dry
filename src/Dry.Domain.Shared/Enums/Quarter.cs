using System.ComponentModel;

namespace Dry.Domain.Shared.Enums
{
    /// <summary>
    /// 季度
    /// </summary>
    public enum Quarter : byte
    {
        /// <summary>
        /// 一季度
        /// </summary>
        [Description("一季度")]
        First = 1,

        /// <summary>
        /// 二季度
        /// </summary>
        [Description("二季度")]
        Second,

        /// <summary>
        /// 三季度
        /// </summary>
        [Description("三季度")]
        Third,

        /// <summary>
        /// 四季度
        /// </summary>
        [Description("四季度")]
        Fourth
    }
}