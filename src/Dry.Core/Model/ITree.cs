namespace Dry.Core.Model
{
    /// <summary>
    /// 值类型id的树状接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITree<T> : IHasId<T> where T : struct
    {
        /// <summary>
        /// 上级id
        /// </summary>
        T? ParentId { get; set; }
    }

    /// <summary>
    /// string类型id的树状接口
    /// </summary>
    public interface IStringTree : IHasId<string>
    {
        /// <summary>
        /// 上级id
        /// </summary>
        string ParentId { get; set; }
    }
}