namespace Dry.Domain.Entities
{
    /// <summary>
    /// 有编码实体
    /// </summary>
    public interface IHasCode
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}