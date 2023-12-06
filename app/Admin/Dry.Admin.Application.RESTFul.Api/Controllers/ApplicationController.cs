using Dry.Application.RESTFul.Api.Controllers;

namespace Dry.Admin.Application.RESTFul.Api.Controllers;

/// <summary>
/// 应用接口
/// </summary>
[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class ApplicationController : ApplicationQueryControllerBase<IApplicationAppService, ApplicationDto, ApplicationQueryDto, ApplicationCreateDto, ApplicationEditDto, string>, IApplicationAppService
{
    /// <summary>
    /// 获取应用类型
    /// </summary>
    /// <returns></returns>
    [HttpGet("Type")]
    public async Task<KeyValuePair<int, string>[]> TypeArrayAsync()
        => await AppService.TypeArrayAsync();
}