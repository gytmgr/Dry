namespace Dry.Core.Model
{
    /// <summary>
    /// 有唯一标识属性
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IHasId<TId>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        TId Id { get; set; }
    }
}