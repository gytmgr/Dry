namespace Dry.Core.Model
{
    /// <summary>
    /// 分页返回结果
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class PagedResult<TResult>
    {
        /// <summary>
        /// 总条目数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 返回条目
        /// </summary>
        public TResult[] Items { get; set; }
    }
}