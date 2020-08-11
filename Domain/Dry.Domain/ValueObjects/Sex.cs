using System.ComponentModel;

namespace Dry.Domain.ValueObjects
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex : byte
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Woman
    }
}