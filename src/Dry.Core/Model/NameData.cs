namespace Dry.Core.Model
{
    /// <summary>
    /// 名称数据
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class NameData<TData>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public TData Data { get; set; }
    }
}