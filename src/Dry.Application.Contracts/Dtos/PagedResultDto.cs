namespace Dry.Application.Contracts.Dtos
{
    /// <summary>
    /// 分页返回dto
    /// </summary>
    /// <typeparam name="TResultDto"></typeparam>
    public class PagedResultDto<TResultDto> where TResultDto : IResultDto
    {
        /// <summary>
        /// 总条目数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 返回条目
        /// </summary>
        public TResultDto[] Items { get; set; }
    }
}