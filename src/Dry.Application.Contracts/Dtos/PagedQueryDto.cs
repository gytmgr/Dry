namespace Dry.Application.Contracts.Dtos
{
    /// <summary>
    /// 分页查询dto
    /// </summary>
    public class PagedQueryDto
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页条目数
        /// </summary>
        public int PageSize { get; set; } = 20;
    }

    /// <summary>
    /// 分页查询dto
    /// </summary>
    /// <typeparam name="TQueryDto"></typeparam>
    public class PagedQueryDto<TQueryDto> : PagedQueryDto where TQueryDto : IQueryDto
    {

        /// <summary>
        /// 查询参数
        /// </summary>
        public TQueryDto Param { get; set; }
    }
}