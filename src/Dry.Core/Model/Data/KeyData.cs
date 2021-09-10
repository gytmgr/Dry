namespace Dry.Core.Model
{
    /// <summary>
    /// 键数据
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class KeyData<TKey, TData> : DryData<TData>
    {
        /// <summary>
        /// 键
        /// </summary>
        public TKey Key { get; set; }
    }
}