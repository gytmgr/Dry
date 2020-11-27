namespace Dry.Application.Contracts.Dtos
{
    /// <summary>
    /// 值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class ValueDto<TValue>
    {
        /// <summary>
        /// 值
        /// </summary>
        public TValue Value { get; set; }
    }
}