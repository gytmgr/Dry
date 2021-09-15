using System.ComponentModel;

namespace Dry.Admin.Domain.Shared.Enums
{
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourceType : byte
    {
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 1,

        /// <summary>
        /// 接口
        /// </summary>
        [Description("接口")]
        Api
    }
}