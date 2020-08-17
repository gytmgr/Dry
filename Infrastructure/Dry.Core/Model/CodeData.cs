namespace Dry.Core.Model
{
    /// <summary>
    /// 编码数据
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class CodeData<TData>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public TData Data { get; set; }
    }
}