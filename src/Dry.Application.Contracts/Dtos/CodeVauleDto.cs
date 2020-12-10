namespace Dry.Application.Contracts.Dtos
{
    /// <summary>
    /// 编码值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class CodeVauleDto<TValue> : ValueDto<TValue>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}