namespace Dry.Core.Model
{
    /// <summary>
    /// 键数据
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class KeyData<TKey, TData>
    {
        /// <summary>
        /// 键
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public TData Data { get; set; }
    }
}