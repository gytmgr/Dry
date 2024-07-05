namespace Dry.Admin.Application.RESTFul.Api.Controllers;

/// <summary>
/// 系统资源接口
/// </summary>
[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class ResourceController : ApplicationQueryControllerBase<IResourceAppService, ResourceDto, ResourceQueryDto, Guid>, IResourceAppService
{
}