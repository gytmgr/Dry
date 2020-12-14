namespace Dry.Admin.Wasm.Common
{
    /// <summary>
    /// 登录用户
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class LoginUser<TUser> where TUser : class
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public TUser UserInfo { get; set; }
    }
}