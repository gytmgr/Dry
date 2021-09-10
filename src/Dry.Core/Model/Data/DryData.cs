namespace Dry.Core.Model
{
    /// <summary>
    /// 泛型数据
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class DryData<TData>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public TData Data { get; set; }
    }
}