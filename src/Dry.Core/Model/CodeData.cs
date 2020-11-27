namespace Dry.Core.Model
{
    /// <summary>
    /// 编码数据
    /// </summary>
    /// <typeparam name="TCode"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class CodeData<TCode, TData>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public TCode Code { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public TData Data { get; set; }
    }

    /// <summary>
    /// 编码数据
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class CodeData<TData> : CodeData<string, TData>
    {
    }
}