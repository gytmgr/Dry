namespace Dry.Core.Model
{
    /// <summary>
    /// 值类型id的树状接口
    /// </summary>
    public interface ITree<T> where T : struct
    {
        /// <summary>
        /// 系统id
        /// </summary>
        T Id { get; set; }

        /// <summary>
        /// 上级id
        /// </summary>
        T? ParentId { get; set; }
    }

    /// <summary>
    /// string类型id的树状接口
    /// </summary>
    public interface IStringTree
    {
        /// <summary>
        /// 系统id
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 上级id
        /// </summary>
        string ParentId { get; set; }
    }
}