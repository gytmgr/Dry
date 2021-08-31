using System.ComponentModel;

namespace Dry.Admin.Domain.Shared.Enums
{
    /// <summary>
    /// 应用类型
    /// </summary>
    public enum ApplicationType
    {
        /// <summary>
        /// Mvc系统
        /// </summary>
        [Description("Mvc系统")]
        MvcSystem = 1,

        /// <summary>
        /// 前端系统
        /// </summary>
        [Description("前端系统")]
        FrontSystem,

        /// <summary>
        /// Web接口
        /// </summary>
        [Description("Web接口")]
        WebApi,

        /// <summary>
        /// Grpc服务
        /// </summary>
        [Description("Grpc服务")]
        GrpcService,

        /// <summary>
        /// Windows服务
        /// </summary>
        [Description("Windows服务")]
        WindowsService,

        /// <summary>
        /// Android应用
        /// </summary>
        [Description("Android应用")]
        AndroidApp,

        /// <summary>
        /// Apple应用
        /// </summary>
        [Description("Apple应用")]
        IOSApp
    }
}